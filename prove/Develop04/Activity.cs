using System;
using System.Collections.Generic;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    private int _durationSeconds;

    public Activity(string name, string description, int durationSeconds)
    {
        _name = name;
        _description = description;
        _durationSeconds = durationSeconds;
    }

    public void Start()
    {
        ShowIntro();
        Spinner(2);
        Run();
        ShowOutro();
    }

    protected virtual void Run() { }

    protected void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}!");
        Console.WriteLine(_description);
        Console.WriteLine($"Duration: {_durationSeconds} seconds\n");
    }

    protected void ShowOutro()
    {
        Console.WriteLine("\nWell done! You completed the activity!");
        Console.WriteLine($"You spent {_durationSeconds} seconds practicing mindfulness.");
        Spinner(3);
    }

    protected int GetDuration() => _durationSeconds;

    protected void Spinner(int seconds)
    {
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write("|");
            Thread.Sleep(250);
            Console.Write("\b/");
            Thread.Sleep(250);
            Console.Write("\b-");
            Thread.Sleep(250);
            Console.Write("\b\\");
            Thread.Sleep(250);
            Console.Write("\b");
        }
        Console.WriteLine();
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }
}
