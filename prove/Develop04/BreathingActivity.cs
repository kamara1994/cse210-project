using System;
using System.Threading;

public class BreathingActivity : Activity
{
    private int _inhaleSeconds;
    private int _exhaleSeconds;

    public BreathingActivity(int duration, int inhaleSeconds, int exhaleSeconds)
        : base("Breathing Activity",
               "This activity will help you relax by guiding you through slow breathing.",
               duration)
    {
        _inhaleSeconds = inhaleSeconds;
        _exhaleSeconds = exhaleSeconds;
    }

    protected override void Run()
    {
        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            Console.Write("Breathe in... ");
            Countdown(_inhaleSeconds);
            Console.Write("Breathe out... ");
            Countdown(_exhaleSeconds);
        }
    }
}
