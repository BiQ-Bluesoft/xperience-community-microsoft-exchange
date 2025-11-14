using Microsoft.Graph.Models;

namespace Kentico.Xperience.Exchange.Helpers;

public static class RecipientsEmailHelper
{
    public static IEnumerable<Recipient> GetRecipients(string emailList)
    {
        var addresses = emailList.Split(';', StringSplitOptions.TrimEntries);
        return addresses
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(addr => new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = addr.ToLowerInvariant()
                }
            }
        );
    }
}
