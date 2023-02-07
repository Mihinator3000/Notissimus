using System.Xml;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notissimus.Abstractions.Core;
using Notissimus.Abstractions.DataAccess;
using Notissimus.Abstractions.Dto;
using Notissimus.Domain.Entities;
using Notissimus.Domain.Exceptions;

namespace Notissimus.Core.Services;

public class OfferService : IOfferService
{
    private readonly IXmlDocumentProvider _xmlDocumentProvider;
    private readonly IXmlOffersParser _offersParser;
    private readonly INotissimusDbContext _context;
    private readonly IMapper _mapper;

    public OfferService(
        IXmlDocumentProvider xmlDocumentProvider,
        IXmlOffersParser offersParser,
        INotissimusDbContext context,
        IMapper mapper)
    {
        _xmlDocumentProvider = xmlDocumentProvider;
        _offersParser = offersParser;
        _context = context;
        _mapper = mapper;
    }

    public async Task<OfferDto> ParseAndSaveOffer(int id)
    {
        XmlDocument xmlDocument = await _xmlDocumentProvider.GetXmlData();
        IReadOnlyCollection<Offer> offers = _offersParser.Parse(xmlDocument);

        Offer? selectedOffer = offers.FirstOrDefault(o => o.Id == id);

        if (selectedOffer is null)
            throw new EntityNotFoundException($"Offer with id {id} was not found");

        await SaveIfDoesNotExist(selectedOffer);

        var selectedOfferDto = _mapper.Map<OfferDto>(selectedOffer);
        return selectedOfferDto;
    }

    private async Task SaveIfDoesNotExist(Offer offer)
    {
        if (await _context.Offers.AnyAsync(o => o.Id == offer.Id))
            return;

        await _context.Offers.AddAsync(offer);
        await _context.SaveChangesAsync();
    }
}