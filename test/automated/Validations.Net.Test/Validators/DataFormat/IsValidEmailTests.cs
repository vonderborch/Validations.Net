using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsValidEmailTests
{
    [Test]
    public void CheckIsValidEmail_WithValidEmail_ReturnsTrue()
    {
        Assert.That("user@example.com".CheckIsValidEmail(), Is.True);
        Assert.That("a@b.co".CheckIsValidEmail(), Is.True);
    }

    [Test]
    public void CheckIsValidEmail_WithInvalidEmail_ReturnsFalse()
    {
        Assert.That("noatsign.com".CheckIsValidEmail(), Is.False);
        Assert.That("@nodomain.com".CheckIsValidEmail(), Is.False);
        Assert.That("nobefore@.com".CheckIsValidEmail(), Is.False);
        Assert.That("user@domain".CheckIsValidEmail(), Is.False);
        Assert.That("user @domain.com".CheckIsValidEmail(), Is.False);
    }

    [Test]
    public void CheckIsValidEmail_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidEmail(), Is.False);
    }

    [Test]
    public void ValidateIsValidEmail_WithValidEmail_ReturnsValid()
    {
        var result = "user@example.com".ValidateIsValidEmail();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidEmail_WithInvalidEmail_ReturnsInvalid()
    {
        var result = "invalid".ValidateIsValidEmail();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidEmail_WithInvalidEmail_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidEmail());
    }
}
