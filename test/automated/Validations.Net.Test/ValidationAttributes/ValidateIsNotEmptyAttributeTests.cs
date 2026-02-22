namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotEmptyAttributeTests
{
    [Test]
    public void Validate_WithNonEmptyString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotEmptyAttribute();
        var result = attr.Validate("hello");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonEmptyCollection_ReturnsSuccess()
    {
        var attr = new ValidateIsNotEmptyAttribute();
        var result = attr.Validate(new[] { 1 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsNotEmptyAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotEmptyAttribute { Message = "Must not be empty" };
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be empty"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotEmptyAttribute();
        var result = attr.Validate("", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
