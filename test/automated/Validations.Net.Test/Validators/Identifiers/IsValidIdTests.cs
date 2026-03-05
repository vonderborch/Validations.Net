using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Identifiers;

namespace Validations.Net.Test.Validators.Identifiers;

[TestFixture]
public class IsValidIdTests
{
    [Test]
    public void CheckIsValidId_WithValidAlphanumeric_ReturnsTrue()
    {
        Assert.That("abc123".CheckIsValidId(), Is.True);
    }

    [Test]
    public void CheckIsValidId_WithHyphenAndUnderscore_ReturnsTrue()
    {
        Assert.That("my-id_value".CheckIsValidId(), Is.True);
    }

    [Test]
    public void CheckIsValidId_WithInvalidCharacters_ReturnsFalse()
    {
        Assert.That("invalid!id".CheckIsValidId(), Is.False);
        Assert.That("id with spaces".CheckIsValidId(), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidId(), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithEmptyString_ReturnsFalse()
    {
        Assert.That("".CheckIsValidId(), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithLengthBelowMin_ReturnsFalse()
    {
        Assert.That("".CheckIsValidId(minLength: 1), Is.False);
        Assert.That("a".CheckIsValidId(minLength: 2), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithLengthAboveMax_ReturnsFalse()
    {
        Assert.That(new string('a', 256).CheckIsValidId(maxLength: 255), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithCustomLengthRange_RespectsBounds()
    {
        Assert.That("ab".CheckIsValidId(minLength: 2, maxLength: 5), Is.True);
        Assert.That("a".CheckIsValidId(minLength: 2, maxLength: 5), Is.False);
        Assert.That("abcdef".CheckIsValidId(minLength: 2, maxLength: 5), Is.False);
    }

    [Test]
    public void CheckIsValidId_WithCustomPattern_RespectsPattern()
    {
        var numericOnly = @"^[0-9]+$";
        Assert.That("12345".CheckIsValidId(allowedPattern: numericOnly), Is.True);
        Assert.That("12a45".CheckIsValidId(allowedPattern: numericOnly), Is.False);
    }

    [Test]
    public void ValidateIsValidId_WithValidId_ReturnsValid()
    {
        var result = "valid-id_123".ValidateIsValidId();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidId_WithInvalidId_ReturnsInvalid()
    {
        var result = "invalid!".ValidateIsValidId();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidId.ValidatorName));
    }

    [Test]
    public void EnsureIsValidId_WithValidId_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "valid_id".EnsureIsValidId());
    }

    [Test]
    public void EnsureIsValidId_WithInvalidId_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid!".EnsureIsValidId());
    }
}
