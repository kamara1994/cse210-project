// EXCEEDS REQUIREMENTS:
// - Supports a small scripture library (random pick each run).
// - Hides only words not already hidden (no duplicates per round).
// - Clean loop: press Enter to hide more, or type "quit" to end.

using System;
using System.Collections.Generic;
using Develop03;

class Program
{
    static void Main()
    {
        Console.Title = "Develop03 - Scripture Memorizer";
        var rnd = new Random();

        var scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.")
        };

        var scripture = scriptures[rnd.Next(scriptures.Count)];
        int hidePerRound = 3;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("     Scripture Memorizer - Develop03");
            Console.WriteLine("====================================");
            Console.WriteLine();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to hide words, or type 'quit': ");
            var input = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (input == "quit")
                break;

            scripture.HideRandomWords(hidePerRound);

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine("All words are hidden. Great job!");
                Console.WriteLine();
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }
        }
    }
}
