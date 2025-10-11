using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new();
    private List<string> _questions = new();
    private Random _rng = new();

    public ReflectionActivity(int duration)
        : base("Reflection Activity",
               "This activity will help you reflect on times you’ve shown strength and resilience.",
               duration)
    {
        LoadPrompts();
        LoadQuestions();
    }

    protected override void Run()
    {
        Console.WriteLine($"\nPrompt: {GetRandomPrompt()}");
        Console.WriteLine("Think about this for a few seconds...\n");
        Spinner(3);

        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.WriteLine($"→ {_questions[i % _questions.Count]}");
            Spinner(4);
            i++;
        }
    }

    private void LoadPrompts()
    {
        _prompts.Add("Think of a time you helped someone in need.");
        _prompts.Add("Think of a moment you overcame a challenge.");
    }

    private void LoadQuestions()
    {
        _questions.Add("Why was this experience meaningful?");
        _questions.Add("What did you learn from it?");
        _questions.Add("How can you apply that lesson now?");
    }

    private string GetRandomPrompt()
    {
        return _prompts[_rng.Next(_prompts.Count)];
    }
}
