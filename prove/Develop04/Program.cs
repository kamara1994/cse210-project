using System;

class Program
{
    static void Main()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Mindfulness App!");
        Console.WriteLine("1) Breathing Activity");
        Console.WriteLine("2) Reflection Activity");
        Console.WriteLine("3) Listing Activity");
        Console.Write("Choose an activity: ");
        string choice = Console.ReadLine();

        Console.Write("Enter duration (seconds): ");
        int duration = int.Parse(Console.ReadLine() ?? "30");

        Activity activity = choice switch
        {
            "1" => new BreathingActivity(duration, 4, 6),
            "2" => new ReflectionActivity(duration),
            "3" => new ListingActivity(duration, "things you're grateful for"),
            _ => new BreathingActivity(duration, 4, 6)
        };

        activity.Start();
    }
}
