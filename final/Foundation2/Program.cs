using System;

namespace Foundation2;

internal class Program
{
    static void Main()
    {
        // Order 1
        var addr1 = new Address("Joseph Allan Kamara", "123 Center St", "Rexburg, ID", "ID", "USA");
        var o1 = new Order(addr1);
        o1.Add(new Product("USB-C Cable", "A100", 2, 4.99m));
        o1.Add(new Product("Notebook", "B220", 5, 2.50m));

        // Order 2
        var addr2 = new Address("Kandeh Sesay", "12 Lumley Rd", "Freetown", "W Area", "Sierra Leone");
        var o2 = new Order(addr2);
        o2.Add(new Product("Headphones", "H900", 1, 39.99m));
        o2.Add(new Product("Charger", "C330", 1, 19.99m));

        PrintOrder(o1);
        Console.WriteLine();
        PrintOrder(o2);
    }

    static void PrintOrder(Order o)
    {
        Console.WriteLine(o.PackingLabel());
        Console.WriteLine();
        Console.WriteLine(o.ShippingLabel());
        Console.WriteLine();
        Console.WriteLine("TOTAL: " + o.Total().ToString("C"));
    }
}
