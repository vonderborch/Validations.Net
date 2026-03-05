using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotLengthTests
{
    [Test]
    public void CheckIsNotLength_WithWrongLengthString_ReturnsTrue()
    {
        Assert.That("abc".CheckIsNotLength(2), Is.True);
    }

    [Test]
    public void CheckIsNotLength_WithExactLengthString_ReturnsFalse()
    {
        Assert.That("abc".CheckIsNotLength(3), Is.False);
    }

    [Test]
    public void CheckIsNotLength_WithNullString_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckIsNotLength(0), Is.True);
    }

    [Test]
    public void ValidateIsNotLength_WithWrongLength_ReturnsValid()
    {
        var result = "hi".ValidateIsNotLength(3);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotLength_WithExactLength_ReturnsInvalid()
    {
        var result = "hi".ValidateIsNotLength(2);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotLength_WithWrongLength_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "xy".EnsureIsNotLength(1));
    }

    [Test]
    public void EnsureIsNotLength_WithExactLength_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsNotLength(1));
    }
}
