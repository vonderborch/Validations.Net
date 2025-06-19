using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class StringDoesNotContainAllSubstringsTestClass
{
    [ValidateDoesNotContainAllAttribute(StringComparison.Ordinal, "foo", "bar")]
    public string? Value { get; set; }
}

public class StringDoesNotContainAllCharsTestClass
{
    [ValidateDoesNotContainAllAttribute(StringComparison.Ordinal, 'x', 'y')]
    public string? Value { get; set; }
}

[TestFixture]
public class TestStringValidateDoesNotContainAllAttribute
{
    [Test]
    public void ValidateDoesNotContainAllAttribute_Substrings_Valid()
    {
        var valid = new StringDoesNotContainAllSubstringsTestClass { Value = "bazqux" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Substrings_Invalid()
    {
        var invalid = new StringDoesNotContainAllSubstringsTestClass { Value = "foobar" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Chars_Valid()
    {
        var valid = new StringDoesNotContainAllCharsTestClass { Value = "hello" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Chars_Invalid()
    {
        var invalid = new StringDoesNotContainAllCharsTestClass { Value = "xylophone" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Empty_Valid()
    {
        var valid = new StringDoesNotContainAllSubstringsTestClass { Value = string.Empty };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAllAttribute(StringComparison.Ordinal, "foo", "bar");
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAllAttribute(StringComparison.Ordinal, "foo");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContainAll"));
    }

    [Test]
    public void ValidateDoesNotContainAllAttribute_Comparison_Valid()
    {
        var attr = new ValidateDoesNotContainAllAttribute(StringComparison.OrdinalIgnoreCase, "FOO", "BAR");
        Assert.That(attr.Check("bazqux", null), Is.True);
    }
} 