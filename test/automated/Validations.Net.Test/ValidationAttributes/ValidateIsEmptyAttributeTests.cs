using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsEmptyAttributeTests
{
    [Test]
    public void Validate_WithEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsEmptyAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyCollection_ReturnsSuccess()
    {
        var attr = new ValidateIsEmptyAttribute();
        var result = attr.Validate(Array.Empty<int>());
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsEmptyAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsEmptyAttribute { Message = "Must be empty" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be empty"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsEmptyAttribute();
        var result = attr.Validate("x", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
