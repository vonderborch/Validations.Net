namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsValidXmlAttributeTests
{
    [Test]
    public void Validate_WithValidXml_ReturnsSuccess()
    {
        var attr = new ValidateIsValidXmlAttribute();
        var result = attr.Validate("<root/>");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithInvalidXml_ReturnsFailure()
    {
        var attr = new ValidateIsValidXmlAttribute();
        var result = attr.Validate("not xml");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithMalformedXml_ReturnsFailure()
    {
        var attr = new ValidateIsValidXmlAttribute();
        var result = attr.Validate("<root>");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsValidXmlAttribute { Message = "Must be valid XML" };
        var result = attr.Validate("invalid");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must be valid XML"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsValidXmlAttribute();
        var result = attr.Validate("invalid", "Document");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Document"));
    }
}
