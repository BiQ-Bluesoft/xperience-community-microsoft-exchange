# Xperience by Kentico: MS Exchange Email Integration

[![CI: Build and Test](https://github.com/BiQ-Bluesoft/xperience-community-exchange/actions/workflows/ci.yml/badge.svg)](https://github.com/BiQ-Bluesoft/xperience-community-exchange/actions/workflows/ci.yml)

## Description

A Xperience by Kentico integration for sending emails via Microsoft 365 / Exchange Online using OAuth (Graph API). Provides service registration, token acquisition, and recipient normalization utilities.

### Limitations

- Attachment sending not implemented. Ready to extend.

## Requirements

### Dependencies

- ASP.NET Core 8.0+
- Xperience by Kentico >= 28.1.0
<!-- TODO: Confirm if consumers must reference Microsoft.Graph / Azure.Identity directly or if bundled. -->

### Other prerequisites

- Azure App Registration with `Mail.Send` [permission](https://learn.microsoft.com/en-us/graph/permissions-reference#mailsend).
- Administrators can configure [application access policy](https://learn.microsoft.com/en-us/exchange/permissions-exo/application-rbac) to limit app access to specific mailboxes and not to all the mailboxes in the organization, even if the app has been granted the Mail.Send application permission.

## Package Installation

Install from NuGet:

```powershell
dotnet add package XperienceCommunity.Exchange
```

## Quick Start

1. Install the NuGet package.
2. Add configuration to `appsettings.json`:
   ```json
   {
     "MicrosoftGraphApiEmailSender": {
       "TenantId": "<GUID>",
       "ClientId": "<GUID>",
       "ClientSecret": "<secret>",
       "Sender": "no-reply@domain.com"
     }
   }
   ```
3. Register services in `Program.cs`:
   ```csharp
   builder.Services.AddMSExchangeEmailSender();
   ```

## Full Instructions

See the [Usage Guide](./docs/Usage-Guide.md) for advanced configuration and scenarios.

## Contributing

Instructions and technical details for contributing to this project can be found in [Contributing Setup](./docs/Contributing-Setup.md).

## License

Distributed under the MIT License. See [`LICENSE.md`](./LICENSE.md) for more information.
