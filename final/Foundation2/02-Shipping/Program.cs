using System;
using System.Collections.Generic;

class Address
{
    public string Street, City, StateProvince, Country;
    public Address(string street, string city, string stateProv, string country)
    { Street = street; City = city; StateProvince = stateProv; Country = country; }
    public bool IsUSA() => Country.Trim().Equals("USA", StringComparison.OrdinalIgnoreCase);
    public override string ToString() => $"{Street}\n{City}, {StateProvince}\n{Country}";
}
class Customer
{
    public string Name { get; }
    public Address Address { get; }
    public Customer(string name, Address address) { Name = name; Address = address; }
}
class Product
{
    public string Name { get; }
    public string Id { get; }
    public decimal UnitPrice { get; }
    public int Qty { get; }
    public Product(string name, string id, decimal unitPrice, int qty)
    { Name = name; Id = id; UnitPrice = unitPrice; Qty = qty; }
    public decimal Subtotal() => UnitPrice * Qty;
}
class Order
{
    public Customer Customer { get; }
    private readonly List<Product> _items = new();
    public Order(Customer customer) { Customer = customer; }
    public void Add(Product p) => _items.Add(p);
    public string PackingLabel()
    {
        var lines = new List<string> { "PACKING LABEL:" };
        foreach (var p in _items) lines.Add($" - {p.Name} (#{p.Id}) x{p.Qty}");
        return string.Join("\n", lines);
    }
    public string ShippingLabel() => $"SHIPPING LABEL:\n{Customer.Name}\n{Customer.Address}";
    public decimal Total()
    {
        decimal items = 0m;
        foreach (var p in _items) items += p.Subtotal();
        decimal shipping = Customer.Address.IsUSA() ? 5m : 35m;
        return items + shipping;
    }
}
class Program
{
    static void Main()
    {
        var o1 = new Order(new Customer("Joseph Allan Kamara",
            new Address("123 Center St", "Rexburg", "ID", "USA")));
        o1.Add(new Product("USB-C Cable","A100", 7.50m, 2));
        o1.Add(new Product("Notebook","B220", 3.25m, 5));

        var o2 = new Order(new Customer("Kandeh Sesay",
            new Address("12 Lumley Rd", "Freetown", "W Area", "Sierra Leone")));
        o2.Add(new Product("Headphones","H900", 29.99m, 1));
        o2.Add(new Product("Charger","C330", 15.00m, 1));

        Print(o1); Console.WriteLine(); Print(o2);

        static void Print(Order o)
        {
            Console.WriteLine(o.PackingLabel());
            Console.WriteLine();
            Console.WriteLine(o.ShippingLabel());
            Console.WriteLine($"\nTOTAL: ");
        }
    }
}
