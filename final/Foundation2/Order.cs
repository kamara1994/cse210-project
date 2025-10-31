using System.Collections.Generic;
using System.Linq;

namespace Foundation2;

public class Order
{
    private readonly List<Product> _items = new();
    public Address ShipTo { get; }

    public Order(Address shipTo) => ShipTo = shipTo;

    public void Add(Product p) => _items.Add(p);

    public string PackingLabel()
        => "PACKING LABEL:\n" + string.Join("\n", _items.Select(i => i.PackingLine()));

    public string ShippingLabel()
        => "SHIPPING LABEL:\n" + ShipTo.ToString();

    public decimal Total() => _items.Sum(i => i.LineTotal);
}
