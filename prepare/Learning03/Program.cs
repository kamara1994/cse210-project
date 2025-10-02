using System;
using Learning03;

// Helper to print both representations
static void Print(Fraction f)
{
    Console.WriteLine(f.GetFractionString());
    Console.WriteLine(f.GetDecimalValue());
}

// Verify all three constructors + examples from the spec
var f1 = new Fraction();       // 1/1
var f2 = new Fraction(5);      // 5/1
var f3 = new Fraction(3, 4);   // 3/4
var f4 = new Fraction(1, 3);   // 1/3

Print(f1);
Print(f2);
Print(f3);
Print(f4);

// (Optional) Demonstrate getters/setters without affecting sample output:
// f3.SetTop(6);
// f3.SetBottom(7);
// Print(f3);
