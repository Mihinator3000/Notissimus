using System.Text;
using System.Xml;
using Notissimus.Abstractions.Core;
using Notissimus.Domain.Exceptions;

namespace Notissimus.Core.Providers;

public class UrlXmlDocumentProvider : IXmlDocumentProvider
{
    private readonly string _xmlDocumentUrl;

    public UrlXmlDocumentProvider(string xmlDocumentUrl)
    {
        _xmlDocumentUrl = xmlDocumentUrl;
    }

    public async Task<XmlDocument> GetXmlData()
    {
        using var client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(_xmlDocumentUrl);

        if (!response.IsSuccessStatusCode)
            throw new DataProviderException($"Error accessing xml file: {response.StatusCode}");

        var stream = await response.Content.ReadAsStreamAsync();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        try
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            return xmlDocument;
        }
        catch (XmlException)
        {
            throw new DataProviderException("Document is not in the correct xml format");
        }
    }
}