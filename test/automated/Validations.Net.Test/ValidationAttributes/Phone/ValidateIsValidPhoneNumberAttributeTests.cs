namespace Validations.Net.Test.ValidationAttributes.Phone;

[TestFixture]
public class ValidateIsValidPhoneNumberAttributeTests
{
    [Test]
    public void Validate_WithValidPhoneNumber_ReturnsSuccess()
    {
        var attr = new ValidateIsValidPhoneNumberAttribute();
        var result = attr.Validate("+1234567890");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidPhoneNumber_ReturnsFailure()
    {
        var attr = new ValidateIsValidPhoneNumberAttribute();
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithFormattedPhoneNumber_ReturnsSuccess()
    {
        var attr = new ValidateIsValidPhoneNumberAttribute();
        var result = attr.Validate("(123) 456-7890");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidPhoneNumberAttribute { Message = "Invalid phone" };
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Invalid phone"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidPhoneNumberAttribute();
        var result = attr.Validate("abc", "Phone");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Phone"));
    }
}
