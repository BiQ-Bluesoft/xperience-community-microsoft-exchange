using Azure.Identity;

using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace XperienceCommunity.Exchange.OAuthEmailClient;

public class MicrosoftGraphClientFactory
{
    private readonly MicrosoftGraphApiSettings microsoftSettings;
    private readonly GraphServiceClient client;
    public GraphServiceClient GetClient() => client;
    public string SendFromEmail => microsoftSettings.EmailFrom;

    public MicrosoftGraphClientFactory(IOptions<MicrosoftGraphApiSettings> microsoftOptions)
    {
        microsoftSettings = microsoftOptions.Value;

        string[] scopes = new[] { "https://graph.microsoft.com/.default" };

        var options = new ClientSecretCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        };

        var credential = new ClientSecretCredential(microsoftSettings.TenantId, microsoftSettings.ClientId, microsoftSettings.ClientSecret, options);

        client = new GraphServiceClient(credential, scopes);
    }
}
