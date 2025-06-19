using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class GreaterThanTestClass
{
    [ValidateIsGreaterThan<int>(5)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsGreaterThanAttribute
{
    [Test]
    public void ValidateIsGreaterThanAttribute_Valid()
    {
        var valid = new GreaterThanTestClass { Value = 6 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsGreaterThanAttribute_Invalid()
    {
        var invalid = new GreaterThanTestClass { Value = 5 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 