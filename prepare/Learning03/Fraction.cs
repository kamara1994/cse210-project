using System;

namespace Learning03
{
    public class Fraction
    {
        // Private attributes (encapsulation)
        private int _top;
        private int _bottom;

        // Constructors
        public Fraction()               // 1) 1/1
        {
            _top = 1;
            _bottom = 1;
        }

        public Fraction(int top)        // 2) top/1
        {
            _top = top;
            _bottom = 1;
        }

        public Fraction(int top, int bottom) // 3) top/bottom
        {
            if (bottom == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            _top = top;
            _bottom = bottom;
        }

        // Getters & Setters
        public int GetTop() => _top;
        public void SetTop(int top) => _top = top;

        public int GetBottom() => _bottom;
        public void SetBottom(int bottom)
        {
            if (bottom == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            _bottom = bottom;
        }

        // Representations
        public string GetFractionString() => $"{_top}/{_bottom}";
        public double GetDecimalValue() => (double)_top / _bottom;
    }
}
