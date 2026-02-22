using Validations.Net.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class DoesNotEndWithTests
{
    [Test]
    public void CheckDoesNotEndWith_WithNull_ReturnsTrue() => Assert.That(((string?)null).CheckDoesNotEndWith("suf"), Is.True);

    [Test]
    public void CheckDoesNotEndWith_WithMatchingSuffix_ReturnsFalse() => Assert.That("here_suffix".CheckDoesNotEndWith("suffix"), Is.False);

    [Test]
    public void CheckDoesNotEndWith_WithNonMatchingSuffix_ReturnsTrue() => Assert.That("other".CheckDoesNotEndWith("suffix"), Is.True);

    [Test]
    public void ValidateDoesNotEndWith_WithNonMatchingSuffix_ReturnsValid() => Assert.That("other".ValidateDoesNotEndWith("suf").IsValid, Is.True);

    [Test]
    public void ValidateDoesNotEndWith_WithMatchingSuffix_ReturnsInvalid()
    {
        var result = "suffix".ValidateDoesNotEndWith("fix");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(DoesNotEndWith.ValidatorName));
    }

    [Test]
    public void EnsureDoesNotEndWith_WithNonMatchingSuffix_DoesNotThrow() => Assert.DoesNotThrow(() => "other".EnsureDoesNotEndWith("fix"));

    [Test]
    public void EnsureDoesNotEndWith_WithMatchingSuffix_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "suffix".EnsureDoesNotEndWith("fix"));
        Assert.That(ex!.Validator, Is.EqualTo(DoesNotEndWith.ValidatorName));
    }
}
