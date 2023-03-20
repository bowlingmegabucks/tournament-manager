
namespace NortheastMegabuck.Tests;

[TestFixture]
internal class Encryption
{
    [OneTimeSetUp]
    public void SetUpKey()
        => NortheastMegabuck.Encryption.Key = "key";

    [Test]
    public void EncryptDecrypt_Returns_OriginalValue()
    {
        var plainText = "test";

        var encrypt = plainText.Encrypt();
        var decrypt = encrypt.Decrypt();

        Assert.That(decrypt, Is.EqualTo(plainText));
    }

    [Test]
    public void Encrypt_DoneMultipleTimesOnSameValue_YeildsDifferentResults()
    {
        var plainText = "test";

        var encrypt1 = plainText.Encrypt();
        var encrypt2 = plainText.Encrypt();
        var encrypt3 = plainText.Encrypt();

        Assert.That(new[] { encrypt1, encrypt2, encrypt3 }.Distinct().Count(), Is.EqualTo(3));
    }

    [Test]
    public void Encrypt_VerifySaltIsRandomized()
    {
        var plainText = "test";

        var encrypted = plainText.Encrypt();

        Assert.That(encrypted.Take(5), Is.Not.EqualTo("AAAAA"));
    }

    [Test]
    public void Encrypt_StringNullOrEmpty_ReturnsEmptyString([Values(null, "")] string value)
        => Assert.That(value.Encrypt(), Is.Empty);

    [Test]
    public void Encrypt_StringWhitespace_ReturnsEncryptedValue([Values(" ", "  ")] string value)
        => Assert.That(value.Encrypt(), Is.Not.Empty);

    [Test]
    public void Decrypt_AllEncryptionsReturnSameResult()
    {
        var plainText = "test";

        var encrypt1 = plainText.Encrypt();
        var encrypt2 = plainText.Encrypt();
        var encrypt3 = plainText.Encrypt();

        var decrypted = new[] { encrypt1.Decrypt(), encrypt2.Decrypt(), encrypt3.Decrypt() };

        Assert.That(decrypted.Distinct().Count(), Is.EqualTo(1));
    }

    [Test]
    public void Decrypt_NullEmpty_ReturnsEmptyString([Values(null, "")] string value)
        => Assert.That(value.Decrypt, Is.Empty);

    [Test]
    public void ValuesMatch_ValuesSame_ReturnsTrue()
    {
        var value1 = "test".Encrypt();
        var value2 = "test".Encrypt();

        Assert.That(NortheastMegabuck.Encryption.ValuesMatch(value1, value2), Is.True);
    }

    [Test]
    public void ValuesMatch_ValuesDifferent_ReturnsFalse()
    {
        var value1 = "test".Encrypt();
        var value2 = "value".Encrypt();

        Assert.That(NortheastMegabuck.Encryption.ValuesMatch(value1, value2), Is.False);
    }
}
