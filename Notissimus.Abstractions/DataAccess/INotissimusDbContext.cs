using Microsoft.EntityFrameworkCore;
using Notissimus.Domain.Entities;

namespace Notissimus.Abstractions.DataAccess;

public interface INotissimusDbContext
{
    DbSet<Offer> Offers { get; }
    DbSet<OfferProperty> OfferProperties { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}