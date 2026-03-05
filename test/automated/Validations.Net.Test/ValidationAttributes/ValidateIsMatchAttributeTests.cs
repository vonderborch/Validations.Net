using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsMatchAttributeTests
{
    [Test]
    public void Validate_WithMatchingString_ReturnsSuccess()
    {
        var attr = new ValidateIsMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate("abc123");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonMatchingString_ReturnsFailure()
    {
        var attr = new ValidateIsMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate("123abc");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsMatchAttribute(@"^[a-z]+\d+$");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsMatchAttribute(@"^\d+$") { Message = "Must be digits only" };
        var result = attr.Validate("abc");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be digits only"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsMatchAttribute(@"^\d+$");
        var result = attr.Validate("abc", "Code");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Code"));
    }
}
