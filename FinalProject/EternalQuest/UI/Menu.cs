namespace EternalQuest.UI;

public class Menu
{
    public int Show()
    {
        Console.WriteLine();
        Console.WriteLine("=== Eternal Quest ===");
        Console.WriteLine("1) Create Goal");
        Console.WriteLine("2) List Goals");
        Console.WriteLine("3) Record Event");
        Console.WriteLine("4) Save");
        Console.WriteLine("5) Load");
        Console.WriteLine("6) Export JSON");        Console.WriteLine("7) Import JSON");        Console.WriteLine("0) Quit");
        Console.Write("Choose: ");
        int.TryParse(Console.ReadLine(), out var choice);
        return choice;
    }
}
