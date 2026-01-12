using Microsoft.Graph.Models;

namespace XperienceCommunity.Exchange.Helpers;

public static class RecipientsEmailHelper
{
    public static IEnumerable<Recipient> GetRecipients(string emailList)
    {
        if (string.IsNullOrWhiteSpace(emailList))
        {
            return [];
        }

        string[] addresses = emailList.Split(';', StringSplitOptions.TrimEntries);
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
