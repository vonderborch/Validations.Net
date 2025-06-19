using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.Test.ValidationAttributes;

namespace Validations.Net.Test.ValidationAttributes;

public class IsEqualsTestClass
{
    [ValidateIsEqualsAttribute<int>(42)]
    public int Value { get; set; }
}

public class IsEqualsStringTestClass
{
    [ValidateIsEqualsAttribute<string>("hello")]
    public string? Value { get; set; }
}

public class IsEqualsCustomTestClass
{
    public CustomComparable? Value { get; set; }
}

[TestFixture]
public class TestValidateIsEqualsAttribute
{
    [Test]
    public void ValidateIsEqualsAttribute_Valid()
    {
        var valid = new IsEqualsTestClass { Value = 42 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsAttribute_Invalid()
    {
        var invalid = new IsEqualsTestClass { Value = 41 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEqualsAttribute_String_Valid()
    {
        var valid = new IsEqualsStringTestClass { Value = "hello" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsAttribute_String_Invalid()
    {
        var invalid = new IsEqualsStringTestClass { Value = "world" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEqualsAttribute_Custom_Valid()
    {
        var attr = new ValidateIsEqualsAttribute<CustomComparable>(new CustomComparable(5));
        Assert.That(attr.Check(new CustomComparable(5), null), Is.True);
    }

    [Test]
    public void ValidateIsEqualsAttribute_Custom_Invalid()
    {
        var attr = new ValidateIsEqualsAttribute<CustomComparable>(new CustomComparable(5));
        Assert.That(attr.Check(new CustomComparable(6), null), Is.False);
        Assert.Throws<ValidationException>(() => attr.Validate(new CustomComparable(6), null, "Value"));
    }

    [Test]
    public void ValidateIsEqualsAttribute_Null_Invalid()
    {
        var invalid = new IsEqualsStringTestClass { Value = null };
        Assert.That(invalid.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
        Assert.That(ex!.Message, Does.Contain("IsEquals"));
    }

    [Test]
    public void ValidateIsEqualsAttribute_WithComparer_Valid()
    {
        var attr = new ValidateIsEqualsAttribute<string>("abc", StringComparer.OrdinalIgnoreCase);
        Assert.That(attr.Check("ABC", null), Is.True);
    }

    [Test]
    public void ValidateIsEqualsAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsEqualsAttribute<int>(5);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("IsEquals"));
    }
} 