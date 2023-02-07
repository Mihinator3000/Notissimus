namespace Notissimus.Abstractions.Dto;

public record OfferDto(
    int Id,
    string Type, 
    int Bid,
    bool Available,
    string Url, 
    int Price,
    string CurrencyId, 
    int CategoryId,
    string Picture, 
    string Description,
    List<OfferPropertyDto> AdditionalProperties);