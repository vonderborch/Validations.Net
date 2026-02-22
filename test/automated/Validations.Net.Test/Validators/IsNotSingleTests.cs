using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotSingleTests
{
    [Test]
    public void CheckIsNotSingle_WithMultiCharString_ReturnsTrue()
    {
        Assert.That("xy".CheckIsNotSingle(), Is.True);
    }

    [Test]
    public void CheckIsNotSingle_WithSingleCharString_ReturnsFalse()
    {
        Assert.That("x".CheckIsNotSingle(), Is.False);
    }

    [Test]
    public void CheckIsNotSingle_WithEmptyString_ReturnsTrue()
    {
        Assert.That(string.Empty.CheckIsNotSingle(), Is.True);
    }

    [Test]
    public void CheckIsNotSingle_WithMultiElementList_ReturnsTrue()
    {
        var list = new List<int> { 1, 2 };
        Assert.That(list.CheckIsNotSingle(), Is.True);
    }

    [Test]
    public void CheckIsNotSingle_WithSingleElementList_ReturnsFalse()
    {
        var list = new List<int> { 1 };
        Assert.That(list.CheckIsNotSingle(), Is.False);
    }

    [Test]
    public void ValidateIsNotSingle_WithMultipleElements_ReturnsValid()
    {
        var list = new List<int> { 1, 2 };
        var result = list.ValidateIsNotSingle();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotSingle_WithSingleElement_ReturnsInvalid()
    {
        var result = "x".ValidateIsNotSingle();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotSingle_WithMultipleElements_DoesNotThrow()
    {
        var list = new List<int> { 1, 2 };
        Assert.DoesNotThrow(() => list.EnsureIsNotSingle());
    }

    [Test]
    public void EnsureIsNotSingle_WithSingleElement_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsNotSingle());
    }
}
