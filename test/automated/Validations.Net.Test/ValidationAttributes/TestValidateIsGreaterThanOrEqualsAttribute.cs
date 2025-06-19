using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class GreaterThanOrEqualsTestClass
{
    [ValidateIsGreaterThanOrEqualsAtrribute<int>(5)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsGreaterThanOrEqualsAttribute
{
    [Test]
    public void ValidateIsGreaterThanOrEqualsAttribute_Valid()
    {
        var valid = new GreaterThanOrEqualsTestClass { Value = 5 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsGreaterThanOrEqualsAttribute_Invalid()
    {
        var invalid = new GreaterThanOrEqualsTestClass { Value = 4 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 
