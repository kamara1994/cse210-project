namespace Foundation2;

public class Product
{
    public string Name { get; }
    public string Id { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public Product(string name, string id, int quantity, decimal unitPrice)
    {
        Name = name;
        Id = id;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal LineTotal => Quantity * UnitPrice;

    public string PackingLine() => $" - {Name} (#{Id}) x{Quantity}";
}
