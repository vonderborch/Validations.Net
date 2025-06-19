using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class InRangeTestClass
{
    [ValidateIsInRangeAttribute<int>(1, 10)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsInRangeAttribute
{
    [Test]
    public void ValidateIsInRangeAttribute_Valid()
    {
        var valid = new InRangeTestClass { Value = 5 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsInRangeAttribute_Invalid()
    {
        var invalid = new InRangeTestClass { Value = 0 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 