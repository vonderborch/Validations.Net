using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.Test.ValidationAttributes;
using System.Collections.Generic;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net.Test.ValidationAttributes;

public class DoesContainAllIntTestClass
{
    [ValidateDoesContainAll<int>(1, 2)]
    public List<int> Values { get; set; } = new();
}

public class DoesContainAllStringTestClass
{
    [ValidateDoesContainAll<string>("foo", "bar")]
    public List<string> Values { get; set; } = new();
}

public class DoesContainAllCustomTestClass
{
    [ValidateDoesContainAll<CustomComparable>("IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesContainAllAttribute
{
    [Test]
    public void ValidateDoesContainAllAttribute_Int_Valid()
    {
        var valid = new DoesContainAllIntTestClass { Values = new List<int> { 1, 2, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Int_Invalid()
    {
        var invalid = new DoesContainAllIntTestClass { Values = new List<int> { 1, 3 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_String_Valid()
    {
        var valid = new DoesContainAllStringTestClass { Values = new List<string> { "foo", "bar", "baz" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_String_Invalid()
    {
        var invalid = new DoesContainAllStringTestClass { Values = new List<string> { "foo", "baz" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAllAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("DoesContainAll"));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Empty_Invalid()
    {
        var invalid = new DoesContainAllIntTestClass { Values = new List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_AllMatch_Valid()
    {
        var valid = new DoesContainAllIntTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_SomeMatch_Invalid()
    {
        var invalid = new DoesContainAllIntTestClass { Values = new List<int> { 1, 3 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAllAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new DoesContainAllCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        var invalid = new DoesContainAllCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAllAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesContainAllAttribute<CustomComparable>("NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<CustomComparable> { new CustomComparable(1) }, null));
        Assert.That(ex!.Message, Does.Contain("DoesContainAll"));
    }
} 
