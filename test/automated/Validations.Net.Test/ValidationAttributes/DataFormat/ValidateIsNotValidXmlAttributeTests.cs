namespace Validations.Net.Test.ValidationAttributes.DataFormat;

[TestFixture]
public class ValidateIsNotValidXmlAttributeTests
{
    [Test]
    public void Validate_WithInvalidXml_ReturnsSuccess()
    {
        var attr = new ValidateIsNotValidXmlAttribute();
        var result = attr.Validate("not xml");
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void Validate_WithValidXml_ReturnsFailure()
    {
        var attr = new ValidateIsNotValidXmlAttribute();
        var result = attr.Validate("<root/>");
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void Validate_WithCustomMessage_UsesCustomMessage()
    {
        var attr = new ValidateIsNotValidXmlAttribute { Message = "Must not be XML" };
        var result = attr.Validate("<root/>");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ExceptionMessage, Does.Contain("Must not be XML"));
    }

    [Test]
    public void Validate_PropagatesMemberName()
    {
        var attr = new ValidateIsNotValidXmlAttribute();
        var result = attr.Validate("<root/>", "Document");
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException!.ParameterName, Is.EqualTo("Document"));
    }
}
