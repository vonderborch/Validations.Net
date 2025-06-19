using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System.Collections.Generic;

namespace Validations.Net.Test.ValidationAttributes;

public class NotEmptyTestClass
{
    [ValidateIsNotEmpty]
    public List<int>? Values { get; set; }
}

public class NotEmptyStringTestClass
{
    [ValidateIsNotEmpty]
    public string? Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotEmptyAttribute
{
    [Test]
    public void ValidateIsNotEmptyAttribute_Valid()
    {
        var valid = new NotEmptyTestClass { Values = new List<int> { 1 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_Invalid()
    {
        var invalid = new NotEmptyTestClass { Values = new List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_String_Valid()
    {
        var valid = new NotEmptyStringTestClass { Value = "abc" };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_String_Empty()
    {
        var empty = new NotEmptyStringTestClass { Value = string.Empty };
        Assert.That(empty.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => empty.ValidateIsValid(nameof(empty)));
        Assert.That(ex!.Message, Does.Contain("IsNotEmpty"));
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_String_Null()
    {
        var nullVal = new NotEmptyStringTestClass { Value = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotEmpty"));
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_Collection_Null()
    {
        var nullVal = new NotEmptyTestClass { Values = null };
        Assert.That(nullVal.CheckIsValid(), Is.False);
        var ex = Assert.Throws<ValidationException>(() => nullVal.ValidateIsValid(nameof(nullVal)));
        Assert.That(ex!.Message, Does.Contain("IsNotEmpty"));
    }

    [Test]
    public void ValidateIsNotEmptyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotEmptyAttribute();
        var ex = Assert.Throws<ValidationException>(() => attr.Check(123, null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
