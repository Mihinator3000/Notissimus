using System.Xml;
using AutoFixture.Xunit2;
using Notissimus.Abstractions.Core;
using Notissimus.Core.Parsers;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;
using Xunit;
using System.Runtime.CompilerServices;

namespace Notissimus.Tests.Parsers;

public class XmlOfferParserTests
{
    private readonly IXmlOfferParser _parser;

    public XmlOfferParserTests()
    {
        _parser = new XmlOfferParser();
    }

    [Theory, AutoData]
    public void ValidOfferXml_AllBasePropertiesAreMapped(int id, string type, int bid, bool available, string url, int price, string currencyId, int categoryId, string picture, string description)
    {
        string offerXml = $"""
            <offer id="{id}" type="{type}" bid="{bid}" available="{available}">
                <url>{url}</url>
                <price>{price}</price>
                <currencyId>{currencyId}</currencyId>
                <categoryId type="Own">{categoryId}</categoryId>
                <picture>{picture}</picture>
                <delivery>true</delivery>
                <artist>Pink Floyd</artist>
                <title>Dark Side Of The Moon, Platinum Disc</title>
                <year>1999</year>
                <media>CD</media>
                <description>{description}</description>
            </offer>
            """;

        XmlNode offerNode = CreateOfferNode(offerXml);
        Offer offer = _parser.Parse(offerNode);

        Assert.Equal(id, offer.Id);
        Assert.Equal(type, offer.Type);
        Assert.Equal(bid, offer.Bid);
        Assert.Equal(available, offer.Available);
        Assert.Equal(url, offer.Url);
        Assert.Equal(price, offer.Price);
        Assert.Equal(currencyId, offer.CurrencyId);
        Assert.Equal(categoryId, offer.CategoryId);
        Assert.Equal(picture, offer.Picture);
        Assert.Equal(description, offer.Description);
    }

    [Theory, AutoData]
    public void ValidOfferXml_AllAdditionalPropertiesAreMapped(string delivery, string artist, string title, string year, string media)
    {
        string offerXml = $"""
            <offer id="12344" type="artist.title" bid="11" available="true">
                <url>http://magazin.ru/product_page.asp?pid=14347</url>
                <price>450</price>
                <currencyId>RUR</currencyId>
                <categoryId type="Own">17</categoryId>
                <picture>http://magazin.ru/img/cd14347.jpg</picture>
                <delivery>{delivery}</delivery>
                <artist>{artist}</artist>
                <title>{title}</title>
                <year>{year}</year>
                <media>{media}</media>
                <description>Description</description>
            </offer>
            """;

        XmlNode offerNode = CreateOfferNode(offerXml);
        Offer offer = _parser.Parse(offerNode);
        IList<OfferProperty> additionalProperties = offer.AdditionalProperties.ToList();

        Assert.Equal(5, additionalProperties.Count);

        AssertPropertyEquals(additionalProperties[0], delivery);
        AssertPropertyEquals(additionalProperties[1], artist);
        AssertPropertyEquals(additionalProperties[2], title);
        AssertPropertyEquals(additionalProperties[3], year);
        AssertPropertyEquals(additionalProperties[4], media);
    }

    [Fact]
    public void InvalidPropertyType_ThrowXmlParseException()
    {
        const string invalidTypeId = "Not integer id";

        const string offerXml = $"""
            <offer id="{invalidTypeId}" type="artist.title" bid="11" available="true">
                <url>http://magazin.ru/product_page.asp?pid=14347</url>
                <price>450</price>
                <currencyId>RUR</currencyId>
                <categoryId type="Own">17</categoryId>
                <picture>http://magazin.ru/img/cd14347.jpg</picture>
                <delivery>true</delivery>
                <artist>Pink Floyd</artist>
                <title>Dark Side Of The Moon, Platinum Disc</title>
                <year>1999</year>
                <media>CD</media>
                <description>Description</description>
            </offer>
            """;

        XmlNode offerNode = CreateOfferNode(offerXml);
        Assert.Throws<XmlParseException>(() => _parser.Parse(offerNode));
    }

    private static void AssertPropertyEquals(OfferProperty property, string value, [CallerArgumentExpression(nameof(value))]string name="")
    {
        Assert.Equal(name, property.Name);
        Assert.Equal(value, property.Value);
    }

    private static XmlNode CreateOfferNode(string offerXml)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(offerXml);
        return xmlDocument.FirstChild!;
    }
}