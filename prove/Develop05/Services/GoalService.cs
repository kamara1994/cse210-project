using System.Text;
using EternalQuest.Models;

namespace EternalQuest.Services;

public class GoalService
{
    private readonly List<Goal> _goals = new();
    private int _totalPoints;

    public IReadOnlyList<Goal> All() => _goals;
    public int TotalPoints() => _totalPoints;

    public void Add(Goal g) => _goals.Add(g);

    public Goal? Find(int id) => _goals.FirstOrDefault(g => g.Id == id);

    public int Record(int id)
    {
        var g = Find(id);
        if (g is null) return 0;
        int earned = g.Record();
        _totalPoints += earned;
        return earned;
    }

    // Simple CSV Save/Load (one line per goal)
    public void Save(string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"TOTAL|{_totalPoints}");
        foreach (var g in _goals)
            sb.AppendLine(g.Serialize());
        File.WriteAllText(path, sb.ToString());
    }

    public void Load(string path)
    {
        _goals.Clear();
        _totalPoints = 0;
        if (!File.Exists(path)) return;

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split("|");
            if (parts[0] == "TOTAL") { _totalPoints = int.Parse(parts[1]); continue; }

            Goal g = parts[0] switch
            {
                nameof(SimpleGoal) => new SimpleGoal(),
                nameof(EternalGoal) => new EternalGoal(),
                nameof(ChecklistGoal) => new ChecklistGoal(),
                _ => throw new InvalidOperationException("Unknown goal type")
            };
            g.Deserialize(parts);
            _goals.Add(g);
        }
    }

    public int NextId() => _goals.Any() ? _goals.Max(g => g.Id) + 1 : 1;
}
