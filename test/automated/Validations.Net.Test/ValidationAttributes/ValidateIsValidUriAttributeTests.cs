namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsValidUriAttributeTests
{
    [Test]
    public void Validate_WithValidUri_ReturnsSuccess()
    {
        var attr = new ValidateIsValidUriAttribute();
        var result = attr.Validate("https://example.com");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidUri_ReturnsFailure()
    {
        var attr = new ValidateIsValidUriAttribute();
        var result = attr.Validate(":::invalid");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNull_ReturnsFailure()
    {
        var attr = new ValidateIsValidUriAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidUriAttribute { Message = "Bad URI" };
        var result = attr.Validate(":::invalid");
        Assert.That(result.ExceptionMessage, Does.Contain("Bad URI"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsValidUriAttribute();
        var result = attr.Validate(":::invalid", "Url");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Url"));
    }
}
