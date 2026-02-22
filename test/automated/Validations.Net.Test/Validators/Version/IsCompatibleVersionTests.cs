namespace Validations.Net.Test.Validators.Version;

[TestFixture]
public class IsCompatibleVersionTests
{
    [Test]
    public void CheckIsCompatibleVersion_WithSameMajorAndHigherMinor_ReturnsTrue()
    {
        Assert.That("1.2.0".CheckIsCompatibleVersion("1.3.0"), Is.True);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithSameMajorAndSameMinor_ReturnsTrue()
    {
        Assert.That("1.2.0".CheckIsCompatibleVersion("1.2.5"), Is.True);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithSameMajorAndLowerMinor_ReturnsFalse()
    {
        Assert.That("1.3.0".CheckIsCompatibleVersion("1.2.0"), Is.False);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithDifferentMajor_ReturnsFalse()
    {
        Assert.That("1.2.0".CheckIsCompatibleVersion("2.2.0"), Is.False);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithNullValue_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsCompatibleVersion("1.2.0"), Is.False);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithNullTarget_ReturnsFalse()
    {
        Assert.That("1.2.0".CheckIsCompatibleVersion(null), Is.False);
    }

    [Test]
    public void CheckIsCompatibleVersion_WithInvalidVersion_ReturnsFalse()
    {
        Assert.That("invalid".CheckIsCompatibleVersion("1.2.0"), Is.False);
        Assert.That("1.2.0".CheckIsCompatibleVersion("invalid"), Is.False);
    }

    [Test]
    public void ValidateIsCompatibleVersion_WithCompatibleVersions_ReturnsValid()
    {
        var result = "1.2.0".ValidateIsCompatibleVersion("1.3.0");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsCompatibleVersion_WithIncompatibleVersions_ReturnsInvalid()
    {
        var result = "1.3.0".ValidateIsCompatibleVersion("1.2.0");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsCompatibleVersion.ValidatorName));
    }

    [Test]
    public void EnsureIsCompatibleVersion_WithIncompatibleVersions_Throws()
    {
        Assert.Throws<ValidationException>(() => "1.3.0".EnsureIsCompatibleVersion("1.2.0"));
    }

    [Test]
    public void EnsureIsCompatibleVersion_WithCompatibleVersions_ReturnsValue()
    {
        var result = "1.2.0".EnsureIsCompatibleVersion("1.3.0");
        Assert.That(result, Is.EqualTo("1.2.0"));
    }
}
