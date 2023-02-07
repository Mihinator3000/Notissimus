using System.Reflection;
using System.Xml;
using Notissimus.Abstractions.Core;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;

namespace Notissimus.Core.Parsers;

public class XmlOfferParser : IXmlOfferParser
{
    private const StringComparison IgnoreCaseStringComparison = StringComparison.InvariantCultureIgnoreCase;
    private static readonly PropertyInfo[] OfferProperties = typeof(Offer).GetProperties();

    public Offer Parse(XmlNode offerNode)
    {
        var offer = new Offer();

        foreach (XmlAttribute attribute in offerNode.Attributes!)
            SetOfferProperty(offer, attribute.Name, attribute.Value);

        foreach (XmlElement element in offerNode.ChildNodes)
            SetOfferProperty(offer, element.Name, element.InnerText);

        return offer;
    }

    private static void SetOfferProperty(Offer offer, string name, string value)
    {
        var property = OfferProperties.FirstOrDefault(p =>
            p.Name.Equals(name, IgnoreCaseStringComparison));

        if (property is not null)
        {
            object? propertyValue = Convert.ChangeType(value, property.PropertyType);
            if (propertyValue is null)
                throw new XmlParseException($"Failed to parse property {name} with value {value}");

            property.SetValue(offer, propertyValue);
            return;
        }

        var additionalProperty = new OfferProperty(name, value);
        offer.AddProperty(additionalProperty);
    }
}