namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateIsFiniteAttributeTests
{
    [Test]
    public void Validate_WithFiniteDouble_ReturnsSuccess()
    {
        var attr = new ValidateIsFiniteAttribute();
        var result = attr.Validate(1.0);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInfinity_ReturnsFailure()
    {
        var attr = new ValidateIsFiniteAttribute();
        var result = attr.Validate(double.PositiveInfinity);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithNaN_ReturnsFailure()
    {
        var attr = new ValidateIsFiniteAttribute();
        var result = attr.Validate(double.NaN);
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsFiniteAttribute { Message = "Must be finite" };
        var result = attr.Validate(double.PositiveInfinity);
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be finite"));
    }

    [Test]
    public void Validate_WithMemberName_IncludesMemberNameInResult()
    {
        var attr = new ValidateIsFiniteAttribute();
        var result = attr.Validate(double.PositiveInfinity, "MyProperty");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("MyProperty"));
    }
}
