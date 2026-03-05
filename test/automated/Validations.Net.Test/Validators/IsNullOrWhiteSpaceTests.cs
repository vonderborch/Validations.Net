using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNullOrWhiteSpaceTests
{
    [Test]
    public void CheckIsNullOrWhiteSpace_WithNull_ReturnsTrue() => Assert.That(((string?)null).CheckIsNullOrWhiteSpace(), Is.True);

    [Test]
    public void CheckIsNullOrWhiteSpace_WithEmpty_ReturnsTrue() => Assert.That("".CheckIsNullOrWhiteSpace(), Is.True);

    [Test]
    public void CheckIsNullOrWhiteSpace_WithWhitespace_ReturnsTrue() => Assert.That("   ".CheckIsNullOrWhiteSpace(), Is.True);

    [Test]
    public void CheckIsNullOrWhiteSpace_WithContent_ReturnsFalse() => Assert.That("hello".CheckIsNullOrWhiteSpace(), Is.False);

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithNull_ReturnsValid() => Assert.That(((string?)null).ValidateIsNullOrWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithWhitespace_ReturnsValid() => Assert.That("  \t  ".ValidateIsNullOrWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsNullOrWhiteSpace_WithContent_ReturnsInvalid()
    {
        var result = "x".ValidateIsNullOrWhiteSpace();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsNullOrWhiteSpace.ValidatorName));
    }

    [Test]
    public void EnsureIsNullOrWhiteSpace_WithNull_DoesNotThrow() => Assert.DoesNotThrow(() => ((string?)null).EnsureIsNullOrWhiteSpace());

    [Test]
    public void EnsureIsNullOrWhiteSpace_WithContent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "abc".EnsureIsNullOrWhiteSpace());
        Assert.That(ex!.Validator, Is.EqualTo(IsNullOrWhiteSpace.ValidatorName));
    }
}
