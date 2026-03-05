using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsValidJsonAttributeTests
{
    [Test]
    public void Validate_WithValidJson_ReturnsSuccess()
    {
        var attr = new ValidateIsValidJsonAttribute();
        var result = attr.Validate("{\"key\":\"value\"}");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidJson_ReturnsFailure()
    {
        var attr = new ValidateIsValidJsonAttribute();
        var result = attr.Validate("not json");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsValidJsonAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidJsonAttribute { Message = "Must be valid JSON" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid JSON"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidJsonAttribute();
        var result = attr.Validate("invalid", "Payload");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Payload"));
    }
}
