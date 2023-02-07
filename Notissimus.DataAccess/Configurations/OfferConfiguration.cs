using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notissimus.Domain.Entities;

namespace Notissimus.DataAccess.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Navigation(o => o.AdditionalProperties).HasField("_additionalProperties");

        builder.HasKey(o => o.PrimaryKey);
    }
}