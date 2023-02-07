namespace Notissimus.Domain.Entities;

public class OfferProperty
{
    public OfferProperty(string name, string value)
    {
        Id = Guid.NewGuid();
        Name = name;
        Value = value;
    }

    public Guid Id { get; protected init; }
    
    public Offer Offer { get; protected init; } = null!;

    public string Name { get; protected init; }
    public string Value { get; protected init; }
}