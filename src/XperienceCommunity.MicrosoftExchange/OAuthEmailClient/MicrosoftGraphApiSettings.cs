namespace XperienceCommunity.MicrosoftExchange.OAuthEmailClient;

public class MicrosoftGraphApiSettings
{
    public required string TenantId { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string EmailFrom { get; set; }
}
