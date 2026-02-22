namespace Validations.Net.Test.ValidationAttributes;

[TestFixture]
public class ValidateNestedAttributeTests
{
    [Test]
    public void Validate_AlwaysReturnsSuccess()
    {
        var attr = new ValidateNestedAttribute();
        var result = attr.Validate(null);
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValue_ReturnsSuccess()
    {
        var attr = new ValidateNestedAttribute();
        var result = attr.Validate("any");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithObject_ReturnsSuccess()
    {
        var attr = new ValidateNestedAttribute();
        var result = attr.Validate(new object());
        Assert.That(result.IsValid, Is.True);
    }
}
