using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class EndsWithTests
{
    [Test]
    public void CheckEndsWith_WithNull_ReturnsFalse() => Assert.That(((string?)null).CheckEndsWith("suf"), Is.False);

    [Test]
    public void CheckEndsWith_WithMatchingSuffix_ReturnsTrue() => Assert.That("here_suffix".CheckEndsWith("suffix"), Is.True);

    [Test]
    public void CheckEndsWith_WithNonMatchingSuffix_ReturnsFalse() => Assert.That("other".CheckEndsWith("suffix"), Is.False);

    [Test]
    public void CheckEndsWith_WithComparison_RespectsCase()
    {
        Assert.That("Suffix".CheckEndsWith("fix", StringComparison.Ordinal), Is.True);
        Assert.That("suffix".CheckEndsWith("FIX", StringComparison.OrdinalIgnoreCase), Is.True);
    }

    [Test]
    public void ValidateEndsWith_WithMatchingSuffix_ReturnsValid() => Assert.That("x_suf".ValidateEndsWith("suf").IsValid, Is.True);

    [Test]
    public void ValidateEndsWith_WithNonMatchingSuffix_ReturnsInvalid()
    {
        var result = "other".ValidateEndsWith("suf");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(EndsWith.ValidatorName));
    }

    [Test]
    public void EnsureEndsWith_WithMatchingSuffix_DoesNotThrow() => Assert.DoesNotThrow(() => "suffix".EnsureEndsWith("fix"));

    [Test]
    public void EnsureEndsWith_WithNonMatchingSuffix_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "other".EnsureEndsWith("fix"));
        Assert.That(ex!.Validator, Is.EqualTo(EndsWith.ValidatorName));
    }
}
