namespace Notissimus.Domain.Entities;

public class Offer
{
    private readonly List<OfferProperty> _additionalProperties;

    public Offer()
    {
        PrimaryKey = Guid.NewGuid();
        _additionalProperties = new List<OfferProperty>();
    }

    public Guid PrimaryKey { get; protected set; }

    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public int Bid { get; set; }
    public bool Available { get; set; }
    public string Url { get; set; } = null!;
    public int Price { get; set; }
    public string CurrencyId { get; set; } = null!;
    public int CategoryId { get; set; }
    public string Picture { get; set; } = null!;
    public string Description { get; set; } = null!;

    public IReadOnlyCollection<OfferProperty> AdditionalProperties => _additionalProperties;

    public void AddProperty(OfferProperty property)
    {
        OfferProperty? sameNameProperty = _additionalProperties
            .FirstOrDefault(p => p.Name == property.Name);

        if (sameNameProperty is not null)
            _additionalProperties.Remove(sameNameProperty);

        _additionalProperties.Add(property);
    }

    public void RemoveProperty(string name)
    {
        _additionalProperties.RemoveAll(p => p.Name == name);
    }
}