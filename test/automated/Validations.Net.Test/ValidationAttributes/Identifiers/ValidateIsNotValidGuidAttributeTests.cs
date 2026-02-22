namespace Validations.Net.Test.ValidationAttributes.Identifiers;

[TestFixture]
public class ValidateIsNotValidGuidAttributeTests
{
    [Test]
    public void Validate_WithInvalidGuidString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotValidGuidAttribute();
        var result = attr.Validate("not-a-guid");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValidGuidString_ReturnsFailure()
    {
        var attr = new ValidateIsNotValidGuidAttribute();
        var result = attr.Validate("550e8400-e29b-41d4-a716-446655440000");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNonStringValue_ReturnsSuccess()
    {
        var attr = new ValidateIsNotValidGuidAttribute();
        var result = attr.Validate(123);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotValidGuidAttribute { Message = "Must not be GUID" };
        var result = attr.Validate("550e8400-e29b-41d4-a716-446655440000");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be GUID"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotValidGuidAttribute();
        var result = attr.Validate("550e8400-e29b-41d4-a716-446655440000", "Id");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Id"));
    }
}
