using AutoMapper;
using Notissimus.Abstractions.Dto;
using Notissimus.Domain.Entities;

namespace Notissimus.Core.MappingProfiles;

public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<Offer, OfferDto>();
        CreateMap<OfferProperty, OfferPropertyDto>();
    }
}