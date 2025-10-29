namespace EternalQuest.Models;

public sealed class EternalGoal : Goal
{
    public override bool IsComplete => false;        // never completes
    public override int Record() { TimesCompleted++; return Points; }
    public override string Display() =>
        $"[ ] {Name} (Eternal, {Points} pts each, done {TimesCompleted}x)";
}
