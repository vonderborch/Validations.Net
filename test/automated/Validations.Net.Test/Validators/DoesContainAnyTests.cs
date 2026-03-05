using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesContainAnyTests
{
    [Test]
    public void CheckDoesContainAny_Collection_WithNull_ReturnsFalse()
    {
        Assert.That(((List<int>?)null).CheckDoesContainAny(new[] { 1, 2 }), Is.False);
    }

    [Test]
    public void CheckDoesContainAny_Collection_WithAnyPresent_ReturnsTrue()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesContainAny(new[] { 5, 2, 7 }), Is.True);
    }

    [Test]
    public void CheckDoesContainAny_Collection_WithNonePresent_ReturnsFalse()
    {
        var list = new List<int> { 1, 2, 3 };
        Assert.That(list.CheckDoesContainAny(new[] { 5, 6, 7 }), Is.False);
    }

    [Test]
    public void CheckDoesContainAny_String_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckDoesContainAny(new[] { "a", "b" }), Is.False);
    }

    [Test]
    public void CheckDoesContainAny_String_WithAnyPresent_ReturnsTrue()
    {
        Assert.That("hello world".CheckDoesContainAny(new[] { "xyz", "wor" }), Is.True);
    }

    [Test]
    public void CheckDoesContainAny_String_WithNonePresent_ReturnsFalse()
    {
        Assert.That("hello".CheckDoesContainAny(new[] { "xyz", "abc" }), Is.False);
    }

    [Test]
    public void ValidateDoesContainAny_Collection_WithAnyPresent_ReturnsValid()
    {
        var arr = new[] { "a", "b", "c" };
        Assert.That(arr.ValidateDoesContainAny(new[] { "x", "b" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_Collection_WithNonePresent_ReturnsInvalid()
    {
        var result = new List<int> { 1, 2 }.ValidateDoesContainAny(new[] { 5, 6 });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesContainAny.ValidatorName));
    }

    [Test]
    public void ValidateDoesContainAny_String_WithAnyPresent_ReturnsValid()
    {
        Assert.That("abc".ValidateDoesContainAny(new[] { "x", "b" }).IsValid, Is.True);
    }

    [Test]
    public void ValidateDoesContainAny_String_WithNonePresent_ReturnsInvalid()
    {
        var result = "hello".ValidateDoesContainAny(new[] { "xyz", "abc" });
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesContainAny.ValidatorName));
    }

    [Test]
    public void EnsureDoesContainAny_Collection_WithAnyPresent_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => new List<int> { 1, 2, 3 }.EnsureDoesContainAny(new[] { 5, 2 }));
    }

    [Test]
    public void EnsureDoesContainAny_Collection_WithNonePresent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => new List<int> { 1, 2 }.EnsureDoesContainAny(new[] { 5, 6 }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesContainAny.ValidatorName));
    }

    [Test]
    public void EnsureDoesContainAny_String_WithAnyPresent_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "hello world".EnsureDoesContainAny(new[] { "ell", "wor" }));
    }

    [Test]
    public void EnsureDoesContainAny_String_WithNonePresent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "hello".EnsureDoesContainAny(new[] { "xyz", "abc" }));
        Assert.That(ex!.Validator, Is.EqualTo(DoesContainAny.ValidatorName));
    }
}
