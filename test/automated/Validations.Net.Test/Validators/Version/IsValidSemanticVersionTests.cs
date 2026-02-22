using Validations.Net.Validators.Version;

namespace Validations.Net.Test.Validators.Version;

[TestFixture]
public class IsValidSemanticVersionTests
{
    [Test]
    public void CheckIsValidSemanticVersion_WithValidVersion_ReturnsTrue()
    {
        Assert.That("1.0.0".CheckIsValidSemanticVersion(), Is.True);
        Assert.That("2.1.3".CheckIsValidSemanticVersion(), Is.True);
        Assert.That("0.0.1".CheckIsValidSemanticVersion(), Is.True);
    }

    [Test]
    public void CheckIsValidSemanticVersion_WithPreRelease_ReturnsTrue()
    {
        Assert.That("1.0.0-alpha".CheckIsValidSemanticVersion(), Is.True);
        Assert.That("1.0.0-alpha.1".CheckIsValidSemanticVersion(), Is.True);
        Assert.That("1.0.0-0.3.7".CheckIsValidSemanticVersion(), Is.True);
    }

    [Test]
    public void CheckIsValidSemanticVersion_WithBuildMetadata_ReturnsTrue()
    {
        Assert.That("1.0.0+20130313144700".CheckIsValidSemanticVersion(), Is.True);
        Assert.That("1.0.0-alpha+001".CheckIsValidSemanticVersion(), Is.True);
    }

    [Test]
    public void CheckIsValidSemanticVersion_WithNull_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidSemanticVersion(), Is.False);
    }

    [Test]
    public void CheckIsValidSemanticVersion_WithEmpty_ReturnsFalse()
    {
        Assert.That("".CheckIsValidSemanticVersion(), Is.False);
    }

    [Test]
    public void CheckIsValidSemanticVersion_WithInvalidFormat_ReturnsFalse()
    {
        Assert.That("1".CheckIsValidSemanticVersion(), Is.False);
        Assert.That("1.0".CheckIsValidSemanticVersion(), Is.False);
        Assert.That("01.0.0".CheckIsValidSemanticVersion(), Is.False);
        Assert.That("1.00.0".CheckIsValidSemanticVersion(), Is.False);
        Assert.That("1.0.01".CheckIsValidSemanticVersion(), Is.False);
    }

    [Test]
    public void ValidateIsValidSemanticVersion_WithValidVersion_ReturnsValid()
    {
        var result = "1.0.0".ValidateIsValidSemanticVersion();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidSemanticVersion_WithInvalidVersion_ReturnsInvalid()
    {
        var result = "invalid".ValidateIsValidSemanticVersion();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Validator, Is.EqualTo(IsValidSemanticVersion.ValidatorName));
    }

    [Test]
    public void EnsureIsValidSemanticVersion_WithValidVersion_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => "1.0.0".EnsureIsValidSemanticVersion());
    }

    [Test]
    public void EnsureIsValidSemanticVersion_WithInvalidVersion_Throws()
    {
        var ex = Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidSemanticVersion());
        Assert.That(ex!.Validator, Is.EqualTo(IsValidSemanticVersion.ValidatorName));
    }
}
