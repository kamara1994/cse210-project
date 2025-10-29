namespace EternalQuest.Models;

public sealed class SimpleGoal : Goal
{
    private bool _done;

    public override bool IsComplete => _done;

    public override int Record()
    {
        if (_done) return 0;
        _done = true;
        TimesCompleted++;
        return Points;
    }

    public override string Display() =>
        $"[{(_done ? "X" : " ")}] {Name} ({Points} pts)";

    public override string Serialize() => base.Serialize() + $"|{_done}";
    public override void Deserialize(string[] parts)
    {
        base.Deserialize(parts);
        _done = bool.Parse(parts[6]);
    }
}
