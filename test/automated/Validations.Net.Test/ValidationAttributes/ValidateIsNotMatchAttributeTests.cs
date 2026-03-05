using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotMatchAttributeTests
{
    [Test]
    public void Validate_WithNonMatchingString_ReturnsSuccess()
    {
        var attr = new ValidateIsNotMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate("123abc");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithMatchingString_ReturnsFailure()
    {
        var attr = new ValidateIsNotMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate("abc123");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateIsNotMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotMatchAttribute(@"^\d+$") { Message = "Must not be digits only" };
        var result = attr.Validate("123");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be digits only"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNotMatchAttribute(@"^\d+$");
        var result = attr.Validate("123", "Code");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Code"));
    }
}
