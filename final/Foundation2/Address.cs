namespace Foundation2;

public class Address
{
    public string Name { get; }
    public string Street { get; }
    public string City { get; }
    public string StateOrRegion { get; }
    public string Country { get; }

    public Address(string name, string street, string city, string stateOrRegion, string country)
    {
        Name = name;
        Street = street;
        City = city;
        StateOrRegion = stateOrRegion;
        Country = country;
    }

    public bool IsUSA()
    {
        return Country.Trim().ToUpper() == "USA";
    }

    public override string ToString()
        => $"{Name}\n{Street}\n{City}, {StateOrRegion}\n{Country}";
}
