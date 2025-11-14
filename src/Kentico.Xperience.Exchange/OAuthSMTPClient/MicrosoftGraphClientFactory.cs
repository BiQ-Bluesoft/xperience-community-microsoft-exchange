using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace Kentico.Xperience.Exchange.OAuthSMTPClient;

public class MicrosoftGraphClientFactory
{
    private readonly MicrosoftGraphApiSettings _microsoftSettings;
    private readonly GraphServiceClient _client;
    public GraphServiceClient GetClient() => _client;
    public string SendFromEmail => _microsoftSettings.EmailFrom;

    public MicrosoftGraphClientFactory(IOptions<MicrosoftGraphApiSettings> microsoftOptions)
    {
        _microsoftSettings = microsoftOptions.Value;

        var scopes = new[] { "https://graph.microsoft.com/.default" };

        var options = new ClientSecretCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        };

        var credential = new ClientSecretCredential(_microsoftSettings.TenantId, _microsoftSettings.ClientId, _microsoftSettings.ClientSecret, options);

        _client = new GraphServiceClient(credential, scopes);
    }

}
