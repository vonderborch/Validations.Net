using Validations.Net.Validators.Network;

namespace Validations.Net.Test.Validators.Network;

[TestFixture]
public class IsValidIpAddressTests
{
    [Test]
    public void CheckIsValidIpAddress_WithValidIpv4_ReturnsTrue()
    {
        Assert.That("192.168.1.1".CheckIsValidIpAddress(), Is.True);
    }

    [Test]
    public void CheckIsValidIpAddress_WithValidIpv6_ReturnsTrue()
    {
        Assert.That("::1".CheckIsValidIpAddress(), Is.True);
    }

    [Test]
    public void CheckIsValidIpAddress_WithInvalidString_ReturnsFalse()
    {
        Assert.That("not.an.ip".CheckIsValidIpAddress(), Is.False);
    }

    [Test]
    public void CheckIsValidIpAddress_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidIpAddress(), Is.False);
    }

    [Test]
    public void ValidateIsValidIpAddress_WithValidIp_ReturnsValid()
    {
        var result = "10.0.0.1".ValidateIsValidIpAddress();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidIpAddress_WithInvalidIp_ReturnsInvalid()
    {
        var result = "999.999.999.999".ValidateIsValidIpAddress();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidIpAddress_WithValidIp_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "127.0.0.1".EnsureIsValidIpAddress());
    }

    [Test]
    public void EnsureIsValidIpAddress_WithInvalidIp_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidIpAddress());
    }
}
