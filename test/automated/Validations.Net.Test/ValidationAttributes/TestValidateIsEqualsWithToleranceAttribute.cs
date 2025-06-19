using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsEqualsWithToleranceIntTestClass
{
    [ValidateIsEqualsWithToleranceAttribute<int>(100, 5)]
    public int Value { get; set; }
}

public class IsEqualsWithToleranceDoubleTestClass
{
    [ValidateIsEqualsWithToleranceAttribute<double>(10.0, 0.5)]
    public double Value { get; set; }
}

[TestFixture]
public class TestValidateIsEqualsWithToleranceAttribute
{
    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Int_Exact_Valid()
    {
        var valid = new IsEqualsWithToleranceIntTestClass { Value = 100 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Int_WithinTolerance_Valid()
    {
        var valid = new IsEqualsWithToleranceIntTestClass { Value = 104 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Int_OutsideTolerance_Invalid()
    {
        var invalid = new IsEqualsWithToleranceIntTestClass { Value = 106 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Double_Exact_Valid()
    {
        var valid = new IsEqualsWithToleranceDoubleTestClass { Value = 10.0 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Double_WithinTolerance_Valid()
    {
        var valid = new IsEqualsWithToleranceDoubleTestClass { Value = 10.4 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Double_OutsideTolerance_Invalid()
    {
        var invalid = new IsEqualsWithToleranceDoubleTestClass { Value = 10.6 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_Null_Invalid()
    {
        var attr = new ValidateIsEqualsWithToleranceAttribute<int>(100, 5);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("IsEqualsWithTolerance"));
    }

    [Test]
    public void ValidateIsEqualsWithToleranceAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsEqualsWithToleranceAttribute<int>(100, 5);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
