namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotDefaultAttributeTests
{
    [Test]
    public void Validate_WithNonDefaultValue_ReturnsSuccess()
    {
        var attr = new ValidateIsNotDefaultAttribute();
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsNotDefaultAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithBoxedInt_ReturnsSuccess()
    {
        // Boxed 0 is a non-null object, so it is not default(object?)
        var attr = new ValidateIsNotDefaultAttribute();
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotDefaultAttribute { Message = "Must not be default" };
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be default"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotDefaultAttribute();
        var result = attr.Validate(null, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
