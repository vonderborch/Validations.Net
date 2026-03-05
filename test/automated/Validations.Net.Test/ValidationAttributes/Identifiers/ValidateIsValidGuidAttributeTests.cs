using Validations.Net.OLD.Validators.Identifiers;

namespace Validations.Net.Test.ValidationAttributes.Identifiers;

[TestFixture]
public class ValidateIsValidGuidAttributeTests
{
    [Test]
    public void Validate_WithValidGuidString_ReturnsSuccess()
    {
        var attr = new ValidateIsValidGuidAttribute();
        var result = attr.Validate("550e8400-e29b-41d4-a716-446655440000");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidGuidString_ReturnsFailure()
    {
        var attr = new ValidateIsValidGuidAttribute();
        var result = attr.Validate("not-a-guid");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsValidGuidAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidGuidAttribute { Message = "Must be valid GUID" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid GUID"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidGuidAttribute();
        var result = attr.Validate("invalid", "Id");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Id"));
    }
}
