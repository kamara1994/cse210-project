using System;
using System.Linq;
using EternalQuest.Models;
using EternalQuest.Services;

namespace EternalQuestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var svc = new GoalService();
            const string savePath = "goals.csv";

            while (true)
            {
                Console.WriteLine();
                var points = svc.TotalPoints();
                var level  = points / 500; // Level every 500 pts
                Console.WriteLine($"Total Points: {points}   |   Level: {level}");
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
                        CreateGoal(svc);
                        break;

                    case "3":
                        Console.Write("Enter goal id: ");
                        if (int.TryParse(Console.ReadLine(), out var id))
                        {
                            int earned = svc.Record(id);
                            Console.WriteLine(earned > 0
                                ? $"You earned {earned} pts!"
                                : "No points (already complete?).");
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

                    case "0":
                        return;
                }
            }
        }

        private static void CreateGoal(GoalService svc)
        {
            Console.WriteLine("Type: 1) Simple  2) Eternal  3) Checklist");
            var t = Console.ReadLine();
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Points each time: ");
            int.TryParse(Console.ReadLine(), out var pts);
            Console.Write("Notes (optional): ");
            var notes = Console.ReadLine() ?? "";

            Goal g = t switch
            {
                "1" => new SimpleGoal { Id = svc.NextId(), Name = name, Points = pts, Notes = notes },
                "2" => new EternalGoal { Id = svc.NextId(), Name = name, Points = pts, Notes = notes },
                "3" => MakeChecklist(svc, name, pts, notes),
                _ => throw new InvalidOperationException("Invalid type")
            };

            svc.Add(g);
            Console.WriteLine("Created.");
        }

        private static Goal MakeChecklist(GoalService svc, string name, int pts, string notes)
        {
            Console.Write("Target count: ");
            int.TryParse(Console.ReadLine(), out var target);
            Console.Write("Bonus when complete: ");
            int.TryParse(Console.ReadLine(), out var bonus);
            return new ChecklistGoal
            {
                Id = svc.NextId(),
                Name = name,
                Points = pts,
                Notes = notes,
                TargetCount = target,
                Bonus = bonus
            };
        }
    }
}
