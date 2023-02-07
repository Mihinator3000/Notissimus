using System.Xml;

namespace Notissimus.Abstractions.Core;

public interface IXmlDocumentProvider
{
    Task<XmlDocument> GetXmlData();
}