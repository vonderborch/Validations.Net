namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNotSameAsAttributeTests
{
    [Test]
    public void Validate_WhenDifferentReference_ReturnsSuccess()
    {
        var obj = new object();
        var attr = new ValidateIsNotSameAsAttribute(obj);
        var result = attr.Validate(new object());
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenSameReference_ReturnsFailure()
    {
        var obj = new object();
        var attr = new ValidateIsNotSameAsAttribute(obj);
        var result = attr.Validate(obj);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsNotSameAs.ValidatorName));
    }

    [Test]
    public void Validate_WhenValueNullAndOtherNotNull_ReturnsSuccess()
    {
        var obj = new object();
        var attr = new ValidateIsNotSameAsAttribute(obj);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenBothNull_ReturnsFailure()
    {
        var attr = new ValidateIsNotSameAsAttribute(null);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var obj = new object();
        var attr = new ValidateIsNotSameAsAttribute(obj) { Message = "Must not be same instance" };
        var result = attr.Validate(obj);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must not be same instance"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var obj = new object();
        var attr = new ValidateIsNotSameAsAttribute(obj);
        var result = attr.Validate(obj, "Clone");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Clone"));
    }
}
