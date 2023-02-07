using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notissimus.Abstractions.DataAccess;

namespace Notissimus.DataAccess.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
    {
        services.AddDbContext<INotissimusDbContext, NotissimusDbContext>(optionsAction);
        return services;
    }

    public static Task UseDatabaseContext(this IServiceProvider services)
    {
        var context = services.GetRequiredService<NotissimusDbContext>();
        return context.Database.EnsureCreatedAsync();
    }
}