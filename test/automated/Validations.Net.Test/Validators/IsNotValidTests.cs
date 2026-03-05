using Validations.Net.OLD;
using Validations.Net.OLD.Validators;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class IsNotValidTests
{
    #region Test Models

    private class ValidModel
    {
        [ValidateIsNotNull]
        public string Name { get; set; } = "John";
    }

    private class InvalidModel
    {
        [ValidateIsNotNull]
        public string? Name { get; set; }
    }

    #endregion

    #region CheckIsNotValid Tests

    [Test]
    public void CheckIsNotValid_WithNullValue_ReturnsTrue()
    {
        ValidModel? model = null;
        var result = model.CheckIsNotValid();
        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckIsNotValid_WithValidObject_ReturnsFalse()
    {
        var model = new ValidModel { Name = "John" };
        var result = model.CheckIsNotValid();
        Assert.That(result, Is.False);
    }

    [Test]
    public void CheckIsNotValid_WithInvalidObject_ReturnsTrue()
    {
        var model = new InvalidModel { Name = null };
        var result = model.CheckIsNotValid();
        Assert.That(result, Is.True);
    }

    #endregion

    #region ValidateIsNotValid Tests

    [Test]
    public void ValidateIsNotValid_WithNullValue_ReturnsSuccess()
    {
        ValidModel? model = null;
        var result = model.ValidateIsNotValid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValid_WithInvalidObject_ReturnsSuccess()
    {
        var model = new InvalidModel { Name = null };
        var result = model.ValidateIsNotValid();
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateIsNotValid_WithValidObject_ReturnsFailure()
    {
        var model = new ValidModel { Name = "John" };
        var result = model.ValidateIsNotValid();
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.ValidationException, Is.Not.Null);
    }

    #endregion

    #region EnsureIsNotValid Tests

    [Test]
    public void EnsureIsNotValid_WithInvalidObject_DoesNotThrow()
    {
        var model = new InvalidModel { Name = null };
        Assert.DoesNotThrow(() => model.EnsureIsNotValid());
    }

    [Test]
    public void EnsureIsNotValid_WithValidObject_ThrowsValidationException()
    {
        var model = new ValidModel { Name = "John" };
        Assert.Throws<ValidationException>(() => model.EnsureIsNotValid());
    }

    [Test]
    public void EnsureIsNotValid_WithNullValue_DoesNotThrow()
    {
        ValidModel? model = null;
        Assert.DoesNotThrow(() => model.EnsureIsNotValid());
    }

    #endregion
}
