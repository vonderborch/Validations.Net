namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateDoesNotEndWithAttributeTests
{
    [Test]
    public void Validate_WithStringNotEndingWithSuffix_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotEndWithAttribute(".txt");
        var result = attr.Validate("document.pdf");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithStringEndingWithSuffix_ReturnsFailure()
    {
        var attr = new ValidateDoesNotEndWithAttribute(".txt");
        var result = attr.Validate("document.txt");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsSuccess()
    {
        var attr = new ValidateDoesNotEndWithAttribute(".txt");
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateDoesNotEndWithAttribute(".txt") { Message = "Must not end with .txt" };
        var result = attr.Validate("file.txt");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not end with .txt"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateDoesNotEndWithAttribute(".txt");
        var result = attr.Validate("file.txt", "FileName");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("FileName"));
    }
}
