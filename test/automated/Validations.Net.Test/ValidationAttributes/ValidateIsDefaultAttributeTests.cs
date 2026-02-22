namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsDefaultAttributeTests
{
    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsDefaultAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonDefaultValue_ReturnsFailure()
    {
        var attr = new ValidateIsDefaultAttribute();
        var result = attr.Validate(42);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsDefaultAttribute { Message = "Must be default" };
        var result = attr.Validate(1);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be default"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsDefaultAttribute();
        var result = attr.Validate(1, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
