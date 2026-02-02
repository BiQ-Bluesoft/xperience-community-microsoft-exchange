# Usage Guide

This guide provides instructions for extending the `ExchangeOAuthEmailClient` class to customize email sending behavior.

## Extending ExchangeOAuthEmailClient

The `ExchangeOAuthEmailClient` class is designed to be extended for custom scenarios. It provides `virtual` methods that can be overridden to customize behavior.

### Common Extension Scenarios

The base `ExchangeOAuthEmailClient` class has limitations (e.g., no attachment support). You can extend it to add custom functionality:

- **Add attachment support** - Extend the message conversion to include file attachments
- **Custom headers** - Add tracking headers, priority levels, or custom metadata
- **Reply-to customization** - Implement dynamic reply-to addresses based on content
- **Logging and monitoring** - Add telemetry or logging around email operations
- **Error handling** - Implement custom retry logic or error notification

### Adding Attachment Support

Here's an example showing how to extend the email client to support attachments:

```csharp
using CMS.EmailEngine;
using Microsoft.Graph.Models;
using XperienceCommunity.MicrosoftExchange.OAuthEmailClient;

namespace YourProject.Email;

public class ExchangeEmailClientWithAttachments : ExchangeOAuthEmailClient
{
    public ExchangeEmailClientWithAttachments(MicrosoftGraphClientFactory graphClientFactory)
        : base(graphClientFactory)
    {
    }

    protected override Message ConvertToMessage(EmailMessage emailMessage)
    {
        // Call base implementation to get standard message
        var message = base.ConvertToMessage(emailMessage);

        // Add attachments if present
        if (emailMessage.Attachments?.Count > 0)
        {
            message.Attachments = new List<Attachment>();

            foreach (var attachment in emailMessage.Attachments)
            {
                // ... process each attachment

                message.Attachments.Add(fileAttachment);
            }
        }

        return message;
    }
}
```

### Overriding Send Behavior

For advanced scenarios, you can override the `SendEmail` method to add custom logic.

### Registering Custom Implementation

After creating your custom email client, register it in `Program.cs`:

#### Replace the default registration

Instead of using `AddMicrosoftExchangeEmailSender`, manually register your custom implementation:


## Using a custom configuration section

You can specify a different configuration section key name instead of the default `"MicrosoftGraphApiEmailSender"`:

```csharp
builder.Services.AddMicrosoftExchangeEmailSender(builder.Configuration, "MyCustomSection");
```
