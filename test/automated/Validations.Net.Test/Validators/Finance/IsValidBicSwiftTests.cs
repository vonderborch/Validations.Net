using Validations.Net.OLD;
using Validations.Net.OLD.Validators.Finance;

namespace Validations.Net.Test.Validators.Finance;

[TestFixture]
public class IsValidBicSwiftTests
{
    [Test]
    public void CheckIsValidBicSwift_With8CharCode_ReturnsTrue()
    {
        Assert.That("DEUTDEFF".CheckIsValidBicSwift(), Is.True);
    }

    [Test]
    public void CheckIsValidBicSwift_With11CharCode_ReturnsTrue()
    {
        Assert.That("DEUTDEFF500".CheckIsValidBicSwift(), Is.True);
    }

    [Test]
    public void CheckIsValidBicSwift_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidBicSwift(), Is.False);
    }

    [Test]
    public void CheckIsValidBicSwift_WithWrongLength_ReturnsFalse()
    {
        Assert.That("DEUT".CheckIsValidBicSwift(), Is.False);
        Assert.That("DEUTDEFF5001".CheckIsValidBicSwift(), Is.False);
    }

    [Test]
    public void ValidateIsValidBicSwift_WithValidCode_ReturnsValid()
    {
        var result = "CHASUS33".ValidateIsValidBicSwift();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidBicSwift_WithInvalidCode_ReturnsInvalid()
    {
        var result = "123".ValidateIsValidBicSwift();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidBicSwift_WithValidCode_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "DEUTDEFF".EnsureIsValidBicSwift());
    }

    [Test]
    public void EnsureIsValidBicSwift_WithInvalidCode_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsValidBicSwift());
    }
}
