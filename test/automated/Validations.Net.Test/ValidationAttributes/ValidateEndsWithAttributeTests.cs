using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateEndsWithAttributeTests
{
    [Test]
    public void Validate_WithStringEndingWithSuffix_ReturnsSuccess()
    {
        var attr = new ValidateEndsWithAttribute(".txt");
        var result = attr.Validate("document.txt");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringNotEndingWithSuffix_ReturnsFailure()
    {
        var attr = new ValidateEndsWithAttribute(".txt");
        var result = attr.Validate("document.pdf");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateEndsWithAttribute(".txt");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateEndsWithAttribute(".txt") { Message = "Must end with .txt" };
        var result = attr.Validate("file.pdf");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must end with .txt"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateEndsWithAttribute(".txt");
        var result = attr.Validate("file.pdf", "FileName");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("FileName"));
    }
}
