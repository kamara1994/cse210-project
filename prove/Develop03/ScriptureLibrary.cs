using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScriptureLibrary
{
    private readonly List<(Reference Ref, string Text)> _items = new();
    private readonly Random _rng = new();

    public void SeedDefaults()
    {
        _items.Add((new Reference("John", 3, 16),
            "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."));
        _items.Add((new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."));
        _items.Add((new Reference("Mosiah", 2, 17),
            "When ye are in the service of your fellow beings ye are only in the service of your God."));
    }

    // Optional file: scriptures.txt
    // Format per line: Book|Chapter|VerseOrRange|Text
    public void TryLoadFromFile(string path)
    {
        if (!File.Exists(path)) return;
        foreach (var line in File.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split('|');
            if (parts.Length < 4) continue;
            var reference = Reference.FromParts(parts[0].Trim(), parts[1].Trim(), parts[2].Trim());
            var text = string.Join("|", parts.Skip(3)).Trim(); // keep '|' inside text
            _items.Add((reference, text));
        }
    }

    public Scripture GetRandomScripture()
    {
        if (_items.Count == 0)
        {
            var r = new Reference("John", 3, 16);
            return new Scripture(r, "For God so loved the world...");
        }
        var pick = _items[_rng.Next(_items.Count)];
        return new Scripture(pick.Ref, pick.Text);
    }
}
