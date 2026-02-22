using Validations.Net.Validators.Identifiers;

namespace Validations.Net.Test.Validators.Identifiers;

[TestFixture]
public class IsValidGuidTests
{
    [Test]
    public void CheckIsValidGuid_WithValidGuid_ReturnsTrue()
    {
        Assert.That("550e8400-e29b-41d4-a716-446655440000".CheckIsValidGuid(), Is.True);
    }

    [Test]
    public void CheckIsValidGuid_WithValidGuidNoHyphens_ReturnsTrue()
    {
        Assert.That("550e8400e29b41d4a716446655440000".CheckIsValidGuid(), Is.True);
    }

    [Test]
    public void CheckIsValidGuid_WithInvalidString_ReturnsFalse()
    {
        Assert.That("not-a-guid".CheckIsValidGuid(), Is.False);
    }

    [Test]
    public void CheckIsValidGuid_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidGuid(), Is.False);
    }

    [Test]
    public void CheckIsValidGuid_WithEmptyString_ReturnsFalse()
    {
        Assert.That("".CheckIsValidGuid(), Is.False);
    }

    [Test]
    public void ValidateIsValidGuid_WithValidGuid_ReturnsValid()
    {
        var result = "550e8400-e29b-41d4-a716-446655440000".ValidateIsValidGuid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidGuid_WithInvalidGuid_ReturnsInvalid()
    {
        var result = "invalid".ValidateIsValidGuid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidGuid.ValidatorName));
    }

    [Test]
    public void EnsureIsValidGuid_WithValidGuid_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "550e8400-e29b-41d4-a716-446655440000".EnsureIsValidGuid());
    }

    [Test]
    public void EnsureIsValidGuid_WithInvalidGuid_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidGuid());
    }
}
