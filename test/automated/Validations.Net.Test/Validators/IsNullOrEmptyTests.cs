using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNullOrEmptyTests
{
    [Test]
    public void CheckIsNullOrEmpty_WithNullString_ReturnsTrue()
    {
        Assert.That(((string?)null).CheckIsNullOrEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithEmptyString_ReturnsTrue()
    {
        Assert.That(string.Empty.CheckIsNullOrEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithNonEmptyString_ReturnsFalse()
    {
        Assert.That("abc".CheckIsNullOrEmpty(), Is.False);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithNullList_ReturnsTrue()
    {
        List<int>? nullList = null;
        Assert.That(nullList.CheckIsNullOrEmpty(), Is.True);
    }

    [Test]
    public void CheckIsNullOrEmpty_WithEmptyList_ReturnsTrue()
    {
        var list = new List<int>();
        Assert.That(list.CheckIsNullOrEmpty(), Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNull_ReturnsValid()
    {
        var result = ((string?)null).ValidateIsNullOrEmpty();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNullOrEmpty_WithNonEmptyString_ReturnsInvalid()
    {
        var result = "x".ValidateIsNullOrEmpty();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNullOrEmpty_WithNull_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => ((string?)null).EnsureIsNullOrEmpty());
    }

    [Test]
    public void EnsureIsNullOrEmpty_WithNonEmptyString_Throws()
    {
        Assert.Throws<ValidationException>(() => "x".EnsureIsNullOrEmpty());
    }
}
