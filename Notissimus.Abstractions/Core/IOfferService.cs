using Notissimus.Abstractions.Dto;

namespace Notissimus.Abstractions.Core;

public interface IOfferService
{
    Task<OfferDto> ParseAndSaveOffer(int id);
}