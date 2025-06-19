using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.ValidationAttributes.Helpers;
using System.Collections.Generic;
using Validations.Net.Test.ValidationAttributes;

namespace Validations.Net.Test.ValidationAttributes;

public class DoesContainAnyTestClass
{
    [ValidateDoesContainAny<int>(1, 2)]
    public List<int> Values { get; set; } = new();
}

public class DoesContainAnyTestClassWithPredicate
{
    [ValidateDoesContainAny<int>(null, "IsEven")]
    public List<int> Values { get; set; } = new();
}

public class DoesContainAnyStringTestClass
{
    [ValidateDoesContainAny<string>("foo", "bar")]
    public List<string> Values { get; set; } = new();
}

public class DoesContainAnyCustomTestClass
{
    [ValidateDoesContainAny<CustomComparable>(null, "IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesContainAnyAttribute
{
    [Test]
    public void ValidateDoesContainAnyAttribute_Valid()
    {
        var valid = new DoesContainAnyTestClass { Values = new List<int> { 2, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Invalid()
    {
        var invalid = new DoesContainAnyTestClass { Values = new List<int> { 3, 4 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Predicate_ValidAndInvalid()
    {
        // Register a predicate for demonstration
        PredicateCache<int>.Cache.GlobalPredicates["IsEven"] = x => x % 2 == 0;

        var valid = new DoesContainAnyTestClassWithPredicate { Values = new List<int> { 2, 3 } };
        var invalid = new DoesContainAnyTestClassWithPredicate { Values = new List<int> { 1, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_String_Valid()
    {
        var valid = new DoesContainAnyStringTestClass { Values = new List<string> { "foo", "baz" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_String_Invalid()
    {
        var invalid = new DoesContainAnyStringTestClass { Values = new List<string> { "baz", "qux" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Null_Invalid()
    {
        var attr = new ValidateDoesContainAnyAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check(null, null));
        Assert.That(ex!.Message, Does.Contain("DoesContainAny"));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Empty_Invalid()
    {
        var invalid = new DoesContainAnyTestClass { Values = new List<int>() };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_AllMatch_Valid()
    {
        var valid = new DoesContainAnyTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_MultipleMatches_Valid()
    {
        var valid = new DoesContainAnyTestClass { Values = new List<int> { 1, 1, 2, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesContainAnyAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new DoesContainAnyCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        var invalid = new DoesContainAnyCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesContainAnyAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesContainAnyAttribute<int>(null, "NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<int> { 1, 2 }, null));
        Assert.That(ex!.Message, Does.Contain("DoesContainAny"));
    }
} 
