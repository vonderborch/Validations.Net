using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsLengthTests
{
    [Test]
    public void CheckIsLength_WithExactLengthString_ReturnsTrue()
    {
        Assert.That("abc".CheckIsLength(3), Is.True);
    }

    [Test]
    public void CheckIsLength_WithWrongLengthString_ReturnsFalse()
    {
        Assert.That("abc".CheckIsLength(2), Is.False);
    }

    [Test]
    public void CheckIsLength_WithNullString_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsLength(0), Is.False);
    }

    [Test]
    public void CheckIsLength_WithExactCountList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckIsLength(3), Is.True);
    }

    [Test]
    public void CheckIsLength_WithRange_ReturnsTrue()
    {
        Assert.That("ab".CheckIsLength(1, 3), Is.True);
    }

    [Test]
    public void CheckIsLength_WithLengthCheckMode_GreaterThanOrEqual_ReturnsTrue()
    {
        Assert.That("abc".CheckIsLength(2, LengthCheckMode.GreaterThanOrEqual), Is.True);
    }

    [Test]
    public void ValidateIsLength_WithExactLength_ReturnsValid()
    {
        var result = "hi".ValidateIsLength(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsLength_WithWrongLength_ReturnsInvalid()
    {
        var result = "hi".ValidateIsLength(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Context.Context["actualLength"], Is.EqualTo(2));
    }

    [Test]
    public void EnsureIsLength_WithExactLength_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "x".EnsureIsLength(1));
    }

    [Test]
    public void EnsureIsLength_WithWrongLength_Throws()
    {
        Assert.Throws<ValidationException>(() => "xy".EnsureIsLength(1));
    }
}
