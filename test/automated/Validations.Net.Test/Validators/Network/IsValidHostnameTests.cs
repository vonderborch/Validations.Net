using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Network;

namespace Validations.Net.Test.Validators.Network;

[TestFixture]
public class IsValidHostnameTests
{
    [Test]
    public void CheckIsValidHostname_WithValidHostname_ReturnsTrue()
    {
        Assert.That("example.com".CheckIsValidHostname(), Is.True);
    }

    [Test]
    public void CheckIsValidHostname_WithSubdomain_ReturnsTrue()
    {
        Assert.That("www.example.com".CheckIsValidHostname(), Is.True);
    }

    [Test]
    public void CheckIsValidHostname_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidHostname(), Is.False);
    }

    [Test]
    public void CheckIsValidHostname_WithEmptyString_ReturnsFalse()
    {
        Assert.That("".CheckIsValidHostname(), Is.False);
    }

    [Test]
    public void CheckIsValidHostname_WithLeadingHyphen_ReturnsFalse()
    {
        Assert.That("-example.com".CheckIsValidHostname(), Is.False);
    }

    [Test]
    public void ValidateIsValidHostname_WithValidHostname_ReturnsValid()
    {
        var result = "host.example.org".ValidateIsValidHostname();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidHostname_WithInvalidHostname_ReturnsInvalid()
    {
        var result = "invalid..host".ValidateIsValidHostname();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidHostname_WithValidHostname_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "valid-host.com".EnsureIsValidHostname());
    }

    [Test]
    public void EnsureIsValidHostname_WithInvalidHostname_Throws()
    {
        Assert.Throws<ValidationException>(() => "".EnsureIsValidHostname());
    }
}
