# Eternal Quest — W06 Developer Milestone

## Progress Update
I’m building the Foundation 4 version (Eternal Quest – Goal Tracker). So far, I’ve implemented the full class hierarchy (Goal abstract; SimpleGoal, EternalGoal, ChecklistGoal), the GoalService manager, FileStorage (text save/load), JsonStorage (export/import), and the App/UI with a menu. The app runs in the terminal, supports creating/listing/recording goals, shows total points, and saves/loads to text and JSON. I added input validation, a progress bar for checklist goals, and auto-load on start / auto-save on quit, plus a guard to prevent exporting an empty list. Remaining work is minor polish (extra validation/sorting).

## How to Run
dotnet run

## Menu
1 Create • 2 List • 3 Record • 4 Save(txt) • 5 Load(txt) • 6 Export(JSON) • 7 Import(JSON) • 0 Quit
