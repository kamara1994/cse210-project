using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

public class Journal
{
    private readonly List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\n(No entries yet.)\n");
            return;
        }

        Console.WriteLine("\n— Journal —\n");
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry.ToDisplayString());
            Console.WriteLine(new string('-', 60));
        }
    }

    public void SaveToFile(string path)
    {
        try
        {
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            foreach (var e in _entries)
            {
                writer.WriteLine(e.ToFileLine());
            }
            Console.WriteLine($"Saved {_entries.Count} entries to '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return;
            }

            var lines = File.ReadAllLines(path, Encoding.UTF8);
            _entries.Clear();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                _entries.Add(Entry.FromFileLine(line));
            }
            Console.WriteLine($"Loaded {_entries.Count} entries from '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    // JSON save (extra feature)
    public void SaveToJson(string path)
    {
        try
        {
            var dtoList = _entries.Select(e => new EntryDto
            {
                Date = e.Date,
                Prompt = e.Prompt,
                Content = e.Content,
                Mood = e.Mood,
                Tags = e.Tags.ToList()
            }).ToList();

            var opts = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(dtoList, opts);
            File.WriteAllText(path, json, Encoding.UTF8);
            Console.WriteLine($"Saved {_entries.Count} entries to JSON '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving JSON: {ex.Message}");
        }
    }

    // JSON load (extra feature)
    public void LoadFromJson(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("JSON file not found.");
                return;
            }

            var json = File.ReadAllText(path, Encoding.UTF8);
            var dtoList = JsonSerializer.Deserialize<List<EntryDto>>(json) ?? new List<EntryDto>();
            _entries.Clear();
            foreach (var d in dtoList)
            {
                _entries.Add(new Entry(
                    d.Date ?? string.Empty,
                    d.Prompt ?? string.Empty,
                    d.Content ?? string.Empty,
                    d.Mood ?? string.Empty,
                    d.Tags ?? new List<string>()
                ));
            }
            Console.WriteLine($"Loaded {_entries.Count} entries from JSON '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON: {ex.Message}");
        }
    }

    // Search feature (extra)
    public List<Entry> FindByKeyword(string keyword)
    {
        keyword = keyword?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(keyword)) return new List<Entry>();

        return _entries
            .Where(e => e.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                     || e.Prompt.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // Helper DTO for JSON
    private class EntryDto
    {
        public string? Date { get; set; }
        public string? Prompt { get; set; }
        public string? Content { get; set; }
        public string? Mood { get; set; }
        public List<string>? Tags { get; set; }
    }
}
