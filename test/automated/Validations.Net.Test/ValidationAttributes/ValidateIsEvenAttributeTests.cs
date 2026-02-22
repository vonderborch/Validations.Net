namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsEvenAttributeTests
{
    [Test]
    public void Validate_WithEvenInt_ReturnsSuccess()
    {
        var attr = new ValidateIsEvenAttribute();
        var result = attr.Validate(2);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithZero_ReturnsSuccess()
    {
        var attr = new ValidateIsEvenAttribute();
        var result = attr.Validate(0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithOddInt_ReturnsFailure()
    {
        var attr = new ValidateIsEvenAttribute();
        var result = attr.Validate(1);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsEvenAttribute { Message = "Must be even" };
        var result = attr.Validate(3);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be even"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsEvenAttribute();
        var result = attr.Validate(1, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
