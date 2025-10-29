# W06 Prove — Developer: Project Milestone

Project: Eternal Quest (Goal Tracker)
Location in repo: FinalProject/EternalQuest

What’s working now
- Class hierarchy: Goal (base), SimpleGoal, EternalGoal, ChecklistGoal
- Menu UI: create, list, record progress, save, load
- Points shown on menu
- Storage: JSON (goals.json) + CSV/TXT export
- Checklist progress + bonus on completion

OOP Mapping (rubric)
- Abstraction: IStorage (JsonStorage, FileStorage), GoalService
- Encapsulation: private fields, public methods, classes manage their own state
- Inheritance: Goal → Simple/Eternal/Checklist
- Polymorphism: Goal.Record() overridden in each derived class

How to run
dotnet run --project .\FinalProject\EternalQuest\EternalQuest.csproj
