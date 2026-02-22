namespace Validations.Net.Test.ValidationAttributes.Version;

[TestFixture]
public class ValidateIsCompatibleVersionAttributeTests
{
    [Test]
    public void Validate_WithCompatibleVersion_ReturnsSuccess()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("1.3.0");
        var result = attr.Validate("1.2.0");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithIncompatibleVersion_ReturnsFailure()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("1.2.0");
        var result = attr.Validate("1.3.0");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsCompatibleVersion.ValidatorName));
    }

    [Test]
    public void Validate_WithDifferentMajor_ReturnsFailure()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("2.0.0");
        var result = attr.Validate("1.2.0");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("1.2.0");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("1.2.0") { Message = "Version mismatch" };
        var result = attr.Validate("1.3.0");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Version mismatch"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsCompatibleVersionAttribute("1.2.0");
        var result = attr.Validate("1.3.0", "ApiVersion");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("ApiVersion"));
    }
}
