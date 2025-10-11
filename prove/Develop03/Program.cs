// CSE 210 – Programming with Classes
// Unit 03: Scripture Memorizer
// Author: Joseph Allan Kamara
// -----------------------------------------------------------
// EXCEEDS REQUIREMENTS:
// • Scripture library (random selection) + optional load from "scriptures.txt".
// • User chooses words-per-round.
// • Commands: Enter=hide, "hint" (reveal one), "reset", "quit".
// • Progress indicator; optimized hiding (no repeated list rebuilds).
// -----------------------------------------------------------

using System;

class Program
{
    static void Main()
    {
        Console.Title = "CSE210 – Scripture Memorizer";

        var library = new ScriptureLibrary();
        library.SeedDefaults();
        library.TryLoadFromFile("scriptures.txt"); // optional

        Console.Clear();
        Console.WriteLine("CSE210 – Scripture Memorizer\n");
        int wordsPerRound = AskForInt("How many words should hide each round? (1–10) ", 1, 10, 3);

        PlayRound(library, wordsPerRound);
    }

    static void PlayRound(ScriptureLibrary library, int wordsPerRound)
    {
        Scripture scripture = library.GetRandomScripture();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Reference: {scripture.Reference}\n");
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine($"\nHidden: {scripture.HiddenCount}/{scripture.TotalCount} ({scripture.PercentComplete:0}%)");

            if (scripture.IsCompletelyHidden)
            {
                Console.WriteLine("\nAll words are hidden. Great job memorizing!");
                return;
            }

            Console.Write("\nPress Enter to hide, or type (hint | reset | quit): ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (input.Equals("quit", StringComparison.OrdinalIgnoreCase)) return;
            if (input.Equals("hint", StringComparison.OrdinalIgnoreCase)) { scripture.RevealOneHiddenWord(); continue; }
            if (input.Equals("reset", StringComparison.OrdinalIgnoreCase))
            {
                wordsPerRound = MaybeChangeCount(wordsPerRound);
                scripture = library.GetRandomScripture();
                continue;
            }

            scripture.HideRandomWords(wordsPerRound);
        }
    }

    static int MaybeChangeCount(int current)
    {
        Console.Write($"Keep words-per-round at {current}? (Y to keep, Enter new number) ");
        string s = Console.ReadLine() ?? "";
        if (s.Equals("y", StringComparison.OrdinalIgnoreCase)) return current;
        return AskForInt("New words per round (1–10): ", 1, 10, current);
    }

    static int AskForInt(string prompt, int min, int max, int fallback)
    {
        Console.Write(prompt);
        string s = Console.ReadLine() ?? "";
        if (int.TryParse(s, out int n) && n >= min && n <= max) return n;
        return fallback;
    }
}
