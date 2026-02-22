using Validations.Net.Validators.Phone;

namespace Validations.Net.Test.Validators.Phone;

[TestFixture]
public class IsValidPhoneNumberTests
{
    [Test]
    public void CheckIsValidPhoneNumber_WithValidPhone_ReturnsTrue()
    {
        Assert.That("1234567".CheckIsValidPhoneNumber(), Is.True);
        Assert.That("1234567890".CheckIsValidPhoneNumber(), Is.True);
        Assert.That("+1234567890".CheckIsValidPhoneNumber(), Is.True);
        Assert.That("+1 234 567 890".CheckIsValidPhoneNumber(), Is.True);
        Assert.That("123-456-7890".CheckIsValidPhoneNumber(), Is.True);
        Assert.That("(123) 456-7890".CheckIsValidPhoneNumber(), Is.True);
    }

    [Test]
    public void CheckIsValidPhoneNumber_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidPhoneNumber(), Is.False);
    }

    [Test]
    public void CheckIsValidPhoneNumber_WithTooFewDigits_ReturnsFalse()
    {
        Assert.That("123456".CheckIsValidPhoneNumber(), Is.False);
    }

    [Test]
    public void CheckIsValidPhoneNumber_WithTooManyDigits_ReturnsFalse()
    {
        Assert.That("1234567890123456".CheckIsValidPhoneNumber(), Is.False);
    }

    [Test]
    public void CheckIsValidPhoneNumber_WithInvalidCharacters_ReturnsFalse()
    {
        Assert.That("1234567a".CheckIsValidPhoneNumber(), Is.False);
    }

    [Test]
    public void CheckIsValidPhoneNumber_WithPlusAfterDigits_ReturnsFalse()
    {
        Assert.That("123+4567890".CheckIsValidPhoneNumber(), Is.False);
    }

    [Test]
    public void ValidateIsValidPhoneNumber_WithValidPhone_ReturnsValid()
    {
        var result = "1234567890".ValidateIsValidPhoneNumber();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidPhoneNumber_WithInvalidPhone_ReturnsInvalid()
    {
        var result = "123".ValidateIsValidPhoneNumber();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidPhoneNumber.ValidatorName));
    }

    [Test]
    public void EnsureIsValidPhoneNumber_WithValidPhone_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "1234567890".EnsureIsValidPhoneNumber());
    }

    [Test]
    public void EnsureIsValidPhoneNumber_WithInvalidPhone_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "123".EnsureIsValidPhoneNumber());
        Assert.That(ex!.Validator, Is.EqualTo(IsValidPhoneNumber.ValidatorName));
    }
}
