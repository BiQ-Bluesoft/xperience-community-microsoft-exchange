using CMS.EmailEngine;
using Kentico.Xperience.Exchange.Helpers;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace Kentico.Xperience.Exchange.OAuthSMTPClient;

public class OAuthSmtpClient : IEmailClient
{
    private readonly GraphServiceClient microsoftGraphClient;
    private readonly string emailAddress;
    public OAuthSmtpClient(MicrosoftGraphClientFactory graphClientFactory)
    {
        emailAddress = graphClientFactory.SendFromEmail;
        microsoftGraphClient = graphClientFactory.GetClient();
    }

    public async Task<EmailSendResult> SendEmail(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var requestBody = new SendMailPostRequestBody
        {
            Message = new Message
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
                //Attachments = emailMessage.Attachments.Select(attachment =>
                //{
                //    byte[] contentBytes;
                //    using (var ms = new MemoryStream())
                //    {
                //        attachment.ContentStream.CopyTo(ms);
                //        contentBytes = ms.ToArray();
                //    }
                //    return new FileAttachment
                //    {
                //        OdataType = "#microsoft.graph.fileAttachment", //TODO ???
                //        Name = attachment.Name,
                //        ContentType = attachment.ContentType.ToString(),
                //        ContentBytes = contentBytes,
                //    };
                //}).ToList<Attachment>(),
                //TODO doplnit co nejvíc
            },
        };

        try
        {
            await microsoftGraphClient.Users[emailAddress].SendMail.PostAsync(requestBody, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            return new EmailSendResult(false, ex.Message);
        }
        return new EmailSendResult(true);
    }
}
