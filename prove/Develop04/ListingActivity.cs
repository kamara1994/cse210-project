using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private string _topic;
    private List<string> _items = new();

    public ListingActivity(int duration, string topic)
        : base("Listing Activity",
               "This activity helps you reflect by listing as many items as you can.",
               duration)
    {
        _topic = topic;
    }

    protected override void Run()
    {
        Console.WriteLine($"List as many items as you can about: {_topic}");
        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                _items.Add(input);
        }
        Console.WriteLine($"\nYou listed {_items.Count} items!");
    }
}
