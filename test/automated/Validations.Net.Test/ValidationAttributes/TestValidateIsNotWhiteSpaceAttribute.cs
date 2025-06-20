using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotWhiteSpaceTestClass
{
    [ValidateIsNotWhiteSpace]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotWhiteSpaceAttribute
{
    [Test]
    public void ValidateIsNotWhiteSpaceAttribute_Valid()
    {
        var valid = new IsNotWhiteSpaceTestClass { Value = "abc" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotWhiteSpaceAttribute_Invalid()
    {
        var invalid = new IsNotWhiteSpaceTestClass { Value = "   \t\n" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotWhiteSpaceAttribute_EmptyString()
    {
        var empty = new IsNotWhiteSpaceTestClass { Value = string.Empty };
        Assert.That(empty.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateIsNotWhiteSpaceAttribute_Null()
    {
        var nullVal = new IsNotWhiteSpaceTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotWhiteSpace"));
    }

    [Test]
    public void ValidateIsNotWhiteSpaceAttribute_NonString_Throws()
    {
        var attr = new ValidateIsNotWhiteSpaceAttribute();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
