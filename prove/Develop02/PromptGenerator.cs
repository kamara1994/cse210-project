using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class PromptGenerator
{
    private readonly Random _rng = new Random();

    // Curated list of prompts
    private readonly List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        // Extra custom prompts
        "What small win can I celebrate today?",
        "What challenged me today and what did I learn from it?",
        "Which moment today made me smile and why?",
        "List three things I’m grateful for right now.",
        "What habit will I improve tomorrow?",
        "What is one way I served someone today?",
        "What did I study or build today, and what is my next step?",
        "Where did I notice personal growth this week?"
    };

    public string GetRandomPrompt()
    {
        if (_prompts.Count == 0) return "Write anything on your mind today.";
        int i = _rng.Next(_prompts.Count);
        return _prompts[i];
    }

    // Optional: expose read-only prompts (for future feature)
    public ReadOnlyCollection<string> GetAllPrompts() => _prompts.AsReadOnly();
}
