using System.Text.Json;
using System.Text.Json.Serialization;
using EternalQuest.Models;

namespace EternalQuest.Storage;

public class JsonStorage
{
    private record GoalDto(
        string Kind,
        int Id,
        string Name,
        string Description,
        int Points,
        bool Completed,
        int? Target,
        int? Count,
        int? Bonus
    );

    private record SaveDto(
        int TotalPoints,
        List<GoalDto> Goals
    );

    private static GoalDto ToDto(Goal g) => g switch
    {
        SimpleGoal sg => new GoalDto("Simple", sg.Id, sg.Name, sg.Description, sg.Points, sg.Completed, null, null, null),
        EternalGoal eg => new GoalDto("Eternal", eg.Id, eg.Name, eg.Description, eg.Points, false, null, null, null),
        ChecklistGoal cg => new GoalDto("Checklist", cg.Id, cg.Name, cg.Description, cg.Points, cg.Completed, cg.Target, cg.Count, cg.Bonus),
        _ => throw new InvalidOperationException("Unknown goal type")
    };

    private static Goal FromDto(GoalDto d) => d.Kind switch
    {
        "Simple" => new SimpleGoal
        {
            Id = d.Id, Name = d.Name, Description = d.Description, Points = d.Points, Completed = d.Completed
        },
        "Eternal" => new EternalGoal
        {
            Id = d.Id, Name = d.Name, Description = d.Description, Points = d.Points
        },
        "Checklist" => new ChecklistGoal
        {
            Id = d.Id, Name = d.Name, Description = d.Description, Points = d.Points,
            Completed = d.Completed, Target = d.Target ?? 0, Count = d.Count ?? 0, Bonus = d.Bonus ?? 0
        },
        _ => throw new InvalidOperationException("Unknown goal kind")
    };

    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

    // NEW: Export incl. score
    public void Export(IEnumerable<Goal> goals, string path, int totalPoints = 0)
    {
        var list = goals.Select(ToDto).ToList();
        var dto = new SaveDto(totalPoints, list);
        var json = JsonSerializer.Serialize(dto, Options);
        File.WriteAllText(path, json);
    }

    // NEW: Import incl. score (keeps compat with old array format)
    public (List<Goal> Goals, int TotalPoints) ImportWithScore(string path)
    {
        if (!File.Exists(path)) return (new List<Goal>(), 0);
        var json = File.ReadAllText(path);

        // Try new wrapped format
        try
        {
            var dto = JsonSerializer.Deserialize<SaveDto>(json, Options);
            if (dto is not null)
                return (dto.Goals.Select(FromDto).ToList(), dto.TotalPoints);
        }
        catch { /* fall back */ }

        // Fallback: old format was just an array
        try
        {
            var arr = JsonSerializer.Deserialize<List<GoalDto>>(json, Options) ?? new();
            return (arr.Select(FromDto).ToList(), 0);
        }
        catch
        {
            return (new List<Goal>(), 0);
        }
    }

    // Back-compat helper (ignores score)
    public List<Goal> Import(string path) => ImportWithScore(path).Goals;
}
