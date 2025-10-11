using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    public Reference Reference { get; }

    private readonly List<Word> _words;
    private readonly List<int> _countableIndices;
    private readonly Stack<int> _availableIndices;
    private readonly Random _rng = new Random();
    private int _hiddenCount;

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        _words = text.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(t => new Word(t))
                     .ToList();

        _countableIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (_words[i].IsCountable()) _countableIndices.Add(i);
        }

        Shuffle(_countableIndices);
        _availableIndices = new Stack<int>(_countableIndices);
        _hiddenCount = 0;
    }

    public int TotalCount => _countableIndices.Count;
    public int HiddenCount => _hiddenCount;
    public double PercentComplete => TotalCount == 0 ? 0 : (HiddenCount * 100.0 / TotalCount);
    public bool IsCompletelyHidden => TotalCount > 0 && _hiddenCount == TotalCount;

    public string GetDisplayText()
    {
        return string.Join(" ", _words.Select(w => w.Display));
    }

    public void HideRandomWords(int count)
    {
        if (_availableIndices.Count == 0 || count <= 0) return;
        count = Math.Min(count, _availableIndices.Count);

        for (int k = 0; k < count; k++)
        {
            int idx = _availableIndices.Pop(); // already randomized
            if (!_words[idx].IsHidden)
            {
                _words[idx].Hide();
                _hiddenCount++;
            }
        }
    }

    public void RevealOneHiddenWord()
    {
        var hidden = new List<int>();
        foreach (var i in _countableIndices)
        {
            if (_words[i].IsHidden) hidden.Add(i);
        }
        if (hidden.Count == 0) return;

        int pick = hidden[_rng.Next(hidden.Count)];
        _words[pick].Reveal();
        if (_hiddenCount > 0) _hiddenCount--;
        _availableIndices.Push(pick);
    }

    private void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
