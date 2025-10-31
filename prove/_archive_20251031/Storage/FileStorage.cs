using System.Globalization;
using EternalQuest.Models;

namespace EternalQuest.Storage;

public class FileStorage : IStorage
{
    private static string Escape(string s)
        => s.Replace("\\", "\\\\").Replace("|", "\\|");

    private static string Unescape(string s)
    {
        var result = new System.Text.StringBuilder();
        bool esc = false;
        foreach (var ch in s)
        {
            if (esc) { result.Append(ch); esc = false; }
            else if (ch == '\\') esc = true;
            else result.Append(ch);
        }
        return result.ToString();
    }

    public void Save(IEnumerable<Goal> goals, string path)
    {
        // NOTE: The SCORE header is written by App.SaveTxt() before calling this.
        var lines = new List<string>();
        foreach (var g in goals)
        {
            switch (g)
            {
                case SimpleGoal sg:
                    lines.Add(string.Join("|", new[]
                    {
                        "Simple",
                        sg.Id.ToString(CultureInfo.InvariantCulture),
                        Escape(sg.Name),
                        Escape(sg.Description),
                        sg.Points.ToString(CultureInfo.InvariantCulture),
                        sg.Completed ? "1" : "0"
                    }));
                    break;

                case EternalGoal eg:
                    lines.Add(string.Join("|", new[]
                    {
                        "Eternal",
                        eg.Id.ToString(CultureInfo.InvariantCulture),
                        Escape(eg.Name),
                        Escape(eg.Description),
                        eg.Points.ToString(CultureInfo.InvariantCulture)
                    }));
                    break;

                case ChecklistGoal cg:
                    lines.Add(string.Join("|", new[]
                    {
                        "Checklist",
                        cg.Id.ToString(CultureInfo.InvariantCulture),
                        Escape(cg.Name),
                        Escape(cg.Description),
                        cg.Points.ToString(CultureInfo.InvariantCulture),
                        cg.Completed ? "1" : "0",
                        cg.Target.ToString(CultureInfo.InvariantCulture),
                        cg.Count.ToString(CultureInfo.InvariantCulture),
                        cg.Bonus.ToString(CultureInfo.InvariantCulture)
                    }));
                    break;
            }
        }
        System.IO.File.WriteAllLines(path, lines);
    }

    public List<Goal> Load(string path)
    {
        var list = new List<Goal>();
        if (!System.IO.File.Exists(path)) return list;

        var lines = System.IO.File.ReadAllLines(path);
        foreach (var raw in lines)
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;
            if (raw.StartsWith("SCORE|")) continue; // App.LoadTxt() handles the score; skip here

            // split on unescaped '|'
            var parts = new List<string>();
            bool esc = false;
            var cur = new System.Text.StringBuilder();
            foreach (var ch in raw)
            {
                if (esc) { cur.Append(ch); esc = false; }
                else if (ch == '\\') esc = true;
                else if (ch == '|') { parts.Add(cur.ToString()); cur.Clear(); }
                else cur.Append(ch);
            }
            parts.Add(cur.ToString());

            if (parts.Count == 0) continue;
            var kind = parts[0];
            switch (kind)
            {
                case "Simple":
                {
                    var g = new SimpleGoal
                    {
                        Id = int.Parse(parts[1]),
                        Name = Unescape(parts[2]),
                        Description = Unescape(parts[3]),
                        Points = int.Parse(parts[4]),
                        Completed = parts[5] == "1"
                    };
                    list.Add(g);
                    break;
                }
                case "Eternal":
                {
                    var g = new EternalGoal
                    {
                        Id = int.Parse(parts[1]),
                        Name = Unescape(parts[2]),
                        Description = Unescape(parts[3]),
                        Points = int.Parse(parts[4])
                    };
                    list.Add(g);
                    break;
                }
                case "Checklist":
                {
                    var g = new ChecklistGoal
                    {
                        Id = int.Parse(parts[1]),
                        Name = Unescape(parts[2]),
                        Description = Unescape(parts[3]),
                        Points = int.Parse(parts[4]),
                        Completed = parts[5] == "1",
                        Target = int.Parse(parts[6]),
                        Count = int.Parse(parts[7]),
                        Bonus = int.Parse(parts[8])
                    };
                    list.Add(g);
                    break;
                }
            }
        }
        return list;
    }
}
