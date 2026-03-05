using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotEmptyTests
{
    [Test]
    public void CheckIsNotEmpty_WithNonEmptyString_ReturnsTrue()
    {
        Assert.That("abc".CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyString_ReturnsFalse()
    {
        Assert.That(string.Empty.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNullString_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotEmpty_WithNonEmptyList_ReturnsTrue()
    {
        System.Collections.ICollection list = new List<int> { 1 };
        Assert.That(list.CheckIsNotEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotEmpty_WithEmptyList_ReturnsFalse()
    {
        System.Collections.ICollection list = new List<int>();
        Assert.That(list.CheckIsNotEmpty(), Is.False);
    }

    [Test]
    public void ValidateIsNotEmpty_WithNonEmptyString_ReturnsValid()
    {
        var result = "x".ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotEmpty_WithEmptyString_ReturnsInvalid()
    {
        var result = string.Empty.ValidateIsNotEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotEmpty_WithNonEmptyString_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "x".EnsureIsNotEmpty());
    }

    [Test]
    public void EnsureIsNotEmpty_WithEmptyString_Throws()
    {
        Assert.Throws<ValidationException>(() => string.Empty.EnsureIsNotEmpty());
    }
}
