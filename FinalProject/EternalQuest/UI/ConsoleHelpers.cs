namespace EternalQuest.UI;

public static class ConsoleHelpers
{
    public static int ReadInt(string prompt, int defaultValue = 0)
    {
        while (true)
        {
            Console.Write($"{prompt}");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            if (int.TryParse(s, out var v)) return v;
            Warn("Please enter a valid integer.");
        }
    }

    public static string ReadText(string prompt, bool required = false)
    {
        while (true)
        {
            Console.Write($"{prompt}");
            var s = Console.ReadLine() ?? string.Empty;
            s = s.Trim();
            if (!required || s.Length > 0) return s;
            Warn("This field is required.");
        }
    }

    public static void Info(string message)
    {
        var old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ForegroundColor = old;
    }

    public static void Success(string message)
    {
        var old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = old;
    }

    public static void Warn(string message)
    {
        var old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ForegroundColor = old;
    }

    public static void Error(string message)
    {
        var old = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = old;
    }
}
