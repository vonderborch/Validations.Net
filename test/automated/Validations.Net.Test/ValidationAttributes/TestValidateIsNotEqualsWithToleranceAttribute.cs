using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class IsNotEqualsWithToleranceIntTestClass
{
    [ValidateIsNotEqualsWithToleranceAttribute<int>(100, 5)]
    public int Value { get; set; }
}

public class IsNotEqualsWithToleranceDoubleTestClass
{
    [ValidateIsNotEqualsWithToleranceAttribute<double>(10.0, 0.5)]
    public double Value { get; set; }
}

[TestFixture]
public class TestValidateIsNotEqualsWithToleranceAttribute
{
    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Int_Exact_Invalid()
    {
        var invalid = new IsNotEqualsWithToleranceIntTestClass { Value = 100 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Int_WithinTolerance_Invalid()
    {
        var invalid = new IsNotEqualsWithToleranceIntTestClass { Value = 104 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Int_OutsideTolerance_Valid()
    {
        var valid = new IsNotEqualsWithToleranceIntTestClass { Value = 106 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Double_Exact_Invalid()
    {
        var invalid = new IsNotEqualsWithToleranceDoubleTestClass { Value = 10.0 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Double_WithinTolerance_Invalid()
    {
        var invalid = new IsNotEqualsWithToleranceDoubleTestClass { Value = 10.4 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Double_OutsideTolerance_Valid()
    {
        var valid = new IsNotEqualsWithToleranceDoubleTestClass { Value = 10.6 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_Null_Valid()
    {
        var attr = new ValidateIsNotEqualsWithToleranceAttribute<int>(100, 5);
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateIsNotEqualsWithToleranceAttribute_InvalidType_Throws()
    {
        var attr = new ValidateIsNotEqualsWithToleranceAttribute<int>(100, 5);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not an int", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }
} 
