using System.Linq;
using System.IO;
using System.Collections.Generic;
using EternalQuest.Models;
using EternalQuest.Services;
using EternalQuest.Storage;
using EternalQuest.UI;
using static EternalQuest.UI.ConsoleHelpers;

namespace EternalQuest;

public class App
{
    private readonly GoalService _service;
    private readonly IStorage _storage;
    private readonly Menu _menu;

    public App(GoalService service, IStorage storage, Menu menu)
    {
        _service = service;
        _storage = storage;
        _menu = menu;
    }

    public void Run()
    {
        // AUTO-LOAD JSON with score
        try
        {
            const string defaultPath = "goals.json";
            if (File.Exists(defaultPath))
            {
                var json = new JsonStorage();
                var (goals, total) = json.ImportWithScore(defaultPath);
                if (goals.Count > 0 || total > 0)
                {
                    _service.ReplaceAll(goals);
                    _service.SetTotalPoints(total);
                    Info("Auto-loaded goals.json");
                }
            }
        }
        catch { /* ignore */ }

        bool running = true;
        while (running)
        {
            switch (_menu.Show())
            {
                case 1: CreateGoal(); break;
                case 2: ListGoals(); break;
                case 3: RecordEvent(); break;
                case 4: SaveTxt(); break;
                case 5: LoadTxt(); break;
                case 6: ExportJson(); break;
                case 7: ImportJson(); break;
                case 0: running = false; break;
                default: Warn("Invalid choice."); break;
            }
        }

        // AUTO-SAVE JSON with score
        try
        {
            var json = new JsonStorage();
            json.Export(_service.All(), "goals.json", _service.TotalPoints());
            Info("Auto-saved to goals.json");
        }
        catch { /* ignore */ }
    }

    private int NextId()
        => _service.All().Any() ? _service.All().Max(g => g.Id) + 1 : 1;

    private void CreateGoal()
    {
        Console.WriteLine();
        Info("Choose goal type:");
        Console.WriteLine("1) Simple");
        Console.WriteLine("2) Eternal");
        Console.WriteLine("3) Checklist");

        var t    = ReadInt("Type: ");
        var name = ReadText("Name: ", required: true);
        var desc = ReadText("Description: ");
        var pts  = ReadInt("Points (int): ");

        Goal? g = null;
        switch (t)
        {
            case 1:
                g = new SimpleGoal { Id = NextId(), Name = name, Description = desc, Points = pts };
                break;
            case 2:
                g = new EternalGoal { Id = NextId(), Name = name, Description = desc, Points = pts };
                break;
            case 3:
                var target = ReadInt("Target count (int): ");
                var bonus  = ReadInt("Bonus at target (int): ");
                g = new ChecklistGoal {
                    Id = NextId(), Name = name, Description = desc, Points = pts,
                    Target = target, Bonus = bonus
                };
                break;
            default:
                Error("Unknown type.");
                return;
        }

        _service.Add(g);
        Success("Goal created.");
    }

    private void ListGoals()
    {
        Console.WriteLine();
        Info("Your Goals:");
        foreach (var g in _service.All().OrderBy(g => g.Id))
        {
            var line = $"{g.Id,3}: {g.GetDisplay()}";
            if (g.IsComplete()) Success(line);
            else Console.WriteLine(line);
        }
        Console.WriteLine(new string('-', 40));
        Info($"Total Points: {_service.TotalPoints()}");
    }

    private void RecordEvent()
    {
        var id = ReadInt("Enter goal id to record: ");
        var earned = _service.Record(id);
        if (earned > 0) Success($"You earned {earned} points.");
        else Warn("No points earned (invalid id or already completed).");
    }

    private void SaveTxt()
    {
        var path = ReadText("Save to file (default goals.txt): ");
        if (string.IsNullOrWhiteSpace(path)) path = "goals.txt";

        // Write SCORE header then goals via storage (append)
        var header = new List<string> { $"SCORE|{_service.TotalPoints()}" };
        File.WriteAllLines(path, header);
        _storage.Save(_service.All(), path);

        Success("Saved.");
    }

    private void LoadTxt()
    {
        var path = ReadText("Load from file (default goals.txt): ");
        if (string.IsNullOrWhiteSpace(path)) path = "goals.txt";
        if (!File.Exists(path)) { Warn("File not found."); return; }

        int total = 0;
        foreach (var line in File.ReadLines(path))
        {
            if (line.StartsWith("SCORE|"))
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out var t)) total = t;
                break;
            }
        }

        var goals = _storage.Load(path);
        if (goals.Count == 0 && total == 0) { Warn("No goals/score found; keeping current list."); return; }

        _service.ReplaceAll(goals);
        _service.SetTotalPoints(total);
        Success("Loaded.");
    }

    private void ExportJson()
    {
        if (!_service.All().Any())
        {
            Warn("There are 0 goals in memory. Type YES to export an empty file, or press Enter to cancel.");
            var confirm = ReadText("> ");
            if (!string.Equals(confirm, "YES", StringComparison.OrdinalIgnoreCase))
            {
                Warn("Export cancelled.");
                return;
            }
        }

        var path = ReadText("Export JSON to (default goals.json): ");
        if (string.IsNullOrWhiteSpace(path)) path = "goals.json";

        var json = new JsonStorage();
        json.Export(_service.All(), path, _service.TotalPoints());
        Success("Exported JSON.");
    }

    private void ImportJson()
    {
        var path = ReadText("Import JSON from (default goals.json): ");
        if (string.IsNullOrWhiteSpace(path)) path = "goals.json";

        var json = new JsonStorage();
        var (goals, total) = json.ImportWithScore(path);
        if (goals.Count == 0 && total == 0) { Warn("No goals/score found; keeping current list."); return; }

        _service.ReplaceAll(goals);
        _service.SetTotalPoints(total);
        Success("Imported JSON.");
    }
}
