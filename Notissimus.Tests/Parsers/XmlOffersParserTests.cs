using Moq;
using Notissimus.Abstractions.Core;
using Notissimus.Core.Parsers;
using System.Xml;
using AutoFixture;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;
using Xunit;

namespace Notissimus.Tests.Parsers;

public class XmlOffersParserTests
{
    [Fact]
    public void ValidXml_ParsedNodeCountIsCorrect()
    {
        const string xml = """
            <yml_catalog date="2010-04-01 17:00">
                <shop>
                    <name>Magazin</name>
                    <offers>
                        <offer id="12341" type="vendor.model" bid="13" cbid="20" available="true"> 
                        </offer>
                        <offer id="12342" type="book" bid="17" available="true">
                        </offer>
                        <offer id="12343" type="audiobook" bid="17" available="true"> 
                        </offer>
                        <offer id="12344" type="artist.title" bid="11" available="true">
                        </offer>
                        <offer id="12345" type="artist.title" bid="56" available="true">
                        </offer>
                    </offers>
                </shop>
            </yml_catalog>
            """;

        var xmlOfferParserMock = new Mock<IXmlOfferParser>();

        xmlOfferParserMock
            .Setup(p => p.Parse(It.IsAny<XmlNode>()))
            .Returns(new Fixture().Build<Offer>().Create());

        var offersParser = new XmlOffersParser(xmlOfferParserMock.Object);
        var xmlDocument = Create(xml);

        IReadOnlyCollection<Offer> offers = offersParser.Parse(xmlDocument);

        const int offerCount = 5;
        Assert.Equal(offerCount, offers.Count);
    }
    
    [Fact]
    public void NoOffersInXml_ThrowXmlParseException()
    {
        const string xml = "<invalidXml><withNoOffers/></invalidXml>";

        var offersParser = new XmlOffersParser(new Mock<IXmlOfferParser>().Object);
        var xmlDocument = Create(xml);

        Assert.Throws<XmlParseException>(() => offersParser.Parse(xmlDocument));
    }

    private static XmlDocument Create(string xml)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xml);
        return xmlDocument;
    }
}