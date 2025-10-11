// CSE 210 – Learning04: Inheritance Activity
// Demonstrates Assignment, MathAssignment, and WritingAssignment

using System;

class Program
{
    static void Main()
    {
        // Base class test
        var a = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(a.GetSummary());

        Console.WriteLine();

        // Math assignment test
        var m = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(m.GetSummary());
        Console.WriteLine(m.GetHomeworkList());

        Console.WriteLine();

        // Writing assignment test
        var w = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(w.GetSummary());
        Console.WriteLine(w.GetWritingInformation());
    }
}
