using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsWhiteSpaceTests
{
    [Test]
    public void CheckIsWhiteSpace_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckIsWhiteSpace(), Is.False);

    [Test]
    public void CheckIsWhiteSpace_WithEmpty_ReturnsFalse() => Assert.That("".CheckIsWhiteSpace(), Is.False);

    [Test]
    public void CheckIsWhiteSpace_WithWhitespace_ReturnsTrue() => Assert.That("   ".CheckIsWhiteSpace(), Is.True);

    [Test]
    public void CheckIsWhiteSpace_WithContent_ReturnsFalse() => Assert.That("x".CheckIsWhiteSpace(), Is.False);

    [Test]
    public void ValidateIsWhiteSpace_WithWhitespace_ReturnsValid() => Assert.That("  \t\n  ".ValidateIsWhiteSpace().IsValid, Is.True);

    [Test]
    public void ValidateIsWhiteSpace_WithEmpty_ReturnsInvalid()
    {
        var result = "".ValidateIsWhiteSpace();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsWhiteSpace.ValidatorName));
    }

    [Test]
    public void EnsureIsWhiteSpace_WithWhitespace_DoesNotThrow() => Assert.DoesNotThrow(() => "   ".EnsureIsWhiteSpace());

    [Test]
    public void EnsureIsWhiteSpace_WithContent_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "abc".EnsureIsWhiteSpace());
        Assert.That(ex!.Validator, Is.EqualTo(IsWhiteSpace.ValidatorName));
    }
}
