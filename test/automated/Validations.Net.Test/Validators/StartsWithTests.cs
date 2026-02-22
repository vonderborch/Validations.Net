using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class StartsWithTests
{
    [Test]
    public void CheckStartsWith_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckStartsWith("pre"), Is.False);

    [Test]
    public void CheckStartsWith_WithMatchingPrefix_ReturnsTrue() => Assert.That("prefix_here".CheckStartsWith("prefix"), Is.True);

    [Test]
    public void CheckStartsWith_WithNonMatchingPrefix_ReturnsFalse() => Assert.That("other".CheckStartsWith("prefix"), Is.False);

    [Test]
    public void CheckStartsWith_WithComparison_RespectsCase()
    {
        Assert.That("Prefix".CheckStartsWith("pre", StringComparison.Ordinal), Is.False);
        Assert.That("Prefix".CheckStartsWith("pre", StringComparison.OrdinalIgnoreCase), Is.True);
    }

    [Test]
    public void ValidateStartsWith_WithMatchingPrefix_ReturnsValid() => Assert.That("pre_x".ValidateStartsWith("pre").IsValid, Is.True);

    [Test]
    public void ValidateStartsWith_WithNonMatchingPrefix_ReturnsInvalid()
    {
        var result = "other".ValidateStartsWith("pre");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(StartsWith.ValidatorName));
    }

    [Test]
    public void EnsureStartsWith_WithMatchingPrefix_DoesNotThrow() => Assert.DoesNotThrow(() => "prefix".EnsureStartsWith("pre"));

    [Test]
    public void EnsureStartsWith_WithNonMatchingPrefix_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "other".EnsureStartsWith("pre"));
        Assert.That(ex!.Validator, Is.EqualTo(StartsWith.ValidatorName));
    }
}
