namespace Validations.Net.Test.ValidationAttributes.Age;

[TestFixture]
public class ValidateIsMinorAttributeTests
{
    [Test]
    public void Validate_WithMinorAge_ReturnsSuccess()
    {
        var attr = new ValidateIsMinorAttribute();
        var result = attr.Validate(17);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithAdultAge_ReturnsFailure()
    {
        var attr = new ValidateIsMinorAttribute();
        var result = attr.Validate(18);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomAdultAge_RespectsThreshold()
    {
        var attr = new ValidateIsMinorAttribute(21);
        Assert.That(attr.Validate(20).IsValid, Is.True);
        Assert.That(attr.Validate(21).IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsMinorAttribute { Message = "Must be minor" };
        var result = attr.Validate(18);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be minor"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsMinorAttribute();
        var result = attr.Validate(18, "Age");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Age"));
    }
}
