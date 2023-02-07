using Microsoft.EntityFrameworkCore;
using Notissimus.Abstractions.DataAccess;
using Notissimus.Domain.Entities;

namespace Notissimus.DataAccess;

public class NotissimusDbContext : DbContext, INotissimusDbContext
{
    public NotissimusDbContext(DbContextOptions<NotissimusDbContext> options)
        : base(options) { }

    public required DbSet<Offer> Offers { get; init; }
    public required DbSet<OfferProperty> OfferProperties { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}