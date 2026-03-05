using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsSameAsAttributeTests
{
    [Test]
    public void Validate_WhenSameReference_ReturnsSuccess()
    {
        var obj = new object();
        var attr = new ValidateIsSameAsAttribute(obj);
        var result = attr.Validate(obj);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WhenDifferentReference_ReturnsFailure()
    {
        var obj = new object();
        var attr = new ValidateIsSameAsAttribute(obj);
        var result = attr.Validate(new object());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain(IsSameAs.ValidatorName));
    }

    [Test]
    public void Validate_WhenValueNullAndOtherNotNull_ReturnsFailure()
    {
        var obj = new object();
        var attr = new ValidateIsSameAsAttribute(obj);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WhenBothNull_ReturnsSuccess()
    {
        var attr = new ValidateIsSameAsAttribute(null);
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessageOnFailure()
    {
        var obj = new object();
        var attr = new ValidateIsSameAsAttribute(obj) { Message = "Must be same instance" };
        var result = attr.Validate(new object());
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.Message, Does.Contain("Must be same instance"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var obj = new object();
        var attr = new ValidateIsSameAsAttribute(obj);
        var result = attr.Validate(new object(), "Reference");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Reference"));
    }
}
