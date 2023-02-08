using Notissimus.Core.Providers;
using Notissimus.Domain.Exceptions;
using Xunit;

namespace Notissimus.Tests.Services;

public class UrlXmlDocumentProviderTests
{
    [Fact]
    public void InvalidUrl_ThrowDataProviderException()
    {
        const string invalidUrl = "invalid url";
        var xmlDocumentProvider = new UrlXmlDocumentProvider(invalidUrl);

        Assert.ThrowsAsync<DataProviderException>(xmlDocumentProvider.GetXmlData);
    }

    [Fact]
    public void DataIsNotXml_ThrowDataProviderException()
    {
        const string notXmlUrl = "https://picsum.photos/200/300";
        var xmlDocumentProvider = new UrlXmlDocumentProvider(notXmlUrl);

        Assert.ThrowsAsync<DataProviderException>(xmlDocumentProvider.GetXmlData);
    }
}