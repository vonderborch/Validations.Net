using Validations.Net.Validators.Age;

namespace Validations.Net.Test.ValidationAttributes.Age;

[TestFixture]
public class ValidateIsValidAgeAttributeTests
{
    [Test]
    public void Validate_WithValidAge_ReturnsSuccess()
    {
        var attr = new ValidateIsValidAgeAttribute();
        var result = attr.Validate(25);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithNegativeAge_ReturnsFailure()
    {
        var attr = new ValidateIsValidAgeAttribute();
        var result = attr.Validate(-1);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithAgeOver150_ReturnsFailure()
    {
        var attr = new ValidateIsValidAgeAttribute();
        var result = attr.Validate(200);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidAgeAttribute { Message = "Bad age" };
        var result = attr.Validate(-1);
        Assert.That(result.ExceptionMessage, Does.Contain("Bad age"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsValidAgeAttribute();
        var result = attr.Validate(-1, "UserAge");
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("UserAge"));
    }
}
