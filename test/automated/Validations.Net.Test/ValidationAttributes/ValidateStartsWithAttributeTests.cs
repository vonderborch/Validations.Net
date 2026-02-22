namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateStartsWithAttributeTests
{
    [Test]
    public void Validate_WithMatchingPrefix_ReturnsSuccess()
    {
        var attr = new ValidateStartsWithAttribute("Hello");
        var result = attr.Validate("Hello World");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNonMatchingPrefix_ReturnsFailure()
    {
        var attr = new ValidateStartsWithAttribute("Hello");
        var result = attr.Validate("Goodbye World");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateStartsWithAttribute("Hello");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateStartsWithAttribute("Pre") { Message = "Must start with Pre" };
        var result = attr.Validate("Other");
        Assert.That(result.ExceptionMessage, Does.Contain("Must start with Pre"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateStartsWithAttribute("X");
        var result = attr.Validate("Y", "Code");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Code"));
    }
}
