using System;
using System.Collections.Generic;
using System.Linq;

public class Entry
{
    private const string Separator = "~|~"; // unlikely in normal text

    private string _date;      // e.g., "2025-09-27"
    private string _prompt;    // the question shown
    private string _content;   // user's response
    private string _mood;      // optional: e.g., "happy", "stressed"
    private List<string> _tags;// optional: e.g., ["school", "family"]

    public Entry(string date, string prompt, string content, string mood = "", IEnumerable<string>? tags = null)
    {
        _date = date;
        _prompt = prompt;
        _content = content;
        _mood = mood ?? string.Empty;
        _tags = tags?.ToList() ?? new List<string>();
    }

    // Public getters (read-only from outside)
    public string Date => _date;
    public string Prompt => _prompt;
    public string Content => _content;
    public string Mood => _mood;
    public IReadOnlyList<string> Tags => _tags;

    public string ToDisplayString()
    {
        var tagsJoined = _tags.Count > 0 ? $" [tags: {string.Join(", ", _tags)}]" : string.Empty;
        var moodPart = string.IsNullOrWhiteSpace(_mood) ? string.Empty : $" (mood: {_mood})";
        return $"Date: {_date}{moodPart}\nPrompt: {_prompt}\nResponse: {_content}{tagsJoined}";
    }

    // Serialize to text (for saving to file)
    public string ToFileLine()
    {
        string Escape(string s) => s.Replace("\r", " ").Replace("\n", " ").Replace(Separator, " ");
        string safeTags = string.Join(";", _tags.Select(Escape));
        return string.Join(Separator, new[] { Escape(_date), Escape(_prompt), Escape(_content), Escape(_mood), safeTags });
    }

    // Rebuild from file
    public static Entry FromFileLine(string line)
    {
        var parts = line.Split(Separator);
        string date = parts.Length > 0 ? parts[0] : string.Empty;
        string prompt = parts.Length > 1 ? parts[1] : string.Empty;
        string content = parts.Length > 2 ? parts[2] : string.Empty;
        string mood = parts.Length > 3 ? parts[3] : string.Empty;
        var tags = new List<string>();
        if (parts.Length > 4 && !string.IsNullOrWhiteSpace(parts[4]))
        {
            tags = parts[4].Split(';', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
        }
        return new Entry(date, prompt, content, mood, tags);
    }
}
