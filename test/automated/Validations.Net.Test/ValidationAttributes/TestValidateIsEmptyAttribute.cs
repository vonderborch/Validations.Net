using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class IsEmptyTestClass
{
    [ValidateIsEmpty<int>]
    public List<int>? Values { get; set; }
}

public class IsEmptyStringTestClass
{
    [ValidateIsEmpty<string>]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsEmptyAttribute
{
    [Test]
    public void ValidateIsEmptyAttribute_Valid()
    {
        var valid = new IsEmptyTestClass { Values = new List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEmptyAttribute_Invalid()
    {
        var invalid = new IsEmptyTestClass { Values = new List<int> { 1 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEmptyAttribute_String_Valid()
    {
        var valid = new IsEmptyStringTestClass { Value = string.Empty };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEmptyAttribute_String_Invalid()
    {
        var invalid = new IsEmptyStringTestClass { Value = "abc" };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEmptyAttribute_String_Null()
    {
        var nullVal = new IsEmptyStringTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsEmpty"));
    }

    [Test]
    public void ValidateIsEmptyAttribute_Collection_Null()
    {
        var nullVal = new IsEmptyTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsEmpty"));
    }

    [Test]
    public void ValidateIsEmptyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsEmptyAttribute<int>();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
