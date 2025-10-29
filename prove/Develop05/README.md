# Develop05 — Eternal Quest (Polymorphism)

Menu-driven goal tracker with:
- **SimpleGoal** (one-and-done)
- **ChecklistGoal** (N times + bonus at completion)
- **EternalGoal** (repeatable, no completion)

Features:
- Running total **Score** shown on menu
- **Save/Load** (JSON / CSV / TXT)
- Clear separation of **Models / Services / Storage / UI**

OOP Mapping:
- **Abstraction:** GoalService, IStorage boundary
- **Encapsulation:** private fields, behavior via methods
- **Inheritance:** Goal → Simple / Checklist / Eternal
- **Polymorphism:** Goal.Record() overridden per type

## Run
dotnet restore
dotnet build
dotnet run
