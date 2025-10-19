// CSE 210 - Eternal Quest (Unit 05)
// Author: Joseph Allan Kamara
//
// EXCEEDS REQUIREMENTS:
// - Leveling system (1 level per 1000 points).
// - Badges: FirstSimple, First10Events, FirstChecklist.
// - Negative goals supported (penalty points allowed).
// - Factory-style save/load (human readable).
// - Encapsulation + polymorphism (base Goal + overrides).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; // needed for OrderBy / ThenBy

abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract bool IsComplete { get; }
    public abstract int RecordEvent(out string feedback);
    public abstract string ToDisplayString();
    public abstract string Serialize();

    public static Goal Deserialize(string line)
    {
        var parts = line.Split('|');
        if (parts.Length == 0) throw new InvalidDataException("Invalid line: " + line);

        string type = parts[0];
        switch (type)
        {
            case "Simple":
                // Simple|Name|Desc|Points|Completed
                if (parts.Length < 5) throw new InvalidDataException("Simple goal malformed.");
                {
                    string sName = parts[1];
                    string sDesc = parts[2];
                    int sPts = int.Parse(parts[3]);
                    bool sDone = bool.Parse(parts[4]);
                    var sg = new SimpleGoal(sName, sDesc, sPts);
                    if (sDone) sg.MarkComplete();
                    return sg;
                }

            case "Eternal":
                // Eternal|Name|Desc|Points|TimesRecorded
                if (parts.Length < 5) throw new InvalidDataException("Eternal goal malformed.");
                {
                    string eName = parts[1];
                    string eDesc = parts[2];
                    int ePts = int.Parse(parts[3]);
                    int times = int.Parse(parts[4]);
                    var eg = new EternalGoal(eName, eDesc, ePts);
                    eg.SetTimes(times);
                    return eg;
                }

            case "Checklist":
                // Checklist|Name|Desc|Points|Target|Current|Bonus
                if (parts.Length < 7) throw new InvalidDataException("Checklist goal malformed.");
                {
                    string cName = parts[1];
                    string cDesc = parts[2];
                    int cPts = int.Parse(parts[3]);
                    int target = int.Parse(parts[4]);
                    int current = int.Parse(parts[5]);
                    int bonus = int.Parse(parts[6]);
                    var cg = new ChecklistGoal(cName, cDesc, cPts, target, bonus);
                    cg.SetCurrent(current);
                    return cg;
                }

            default:
                throw new InvalidDataException("Unknown goal type: " + type);
        }
    }
}

sealed class SimpleGoal : Goal
{
    private bool _complete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _complete = false;
    }

    public override bool IsComplete => _complete;

    public void MarkComplete() => _complete = true;

    public override int RecordEvent(out string feedback)
    {
        if (_complete)
        {
            feedback = $"'{Name}' is already completed. No points awarded.";
            return 0;
        }
        _complete = true;
        feedback = $"Completed '{Name}' (+{Points} pts) 🎉";
        return Points;
    }

    public override string ToDisplayString()
    {
        var status = IsComplete ? "[X]" : "[ ]";
        return $"{status} {Name} ({Description}) — {Points} pts";
    }

    public override string Serialize()
    {
        return $"Simple|{Name}|{Description}|{Points}|{_complete}";
    }
}

sealed class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    public override bool IsComplete => false; // never complete
    public int TimesRecorded => _timesRecorded;
    public void SetTimes(int t) => _timesRecorded = Math.Max(0, t);

    public override int RecordEvent(out string feedback)
    {
        _timesRecorded++;
        feedback = $"Recorded '{Name}' #{_timesRecorded} (+{Points} pts)";
        return Points;
    }

    public override string ToDisplayString()
    {
        return $"[∞] {Name} ({Description}) — {Points} pts each — Logged: {TimesRecorded}";
    }

    public override string Serialize()
    {
        return $"Eternal|{Name}|{Description}|{Points}|{_timesRecorded}";
    }
}

sealed class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = Math.Max(1, targetCount);
        _bonus = bonus;
        _currentCount = 0;
    }

    public override bool IsComplete => _currentCount >= _targetCount;
    public int Target => _targetCount;
    public int Current => _currentCount;
    public int Bonus => _bonus;
    public void SetCurrent(int c) => _currentCount = Math.Max(0, c);

    public override int RecordEvent(out string feedback)
    {
        _currentCount++;
        int total = Points;
        bool justCompleted = _currentCount == _targetCount;
        if (justCompleted)
        {
            total += _bonus;
            feedback = $"Progress '{Name}' {Current}/{Target} (+{Points} pts) ✅ Bonus reached! (+{_bonus} pts) 🎊";
        }
        else
        {
            feedback = $"Progress '{Name}' {Current}/{Target} (+{Points} pts)";
        }
        return total;
    }

    public override string ToDisplayString()
    {
        var status = IsComplete ? "[X]" : "[ ]";
        return $"{status} {Name} ({Description}) — {Points} pts, Bonus {Bonus} at {Target} — Completed {Current}/{Target}";
    }

    public override string Serialize()
    {
        return $"Checklist|{Name}|{Description}|{Points}|{_targetCount}|{_currentCount}|{_bonus}";
    }
}

sealed class PlayerProfile
{
    private int _score;
    private int _level;
    private readonly HashSet<string> _badges = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    public int Score => _score;
    public int Level => _level;
    public IReadOnlyCollection<string> Badges => _badges;

    public PlayerProfile()
    {
        _score = 0;
        _level = 1;
    }

    public void AddScore(int delta, out bool leveledUp)
    {
        int beforeLevel = _level;
        _score += delta;
        if (_score < 0) _score = 0;

        _level = Math.Max(1, 1 + (_score / 1000)); // 1000 pts per level
        leveledUp = _level > beforeLevel;
    }

    public void AddBadge(string name) => _badges.Add(name);

    public string Serialize()
    {
        string badges = string.Join(",", _badges);
        return $"SCORE|{_score}|LEVEL|{_level}|BADGES|{badges}";
    }

    public void DeserializeHeader(string headerLine)
    {
        // SCORE|{score}|LEVEL|{level}|BADGES|a,b,c
        var p = headerLine.Split('|');
        if (p.Length >= 2 && p[0] == "SCORE") _score = int.Parse(p[1]);
        if (p.Length >= 4 && p[2] == "LEVEL") _level = int.Parse(p[3]);
        if (p.Length >= 6 && p[4] == "BADGES")
        {
            _badges.Clear();
            if (!string.IsNullOrWhiteSpace(p[5]))
            {
                foreach (var b in p[5].Split(',', StringSplitOptions.RemoveEmptyEntries))
                    _badges.Add(b.Trim());
            }
        }
        if (_level < 1) _level = 1;
        if (_score < 0) _score = 0;
    }
}

class App
{
    private readonly List<Goal> _goals = new List<Goal>();
    private readonly PlayerProfile _player = new PlayerProfile();
    private int _totalEventsRecorded = 0;

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Eternal Quest =====");
            Console.WriteLine($"Score: {_player.Score}   Level: {_player.Level}");
            Console.WriteLine($"Badges: {( _player.Badges.Count == 0 ? "(none)" : string.Join(", ", _player.Badges) )}");
            Console.WriteLine();
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Save goals");
            Console.WriteLine("5. Load goals");
            Console.WriteLine("6. Show score");
            Console.WriteLine("7. Quit");
            Console.Write("Select an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); Pause(); break;
                case "3": RecordEvent(); Pause(); break;
                case "4": Save(); Pause(); break;
                case "5": Load(); Pause(); break;
                case "6": ShowScore(); Pause(); break;
                case "7": return;
                default:  Console.WriteLine("Invalid option."); Pause(); break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.Clear();
        Console.WriteLine("Create Goal Type:");
        Console.WriteLine("  1) Simple (one-time)");
        Console.WriteLine("  2) Eternal (repeatable)");
        Console.WriteLine("  3) Checklist (N times + bonus)");
        Console.WriteLine("  4) Negative (one-time penalty; use negative points)");
        Console.Write("Choose: ");
        var t = Console.ReadLine();

        string name = Prompt("Name: ");
        string desc = Prompt("Description: ");
        int points = PromptInt("Points (can be negative for a penalty): ");

        switch (t)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, desc, points));
                Console.WriteLine("Simple goal created.");
                break;
            case "2":
                _goals.Add(new EternalGoal(name, desc, points));
                Console.WriteLine("Eternal goal created.");
                break;
            case "3":
                int target = PromptInt("How many times to complete? ");
                int bonus = PromptInt("Bonus on final completion: ");
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                Console.WriteLine("Checklist goal created.");
                break;
            case "4":
                _goals.Add(new SimpleGoal(name, desc, points));
                Console.WriteLine("Negative one-time goal created (simple goal with negative points).");
                break;
            default:
                Console.WriteLine("Canceled.");
                break;
        }
    }

    private void ListGoals()
    {
        Console.Clear();
        if (_goals.Count == 0)
        {
            Console.WriteLine("(No goals yet)");
            return;
        }

        Console.WriteLine("Your Goals:");
        int idx = 1;
        foreach (var g in SortedGoals())
        {
            Console.WriteLine($"{idx}. {g.ToDisplayString()}");
            idx++;
        }
    }

    private IEnumerable<Goal> SortedGoals()
    {
        return _goals
            .OrderBy(g => g.IsComplete ? 2 : (g is EternalGoal ? 1 : 0))
            .ThenBy(g => g.Name, StringComparer.OrdinalIgnoreCase);
    }

    private void RecordEvent()
    {
        Console.Clear();
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to record.");
            return;
        }

        Console.WriteLine("Select a goal to record:");
        int i = 1;
        foreach (var g in SortedGoals())
        {
            Console.WriteLine($"{i}. {g.ToDisplayString()}");
            i++;
        }
        int pick = PromptInt("Enter # (or 0 to cancel): ");
        if (pick <= 0 || pick > _goals.Count)
        {
            Console.WriteLine("Canceled.");
            return;
        }

        Goal chosen = null!;
        int idx = 1;
        foreach (var g in SortedGoals())
        {
            if (idx == pick) { chosen = g; break; }
            idx++;
        }

        int earned = chosen.RecordEvent(out string feedback);
        _totalEventsRecorded++;
        _player.AddScore(earned, out bool leveledUp);

        if (chosen is SimpleGoal sg && sg.IsComplete)
            _player.AddBadge("FirstSimple");
        if (_totalEventsRecorded == 10)
            _player.AddBadge("First10Events");
        if (chosen is ChecklistGoal cg && cg.IsComplete)
            _player.AddBadge("FirstChecklist");

        Console.WriteLine(feedback);
        if (earned != 0) Console.WriteLine($"Total points now: {_player.Score}");
        if (leveledUp) Console.WriteLine($"🎯 Level Up! You are now Level {_player.Level}");
    }

    private void Save()
    {
        Console.Clear();
        string file = Prompt("Save to filename (e.g., goals.txt): ");
        try
        {
            using var sw = new StreamWriter(file);
            sw.WriteLine(_player.Serialize());
            foreach (var g in _goals)
                sw.WriteLine(g.Serialize());
            Console.WriteLine("Saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Save failed: " + ex.Message);
        }
    }

    private void Load()
    {
        Console.Clear();
        string file = Prompt("Load from filename (e.g., goals.txt): ");
        try
        {
            var lines = File.ReadAllLines(file);
            _goals.Clear();
            _totalEventsRecorded = 0;

            if (lines.Length == 0)
                throw new InvalidDataException("File is empty.");

            _player.DeserializeHeader(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                _goals.Add(Goal.Deserialize(lines[i]));
            }

            Console.WriteLine($"Loaded {_goals.Count} goals. Score={_player.Score}, Level={_player.Level}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Load failed: " + ex.Message);
        }
    }

    private void ShowScore()
    {
        Console.Clear();
        Console.WriteLine($"Score:  { _player.Score }");
        Console.WriteLine($"Level:  { _player.Level }");
        Console.WriteLine($"Badges: {( _player.Badges.Count == 0 ? "(none)" : string.Join(", ", _player.Badges))}");
    }

    private static string Prompt(string label)
    {
        Console.Write(label);
        return Console.ReadLine() ?? "";
    }

    private static int PromptInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            var s = Console.ReadLine();
            if (int.TryParse(s, out int n)) return n;
            Console.WriteLine("Please enter a valid integer.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("(press Enter) ");
        Console.ReadLine();
    }
}

class Program
{
    static void Main()
    {
        new App().Run();
    }
}
