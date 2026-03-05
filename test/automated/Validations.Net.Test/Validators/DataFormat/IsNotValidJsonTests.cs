using Validations.Net.OLD;
using Validations.Net.OLD.Validators.DataFormat;

namespace Validations.Net.Test.Validators.DataFormat;

[TestFixture]
public class IsNotValidJsonTests
{
    [Test]
    public void CheckIsNotValidJson_WithInvalidJson_ReturnsTrue()
    {
        Assert.That("{".CheckIsNotValidJson(), Is.True);
        Assert.That("not json".CheckIsNotValidJson(), Is.True);
    }

    [Test]
    public void CheckIsNotValidJson_WithValidJson_ReturnsFalse()
    {
        Assert.That("{}".CheckIsNotValidJson(), Is.False);
        Assert.That("{\"a\":1}".CheckIsNotValidJson(), Is.False);
    }

    [Test]
    public void ValidateIsNotValidJson_WithInvalidJson_ReturnsValid()
    {
        var result = "invalid".ValidateIsNotValidJson();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValidJson_WithValidJson_ReturnsInvalid()
    {
        var result = "{}".ValidateIsNotValidJson();
        Assert.That(result.IsValid, Is.False);
    }

    [Test]
    public void EnsureIsNotValidJson_WithValidJson_Throws()
    {
        Assert.Throws<ValidationException>(() => "{}".EnsureIsNotValidJson());
    }
}
