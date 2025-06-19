using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test.ValidationAttributes;

public class LessThanOrEqualsTestClass
{
    [ValidateIsLessThanOrEqualsAttribute<int>(5)]
    public int Value { get; set; }
}

[TestFixture]
public class TestValidateIsLessThanOrEqualsAttribute
{
    [Test]
    public void ValidateIsLessThanOrEqualsAttribute_Valid()
    {
        var valid = new LessThanOrEqualsTestClass { Value = 5 };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateIsLessThanOrEqualsAttribute_Invalid()
    {
        var invalid = new LessThanOrEqualsTestClass { Value = 6 };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }
} 