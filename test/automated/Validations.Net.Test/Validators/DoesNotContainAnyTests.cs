using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotContainAnyTests
{
    [Test]
    public void CheckDoesNotContainAny_Collection_WithNull_ReturnsTrue()
    {
        Assert.That(((List<int>?)null).CheckDoesNotContainAny(new[] { 1, 2 }), Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_Collection_WithNonePresent_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesNotContainAny(new[] { 5, 6, 7 }), Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_Collection_WithAnyPresent_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesNotContainAny(new[] { 5, 2, 7 }), Is.False);
    }

    [Test]
    public void CheckDoesNotContainAny_String_WithNull_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckDoesNotContainAny(new[] { "a", "b" }), Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_String_WithNonePresent_ReturnsTrue()
    {
        Assert.That("hello".CheckDoesNotContainAny(new[] { "xyz", "abc" }), Is.True);
    }

    [Test]
    public void CheckDoesNotContainAny_String_WithAnyPresent_ReturnsFalse()
    {
        Assert.That("hello world".CheckDoesNotContainAny(new[] { "xyz", "wor" }), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAny_Collection_WithNonePresent_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesNotContainAny(new[] { "x", "y" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_Collection_WithAnyPresent_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesNotContainAny(new[] { 5, 2 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotContainAny.ValidatorName));
    }

    [Test]
    public void ValidateDoesNotContainAny_String_WithNonePresent_ReturnsValid()
    {
        Assert.That("abc".ValidateDoesNotContainAny(new[] { "x", "y" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAny_String_WithAnyPresent_ReturnsInvalid()
    {
        var result = "hello".ValidateDoesNotContainAny(new[] { "xyz", "ell" });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotContainAny.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotContainAny_Collection_WithNonePresent_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesNotContainAny(new[] { 5, 6 }));
    }

    [Test]
    public void EnsureDoesNotContainAny_Collection_WithAnyPresent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesNotContainAny(new[] { 5, 2 }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotContainAny.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotContainAny_String_WithNonePresent_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "hello".EnsureDoesNotContainAny(new[] { "xyz", "abc" }));
    }

    [Test]
    public void EnsureDoesNotContainAny_String_WithAnyPresent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "hello world".EnsureDoesNotContainAny(new[] { "ell", "wor" }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotContainAny.ValidatorName));
    }
}
