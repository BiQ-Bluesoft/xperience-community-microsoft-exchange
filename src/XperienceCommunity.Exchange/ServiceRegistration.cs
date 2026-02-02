using Azure.Identity;

using CMS.EmailEngine;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

using XperienceCommunity.Exchange.OAuthEmailClient;

namespace XperienceCommunity.Exchange;

public static class ServiceRegistration
{
    public static IServiceCollection AddMicrosoftExchangeEmailSender(this IServiceCollection services,
        IConfiguration configuration,
        string configurationSectionKey = "MicrosoftGraphApiEmailSender")
    {
        services.Configure<MicrosoftGraphApiSettings>(configuration.GetSection(key: configurationSectionKey));
        services.AddSingleton(sp =>
        {
            var microsoftOptions = sp.GetRequiredService<IOptions<MicrosoftGraphApiSettings>>().Value;
            return new GraphServiceClient(new ClientSecretCredential(
                    microsoftOptions.TenantId,
                    microsoftOptions.ClientId,
                    microsoftOptions.ClientSecret),
                ["https://graph.microsoft.com/.default"]);
        });
        services.AddSingleton<IEmailClient, ExchangeOAuthEmailClient>();

        return services;
    }
}
