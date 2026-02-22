namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsNaNAttributeTests
{
    [Test]
    public void Validate_WithNaN_ReturnsSuccess()
    {
        var attr = new ValidateIsNaNAttribute();
        var result = attr.Validate(double.NaN);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithFiniteDouble_ReturnsFailure()
    {
        var attr = new ValidateIsNaNAttribute();
        var result = attr.Validate(1.0);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithInfinity_ReturnsFailure()
    {
        var attr = new ValidateIsNaNAttribute();
        var result = attr.Validate(double.PositiveInfinity);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNaNAttribute { Message = "Must be NaN" };
        var result = attr.Validate(1.0);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be NaN"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsNaNAttribute();
        var result = attr.Validate(1.0, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
