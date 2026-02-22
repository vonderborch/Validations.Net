namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotNullOrEmptyAttributeTests
{
    [Test]
    public void Validate_WithNonEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonEmptyCollection_ReturnsSuccess()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute();
        var result = attr.Validate(new[] { 1 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotNullOrEmptyAttribute { Message = "Required field" };
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Required field"));
    }
}
