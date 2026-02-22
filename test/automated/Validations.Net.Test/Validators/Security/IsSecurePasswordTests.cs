using Validations.Net.Validators.Security;

namespace Validations.Net.Test.Validators.Security;

[TestFixture]
public class IsSecurePasswordTests
{
    [Test]
    public void CheckIsSecurePassword_WithValidPassword_ReturnsTrue()
    {
        Assert.That("Abcdef1!".CheckIsSecurePassword(), Is.True);
        Assert.That("SecureP@ss123".CheckIsSecurePassword(), Is.True);
    }

    [Test]
    public void CheckIsSecurePassword_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithTooShort_ReturnsFalse()
    {
        Assert.That("Abc1!".CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithNoUppercase_ReturnsFalse()
    {
        Assert.That("abcdef1!".CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithNoLowercase_ReturnsFalse()
    {
        Assert.That("ABCDEF1!".CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithNoDigit_ReturnsFalse()
    {
        Assert.That("Abcdefg!".CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithNoSpecialChar_ReturnsFalse()
    {
        Assert.That("Abcdef12".CheckIsSecurePassword(), Is.False);
    }

    [Test]
    public void CheckIsSecurePassword_WithRelaxedRequirements_ReturnsTrue()
    {
        Assert.That("abc".CheckIsSecurePassword(minLength: 3, requireUppercase: false, requireLowercase: true, requireDigit: false, requireSpecialChar: false), Is.True);
    }

    [Test]
    public void ValidateIsSecurePassword_WithValidPassword_ReturnsValid()
    {
        var result = "Abcdef1!".ValidateIsSecurePassword();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsSecurePassword_WithInvalidPassword_ReturnsInvalid()
    {
        var result = "weak".ValidateIsSecurePassword();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsSecurePassword.ValidatorName));
    }

    [Test]
    public void EnsureIsSecurePassword_WithValidPassword_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "Abcdef1!".EnsureIsSecurePassword());
    }

    [Test]
    public void EnsureIsSecurePassword_WithInvalidPassword_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "weak".EnsureIsSecurePassword());
        Assert.That(ex!.Validator, Is.EqualTo(IsSecurePassword.ValidatorName));
    }
}
