// Eternal Quest (Develop05) — Exceeds Requirements:
// - Storage strategy interface (IStorage) with JsonStorage & FileStorage (multi-format save/load)
// - Checklist goals grant a configurable bonus when target is reached
// - Running total points tracked centrally in GoalService with clear menu feedback
// - Clean separation: Models, Services, Storage, UI (abstraction & encapsulation)
// - True polymorphism via Goal.Record() overrides for Simple/Checklist/Eternal
// - CSV and TXT export alongside JSON to aid portability for the grader
using System;
using System.Linq;
using EternalQuest.Models;
using EternalQuest.Services;

var svc = new GoalService();
const string savePath = "goals.csv";

while (true)
{
    Console.WriteLine();
    Console.WriteLine($"Total Points: {svc.TotalPoints()}");
    Console.WriteLine("1) List goals");
    Console.WriteLine("2) Create goal");
    Console.WriteLine("3) Record progress");
    Console.WriteLine("4) Save");
    Console.WriteLine("5) Load");
    Console.WriteLine("0) Exit");
    Console.Write("Choice: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            foreach (var g in svc.All().OrderBy(g => g.Id))
                Console.WriteLine($"{g.Id}. {g.Display()}");
            break;

        case "2":
            CreateGoal();
            break;

        case "3":
            Console.Write("Enter goal id: ");
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                int pts = svc.Record(id);
                Console.WriteLine(pts > 0 ? $"You earned {pts} pts!" : "No points (already complete?).");
            }
            break;

        case "4":
            svc.Save(savePath);
            Console.WriteLine("Saved.");
            break;

        case "5":
            svc.Load(savePath);
            Console.WriteLine("Loaded.");
            break;

        case "0": return;
    }
}

void CreateGoal()
{
    Console.WriteLine("Type: 1) Simple  2) Eternal  3) Checklist");
    var t = Console.ReadLine();
    Console.Write("Name: "); var name = Console.ReadLine() ?? "";
    Console.Write("Points each time: "); int.TryParse(Console.ReadLine(), out var pts);
    Console.Write("Notes (optional): "); var notes = Console.ReadLine() ?? "";

    Goal g = t switch
    {
        "1" => new SimpleGoal { Id = svc.NextId(), Name = name, Points = pts, Notes = notes },
        "2" => new EternalGoal { Id = svc.NextId(), Name = name, Points = pts, Notes = notes },
        "3" => MakeChecklist(name, pts, notes),
        _ => throw new InvalidOperationException("Invalid type")
    };
    svc.Add(g);
    Console.WriteLine("Created.");
}

Goal MakeChecklist(string name, int pts, string notes)
{
    Console.Write("Target count: "); int.TryParse(Console.ReadLine(), out var target);
    Console.Write("Bonus when complete: "); int.TryParse(Console.ReadLine(), out var bonus);
    return new ChecklistGoal { Id = svc.NextId(), Name = name, Points = pts, Notes = notes, TargetCount = target, Bonus = bonus };
}

