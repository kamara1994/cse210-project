using System;

public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int? _endVerse; // null => single verse

    // Single-verse constructor
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = null;
    }

    // Range constructor
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        if (endVerse < startVerse) throw new ArgumentException("End verse < start verse.");
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        return _endVerse.HasValue
            ? $"{_book} {_chapter}:{_startVerse}-{_endVerse.Value}"
            : $"{_book} {_chapter}:{_startVerse}";
    }

    // Helper for scriptures.txt parsing: Chapter="3", VerseOrRange="5" or "5-6"
    public static Reference FromParts(string book, string chapterStr, string verseOrRange)
    {
        if (!int.TryParse(chapterStr, out int chapter)) throw new FormatException("Invalid chapter.");
        if (verseOrRange.Contains('-'))
        {
            var p = verseOrRange.Split('-', 2, StringSplitOptions.TrimEntries);
            return new Reference(book, chapter, int.Parse(p[0]), int.Parse(p[1]));
        }
        return new Reference(book, chapter, int.Parse(verseOrRange));
    }
}
