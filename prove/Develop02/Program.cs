// EXCEEDS REQUIREMENTS:
// - Save/Load JSON
// - Search entries by keyword
// - (optional) mood/tags for entries
// Sources (if any): <add links here>
// CSE 210 – Develop02: Journal Program
// Author: <Joseph Allan Kamara>
// Notes (Exceeds Requirements):
// - JSON save/load (menu 5 & 6)
// - Keyword search (menu 7)
// - Optional mood & tags on each entry

using System;
using System.Linq;
using System.Text;

public class Program
{
    private static readonly Journal _journal = new Journal();
    private static readonly PromptGenerator _promptGenerator = new PromptGenerator();

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        ShowWelcome();

        bool running = true;
        while (running)
        {
            PrintMenu();
            Console.Write("Select an option (1-8): ");
            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;

                case "2":
                    _journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter filename to SAVE (e.g., journal.txt): ");
                    var savePath = Console.ReadLine() ?? "journal.txt";
                    _journal.SaveToFile(savePath);
                    break;

                case "4":
                    Console.Write("Enter filename to LOAD (e.g., journal.txt): ");
                    var loadPath = Console.ReadLine() ?? "journal.txt";
                    _journal.LoadFromFile(loadPath);
                    break;

                case "5":
                    Console.Write("Enter JSON filename to SAVE (e.g., journal.json): ");
                    var saveJson = Console.ReadLine() ?? "journal.json";
                    _journal.SaveToJson(saveJson);
                    break;

                case "6":
                    Console.Write("Enter JSON filename to LOAD (e.g., journal.json): ");
                    var loadJson = Console.ReadLine() ?? "journal.json";
                    _journal.LoadFromJson(loadJson);
                    break;

                case "7":
                    Console.Write("Keyword to search: ");
                    var kw = Console.ReadLine() ?? string.Empty;
                    var results = _journal.FindByKeyword(kw);
                    if (results.Count == 0)
                    {
                        Console.WriteLine("No matches found.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Found {results.Count} matching entries:\n");
                        foreach (var e in results)
                        {
                            Console.WriteLine(e.ToDisplayString());
                            Console.WriteLine(new string('-', 60));
                        }
                    }
                    break;

                case "8":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }

    private static void ShowWelcome()
    {
        Console.WriteLine("====================================");
        Console.WriteLine("       Journal Program – Develop02   ");
        Console.WriteLine("====================================\n");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("1) Write a new entry");
        Console.WriteLine("2) Display the journal");
        Console.WriteLine("3) Save the journal to a file (text)");
        Console.WriteLine("4) Load the journal from a file (text)");
        Console.WriteLine("5) Save the journal to JSON (extra)");
        Console.WriteLine("6) Load the journal from JSON (extra)");
        Console.WriteLine("7) Search entries by keyword (extra)");
        Console.WriteLine("8) Quit\n");
    }

    private static void WriteNewEntry()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        string prompt = _promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string content = Console.ReadLine() ?? string.Empty;

        Console.Write("Optional mood (press Enter to skip): ");
        string mood = Console.ReadLine() ?? string.Empty;

        Console.Write("Optional tags (comma-separated, e.g., home,school): ");
        string tagsLine = Console.ReadLine() ?? string.Empty;
        var tags = tagsLine
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim())
            .Where(t => t.Length > 0)
            .ToList();

        var entry = new Entry(today, prompt, content, mood, tags);
        _journal.AddEntry(entry);
        Console.WriteLine("\nEntry added!\n");
    }
}

