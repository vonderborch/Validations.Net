using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsNotValidJsonAttributeTests
{
    [Test]
    public void Validate_WithInvalidJson_ReturnsSuccess()
    {
        var attr = new ValidateIsNotValidJsonAttribute();
        var result = attr.Validate("not json");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValidJson_ReturnsFailure()
    {
        var attr = new ValidateIsNotValidJsonAttribute();
        var result = attr.Validate("{\"key\":\"value\"}");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotValidJsonAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotValidJsonAttribute { Message = "Must not be JSON" };
        var result = attr.Validate("{\"key\":\"value\"}");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be JSON"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotValidJsonAttribute();
        var result = attr.Validate("{\"key\":\"value\"}", "Payload");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Payload"));
    }
}
