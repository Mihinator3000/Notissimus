using Notissimus.Domain.Entities;
using System.Xml;

namespace Notissimus.Abstractions.Core;

public interface IXmlOffersParser
{
    IReadOnlyCollection<Offer> Parse(XmlDocument xmlDocument);
}