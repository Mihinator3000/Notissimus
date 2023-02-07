using Microsoft.Extensions.DependencyInjection;
using Notissimus.Abstractions.Core;
using Notissimus.Core.Options;
using Notissimus.Core.Parsers;
using Notissimus.Core.Providers;
using Notissimus.Core.Services;

namespace Notissimus.Core.Extensions;

public static class RegistrationExtensions
{
    public static IServiceCollection AddCoreServices(
        this IServiceCollection services,
        Action<CoreOptions> action)
    {
        var instance = new CoreOptions();
        action(instance);

        services
            .AddScoped<IXmlDocumentProvider>(_ => new UrlXmlDocumentProvider(instance.XmlDocumentUrl))
            .AddScoped<IXmlOffersParser, XmlOffersParser>()
            .AddScoped<IXmlOfferParser, XmlOfferParser>()
            .AddScoped<IOfferService, OfferService>()
            .AddAutoMapper(typeof(IAssemblyMarker));
        
        return services;
    }
}