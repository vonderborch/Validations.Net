using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using System;

namespace Validations.Net.Test.ValidationAttributes;

public class NotEqualsWithinIntTestClass
{
    [ValidateIsNotEqualsWithinAttribute<int>(10, 2)]
    public int Value { get; set; }
}

public class NotEqualsWithinDoubleTestClass
{
    [ValidateIsNotEqualsWithinAttribute<double>(10.0, 0.5)]
    public double Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotEqualsWithinAttribute
{
    [Test]
    public void NotEqualsWithin_Int_Valid()
    {
        var valid = new NotEqualsWithinIntTestClass { Value = 13 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void NotEqualsWithin_Int_Invalid()
    {
        var invalid = new NotEqualsWithinIntTestClass { Value = 11 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void NotEqualsWithin_Double_Valid()
    {
        var valid = new NotEqualsWithinDoubleTestClass { Value = 11.0 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void NotEqualsWithin_Double_Invalid()
    {
        var invalid = new NotEqualsWithinDoubleTestClass { Value = 10.2 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void NotEqualsWithin_Null_Invalid()
    {
        var attr = new ValidateIsNotEqualsWithinAttribute<int>(10, 2);
        Assert.That(attr.Check(null, null), Is.False);
    }

    [Test]
    public void NotEqualsWithin_InvalidType_Throws()
    {
        var attr = new ValidateIsNotEqualsWithinAttribute<int>(10, 2);
        Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
    }
} 