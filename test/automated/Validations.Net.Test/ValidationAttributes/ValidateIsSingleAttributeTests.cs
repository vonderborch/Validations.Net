using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsSingleAttributeTests
{
    [Test]
    public void Validate_WithStringOfLengthOne_ReturnsSuccess()
    {
        var attr = new ValidateIsSingleAttribute();
        var result = attr.Validate("x");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCollectionOfOne_ReturnsSuccess()
    {
        var attr = new ValidateIsSingleAttribute();
        var result = attr.Validate(new[] { 42 });
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithEmptyString_ReturnsFailure()
    {
        var attr = new ValidateIsSingleAttribute();
        var result = attr.Validate("");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsSingleAttribute { Message = "Must have exactly one" };
        var result = attr.Validate("ab");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must have exactly one"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsSingleAttribute();
        var result = attr.Validate("ab", "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
