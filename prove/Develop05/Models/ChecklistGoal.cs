namespace EternalQuest.Models;

public sealed class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _bonus;

    public int TargetCount { get => _targetCount; init => _targetCount = value; }
    public int Bonus { get => _bonus; init => _bonus = value; }

    public override bool IsComplete => TimesCompleted >= TargetCount;

    public override int Record()
    {
        if (IsComplete) return 0;
        TimesCompleted++;
        return Points + (IsComplete ? Bonus : 0);
    }

    public override string Display()
        => $"[{(IsComplete ? "X" : " ")}] {Name} ({TimesCompleted}/{TargetCount}) " +
           $"+{Points} each{(IsComplete ? $", +{Bonus} bonus" : "")}";

    public override string Serialize() => base.Serialize() + $"|{_targetCount}|{_bonus}";
    public override void Deserialize(string[] parts)
    {
        base.Deserialize(parts);
        _targetCount = int.Parse(parts[6]);
        _bonus = int.Parse(parts[7]);
    }
}
