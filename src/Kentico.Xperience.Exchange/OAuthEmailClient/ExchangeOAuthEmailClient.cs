using CMS.EmailEngine;

using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

using XperienceCommunity.Exchange.Helpers;

namespace XperienceCommunity.Exchange.OAuthEmailClient;

public class ExchangeOAuthEmailClient : IEmailClient
{
    protected readonly GraphServiceClient MicrosoftGraphClient;
    protected readonly string EmailAddress;

    public ExchangeOAuthEmailClient(MicrosoftGraphClientFactory graphClientFactory)
    {
        EmailAddress = graphClientFactory.SendFromEmail;
        MicrosoftGraphClient = graphClientFactory.GetClient();
    }

    public virtual async Task<EmailSendResult> SendEmail(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var requestBody = new SendMailPostRequestBody
        {
            Message = ConvertToMessage(emailMessage),
        };

        try
        {
            await MicrosoftGraphClient.Users[EmailAddress].SendMail.PostAsync(requestBody, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return new EmailSendResult(false, ex.Message);
        }
        return new EmailSendResult(true);
    }

    protected virtual Message ConvertToMessage(EmailMessage emailMessage) => new()
    {
        Subject = emailMessage.Subject,
        Body = new ItemBody
        {
            ContentType = emailMessage.EmailFormat == EmailFormatEnum.PlainText ? BodyType.Text : BodyType.Html,
            Content = emailMessage.Body,
        },
        ToRecipients = [.. RecipientsEmailHelper.GetRecipients(emailMessage.Recipients)],
        CcRecipients = [.. RecipientsEmailHelper.GetRecipients(emailMessage.CcRecipients)],
        BccRecipients = [.. RecipientsEmailHelper.GetRecipients(emailMessage.BccRecipients)],
        ReplyTo = [.. RecipientsEmailHelper.GetRecipients(emailMessage.ReplyTo)],
    };
}
