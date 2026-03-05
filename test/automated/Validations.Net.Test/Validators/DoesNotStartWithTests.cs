using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotStartWithTests
{
    [Test]
    public void CheckDoesNotStartWith_WithNull_ReturnsTrue() => Assert.That(((string?)null).CheckDoesNotStartWith("pre"), Is.True);

    [Test]
    public void CheckDoesNotStartWith_WithMatchingPrefix_ReturnsFalse() => Assert.That("prefix_here".CheckDoesNotStartWith("prefix"), Is.False);

    [Test]
    public void CheckDoesNotStartWith_WithNonMatchingPrefix_ReturnsTrue() => Assert.That("other".CheckDoesNotStartWith("prefix"), Is.True);

    [Test]
    public void ValidateDoesNotStartWith_WithNonMatchingPrefix_ReturnsValid() => Assert.That("other".ValidateDoesNotStartWith("pre").IsValid, Is.True);

    [Test]
    public void ValidateDoesNotStartWith_WithMatchingPrefix_ReturnsInvalid()
    {
        var result = "prefix".ValidateDoesNotStartWith("pre");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotStartWith.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotStartWith_WithNonMatchingPrefix_DoesNotThrow() => Assert.DoesNotThrow(() => "other".EnsureDoesNotStartWith("pre"));

    [Test]
    public void EnsureDoesNotStartWith_WithMatchingPrefix_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "prefix".EnsureDoesNotStartWith("pre"));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotStartWith.ValidatorName));
    }
}
