using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsValidXmlTests
{
    [Test]
    public void CheckIsValidXml_WithValidXml_ReturnsTrue()
    {
        Assert.That("<root/>".CheckIsValidXml(), Is.True);
        Assert.That("<root><child/></root>".CheckIsValidXml(), Is.True);
    }

    [Test]
    public void CheckIsValidXml_WithInvalidXml_ReturnsFalse()
    {
        Assert.That("<root>".CheckIsValidXml(), Is.False);
        Assert.That("not xml".CheckIsValidXml(), Is.False);
    }

    [Test]
    public void CheckIsValidXml_WithNullOrEmpty_ReturnsFalse()
    {
        Assert.That(((string?)null).CheckIsValidXml(), Is.False);
        Assert.That(string.Empty.CheckIsValidXml(), Is.False);
    }

    [Test]
    public void ValidateIsValidXml_WithValidXml_ReturnsValid()
    {
        var result = "<root/>".ValidateIsValidXml();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsValidXml_WithInvalidXml_ReturnsInvalid()
    {
        var result = "<unclosed".ValidateIsValidXml();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsValidXml_WithInvalidXml_Throws()
    {
        Assert.Throws<ValidationException>(() => "invalid".EnsureIsValidXml());
    }
}
