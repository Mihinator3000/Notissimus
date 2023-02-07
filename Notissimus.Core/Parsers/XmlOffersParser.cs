using System.Xml;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;
using Notissimus.Abstractions.Core;

namespace Notissimus.Core.Parsers;

public class XmlOffersParser : IXmlOffersParser
{
    private readonly IXmlOfferParser _offerParser;

    public XmlOffersParser(IXmlOfferParser offerParser)
    {
        _offerParser = offerParser;
    }

    public IReadOnlyCollection<Offer> Parse(XmlDocument xmlDocument)
    {
        XmlNodeList? offerNodes = xmlDocument
            .SelectSingleNode("/yml_catalog/shop")
            ?.SelectSingleNode("offers")
            ?.SelectNodes("offer");

        if (offerNodes is null)
            throw new XmlParseException("Failed to find offers in xml");
        
        return offerNodes
            .Cast<XmlNode>()
            .Select(_offerParser.Parse)
            .ToArray();
    }
}