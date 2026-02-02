using XperienceCommunity.MicrosoftExchange.Helpers;

namespace XperienceCommunity.MicrosoftExchange.Tests;

[TestFixture]
public class RecipientsEmailHelperTests
{
    [Test]
    public void GetRecipients_NullInput_ReturnsEmpty()
    {
        var result = RecipientsEmailHelper.GetRecipients(null);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetRecipients_EmptyString_ReturnsEmpty()
    {
        var result = RecipientsEmailHelper.GetRecipients(string.Empty);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetRecipients_WhitespaceString_ReturnsEmpty()
    {
        var result = RecipientsEmailHelper.GetRecipients("   ");
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetRecipients_SingleEmail_ReturnsOneRecipient_Lowercased()
    {
        var result = RecipientsEmailHelper.GetRecipients("Test@Example.com").ToList();
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].EmailAddress.Address, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void GetRecipients_MultipleEmails_ReturnsAllRecipients_Lowercased()
    {
        string input = "User1@Example.com;User2@Example.com";
        var result = RecipientsEmailHelper.GetRecipients(input).ToList();
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].EmailAddress.Address, Is.EqualTo("user1@example.com"));
        Assert.That(result[1].EmailAddress.Address, Is.EqualTo("user2@example.com"));
    }

    [Test]
    public void GetRecipients_EmailsWithExtraSemicolonsAndWhitespace_IgnoresEmptyEntries()
    {
        string input = "  User1@Example.com ; ; User2@Example.com;; ";
        var result = RecipientsEmailHelper.GetRecipients(input).ToList();
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].EmailAddress.Address, Is.EqualTo("user1@example.com"));
        Assert.That(result[1].EmailAddress.Address, Is.EqualTo("user2@example.com"));
    }

    [Test]
    public void GetRecipients_MixedCaseEmails_ReturnsLowercasedAddresses()
    {
        string input = "UPPER@EXAMPLE.COM;MiXeD@eXaMpLe.CoM";
        var result = RecipientsEmailHelper.GetRecipients(input).ToList();
        Assert.That(result[0].EmailAddress.Address, Is.EqualTo("upper@example.com"));
        Assert.That(result[1].EmailAddress.Address, Is.EqualTo("mixed@example.com"));
    }
}
