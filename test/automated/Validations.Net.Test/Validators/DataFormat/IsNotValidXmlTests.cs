using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsNotValidXmlTests
{
    [Test]
    public void CheckIsNotValidXml_WithInvalidXml_ReturnsTrue()
    {
        Assert.That("<root>".CheckIsNotValidXml(), Is.True);
        Assert.That("not xml".CheckIsNotValidXml(), Is.True);
    }

    [Test]
    public void CheckIsNotValidXml_WithValidXml_ReturnsFalse()
    {
        Assert.That("<root/>".CheckIsNotValidXml(), Is.False);
    }

    [Test]
    public void ValidateIsNotValidXml_WithInvalidXml_ReturnsValid()
    {
        var result = "invalid".ValidateIsNotValidXml();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValidXml_WithValidXml_ReturnsInvalid()
    {
        var result = "<root/>".ValidateIsNotValidXml();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotValidXml_WithValidXml_Throws()
    {
        Assert.Throws<ValidationException>(() => "<root/>".EnsureIsNotValidXml());
    }
}
