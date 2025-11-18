using CMS.EmailEngine;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using XperienceCommunity.Exchange.OAuthEmailClient;

namespace XperienceCommunity.Exchange;

public static class ServiceRegistration
{
    public static IServiceCollection AddMSExchangeEmailSender(this IServiceCollection services,
        IConfiguration configuration,
        string configurationSectionKey = "MicrosoftGraphApi")
    {
        services.Configure<MicrosoftGraphApiSettings>(configuration.GetSection(key: configurationSectionKey));
        services.AddSingleton<MicrosoftGraphClientFactory>();
        services.AddSingleton<IEmailClient, ExchangeOAuthEmailClient>();

        return services;
    }
}
