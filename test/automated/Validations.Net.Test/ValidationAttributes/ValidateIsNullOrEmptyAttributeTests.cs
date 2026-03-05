using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNullOrEmptyAttributeTests
{
    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrEmptyAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrEmptyAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyCollection_ReturnsSuccess()
    {
        var attr = new ValidateIsNullOrEmptyAttribute();
        var result = attr.Validate(Array.Empty<int>());
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsNullOrEmptyAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNullOrEmptyAttribute { Message = "Must be null or empty" };
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be null or empty"));
    }
}
