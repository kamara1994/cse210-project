using System;
using System.Collections.Generic;
using System.Linq;

namespace Develop03
{
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private readonly Random _rand = new Random();

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                         .Select(t => new Word(t))
                         .ToList();
        }

        public string GetDisplayText()
        {
            var parts = _words.Select(w => w.GetDisplayText());
            return $"{_reference.GetDisplayText()}\n{string.Join(" ", parts)}";
        }

        public void HideRandomWords(int count)
        {
            var available = new List<int>();
            for (int i = 0; i < _words.Count; i++)
            {
                if (!_words[i].IsHidden()) available.Add(i);
            }

            for (int n = 0; n < count && available.Count > 0; n++)
            {
                int pick = _rand.Next(available.Count);
                int idx = available[pick];
                _words[idx].Hide();
                available.RemoveAt(pick);
            }
        }

        public bool IsCompletelyHidden()
        {
            foreach (var w in _words)
            {
                if (!w.IsHidden()) return false;
            }
            return true;
        }
    }
}
