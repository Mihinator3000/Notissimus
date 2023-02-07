using Notissimus.Domain.Entities;
using System.Xml;

namespace Notissimus.Abstractions.Core;

public interface IXmlOfferParser
{
    Offer Parse(XmlNode offerNode);
}