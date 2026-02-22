using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotNullOrEmptyTests
{
    [Test]
    public void CheckIsNotNullOrEmpty_WithNonEmptyString_ReturnsTrue()
    {
        Assert.That("abc".CheckIsNotNullOrEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNullString_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsNotNullOrEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithEmptyString_ReturnsFalse()
    {
        Assert.That(string.Empty.CheckIsNotNullOrEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNonEmptyList_ReturnsTrue()
    {
        System.Collections.ICollection list = new List<int> { 1 };
        Assert.That(list.CheckIsNotNullOrEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNotNullOrEmpty_WithNullList_ReturnsFalse()
    {
        System.Collections.ICollection? nullList = null;
        Assert.That(nullList.CheckIsNotNullOrEmpty(), Is.False);
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNonEmptyString_ReturnsValid()
    {
        var result = "x".ValidateIsNotNullOrEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotNullOrEmpty_WithNull_ReturnsInvalid()
    {
        var result = ((string?)null).ValidateIsNotNullOrEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotNullOrEmpty_WithNonEmptyString_ReturnsValue()
    {
        var result = "x".EnsureIsNotNullOrEmpty();
        Assert.That(result, Is.EqualTo("x"));
    }

    [Test]
    public void EnsureIsNotNullOrEmpty_WithNull_Throws()
    {
        Assert.Throws<ValidationException>(() => ((string?)null).EnsureIsNotNullOrEmpty());
    }
}
