using Validations.Net.Validators.Finance;

namespace Validations.Net.Test.Validators.Finance;

[TestFixture]
public class IsValidIbanTests
{
    [Test]
    public void CheckIsValidIban_WithValidFormat_ReturnsTrue()
    {
        Assert.That("GB82WEST12345698765432".CheckIsValidIban(), Is.True);
    }

    [Test]
    public void CheckIsValidIban_WithSpaces_ReturnsTrue()
    {
        Assert.That("GB82 WEST 1234 5698 7654 32".CheckIsValidIban(), Is.True);
    }

    [Test]
    public void CheckIsValidIban_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidIban(), Is.False);
    }

    [Test]
    public void CheckIsValidIban_WithTooShort_ReturnsFalse()
    {
        Assert.That("GB12".CheckIsValidIban(), Is.False);
    }

    [Test]
    public void CheckIsValidIban_WithInvalidCountryCode_ReturnsFalse()
    {
        Assert.That("123456789012345678901234567".CheckIsValidIban(), Is.False);
    }

    [Test]
    public void ValidateIsValidIban_WithValidFormat_ReturnsValid()
    {
        var result = "DE89370400440532013000".ValidateIsValidIban();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidIban_WithInvalidFormat_ReturnsInvalid()
    {
        var result = "invalid".ValidateIsValidIban();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidIban_WithValidFormat_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "GB82WEST12345698765432".EnsureIsValidIban());
    }

    [Test]
    public void EnsureIsValidIban_WithInvalidFormat_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsValidIban());
    }
}
