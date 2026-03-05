using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsOddAttributeTests
{
    [Test]
    public void Validate_WithOddInt_ReturnsSuccess()
    {
        var attr = new ValidateIsOddAttribute();
        var result = attr.Validate(1);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEvenInt_ReturnsFailure()
    {
        var attr = new ValidateIsOddAttribute();
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithZero_ReturnsFailure()
    {
        var attr = new ValidateIsOddAttribute();
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsOddAttribute { Message = "Must be odd" };
        var result = attr.Validate(4);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be odd"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsOddAttribute();
        var result = attr.Validate(2, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
