using Validations.Net.Validators.Age;

namespace Validations.Net.Test.ValidationAttributes.Age;

[TestFixture]
public class ValidateIsAdultAttributeTests
{
    [Test]
    public void Validate_WithAdultAge_ReturnsSuccess()
    {
        var attr = new ValidateIsAdultAttribute();
        var result = attr.Validate(18);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithMinorAge_ReturnsFailure()
    {
        var attr = new ValidateIsAdultAttribute();
        var result = attr.Validate(17);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomAdultAge_UsesCustomThreshold()
    {
        var attr = new ValidateIsAdultAttribute(21);
        Assert.That(attr.Validate(21).IsValid, Is.True);
        Assert.That(attr.Validate(20).IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsAdultAttribute { Message = "Too young" };
        var result = attr.Validate(10);
        Assert.That(result.ExceptionMessage, Does.Contain("Too young"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsAdultAttribute();
        var result = attr.Validate(5, "Age");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Age"));
    }
}
