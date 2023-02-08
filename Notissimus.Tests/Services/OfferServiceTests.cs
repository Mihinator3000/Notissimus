using AutoMapper;
using Moq;
using Notissimus.Abstractions.Core;
using Notissimus.Abstractions.DataAccess;
using Notissimus.Core.Services;
using System.Xml;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;
using Xunit;

namespace Notissimus.Tests.Services;

public class OfferServiceTests
{
    [Fact]
    public void NoOfferWithSelectedId_ThrowEntityNotFoundException()
    {
        const int selectedId = 12344;

        var xmlOffersParserMock = new Mock<IXmlOffersParser>();
        xmlOffersParserMock
            .Setup(p => p.Parse(It.IsAny<XmlDocument>()))
            .Returns(new List<Offer>
            {
                new() { Id = 0 },
                new() { Id = 1 }
            });

        var offerService = new OfferService(
            new Mock<IXmlDocumentProvider>().Object,
            xmlOffersParserMock.Object,
            new Mock<INotissimusDbContext>().Object,
            new Mock<IMapper>().Object);

        Assert.ThrowsAsync<EntityNotFoundException>(() => offerService.ParseAndSaveOffer(selectedId));
    }
}