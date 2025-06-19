using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringContainsAllSubstringsTestClass
{
    [ValidateDoesContainAllAttribute(StringComparison.Ordinal, "foo", "bar")]
    public string? Value { get; set; }
}

public class StringContainsAllCharsTestClass
{
    [ValidateDoesContainAllAttribute(StringComparison.Ordinal, 'x', 'y')]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesContainAllAttribute
{
    [Test]
    public void ValidateDoesContainAllAttribute_Substrings_Valid()
    {
        var valid = new StringContainsAllSubstringsTestClass { Value = "foo_bar" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Substrings_Invalid()
    {
        var invalid = new StringContainsAllSubstringsTestClass { Value = "fooqux" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Chars_Valid()
    {
        var valid = new StringContainsAllCharsTestClass { Value = "xylophone" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Chars_Invalid()
    {
        var invalid = new StringContainsAllCharsTestClass { Value = "hello" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Empty_Invalid()
    {
        var invalid = new StringContainsAllSubstringsTestClass { Value = string.Empty };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAllAttribute(StringComparison.Ordinal, "foo", "bar");
        Assert.That(attr.Check(null, null), Is.False);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAllAttribute(StringComparison.Ordinal, "foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesContainAllAttribute(StringComparison.OrdinalIgnoreCase, "FOO", "BAR");
        Assert.That(attr.Check("foo_bar", null), Is.True);
    }
} 
