using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsWhiteSpaceTestClass
{
    [ValidateIsWhiteSpace]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsWhiteSpaceAttribute
{
    [Test]
    public void ValidateIsWhiteSpaceAttribute_Valid()
    {
        var valid = new IsWhiteSpaceTestClass { Value = "   \t\n" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsWhiteSpaceAttribute_Invalid()
    {
        var invalid = new IsWhiteSpaceTestClass { Value = "abc" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsWhiteSpaceAttribute_EmptyString()
    {
        var empty = new IsWhiteSpaceTestClass { Value = string.Empty };
        Assert.That(empty.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => empty.ValidateIsValid(nameof(empty)));
        Assert.That(ex!.Message, Does.Contain("IsWhiteSpace"));
    }

    [Test]
    public void ValidateIsWhiteSpaceAttribute_Null()
    {
        var nullVal = new IsWhiteSpaceTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsWhiteSpace"));
    }

    [Test]
    public void ValidateIsWhiteSpaceAttribute_NonString_Throws()
    {
        var attr = new ValidateIsWhiteSpaceAttribute();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
