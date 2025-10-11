using System;

public class Word
{
    private string _text;
    private bool _hidden;

    public Word(string text)
    {
        _text = text;
        _hidden = false;
    }

    public bool IsHidden => _hidden;

    public void Hide()   { _hidden = true;  }
    public void Reveal() { _hidden = false; }

    public string Display
    {
        get
        {
            if (!_hidden) return _text;
            // Replace only letters/digits; keep punctuation
            char[] arr = _text.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (char.IsLetterOrDigit(arr[i])) arr[i] = '_';
            }
            return new string(arr);
        }
    }

    // Countable = has at least one letter or digit
    public bool IsCountable()
    {
        foreach (char c in _text)
        {
            if (char.IsLetterOrDigit(c)) return true;
        }
        return false;
    }
}
