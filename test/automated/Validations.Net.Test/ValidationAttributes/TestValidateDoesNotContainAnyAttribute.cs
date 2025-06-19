using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.ValidationAttributes.Helpers;
using System.Collections.Generic;
using Validations.Net.Test.ValidationAttributes;

namespace Validations.Net.Test.ValidationAttributes;

public class DoesNotContainAnyTestClass
{
    [ValidateDoesNotContainAny<int>(1, 2)]
    public List<int> Values { get; set; } = new();
}

public class DoesNotContainAnyTestClassWithPredicate
{
    [ValidateDoesNotContainAny<int>(null, "IsOdd")]
    public List<int> Values { get; set; } = new();
}

public class DoesNotContainAnyStringTestClass
{
    [ValidateDoesNotContainAny<string>("foo", "bar")]
    public List<string> Values { get; set; } = new();
}

public class DoesNotContainAnyCustomTestClass
{
    [ValidateDoesNotContainAny<CustomComparable>(null, "IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesNotContainAnyAttribute
{
    [Test]
    public void ValidateDoesNotContainAnyAttribute_Valid()
    {
        var valid = new DoesNotContainAnyTestClass { Values = new List<int> { 3, 4 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Invalid()
    {
        var invalid = new DoesNotContainAnyTestClass { Values = new List<int> { 1, 3 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Predicate_ValidAndInvalid()
    {
        // Register a predicate for demonstration
        PredicateCache<int>.Cache.GlobalPredicates["IsOdd"] = x => x % 2 != 0;

        var valid = new DoesNotContainAnyTestClassWithPredicate { Values = new List<int> { 2, 4 } };
        var invalid = new DoesNotContainAnyTestClassWithPredicate { Values = new List<int> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_String_Valid()
    {
        var valid = new DoesNotContainAnyStringTestClass { Values = new List<string> { "baz", "qux" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_String_Invalid()
    {
        var invalid = new DoesNotContainAnyStringTestClass { Values = new List<string> { "foo", "baz" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAnyAttribute<int>(1, 2);
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Empty_Valid()
    {
        var valid = new DoesNotContainAnyTestClass { Values = new List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_AllMatch_Invalid()
    {
        var invalid = new DoesNotContainAnyTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_MultipleMatches_Invalid()
    {
        var invalid = new DoesNotContainAnyTestClass { Values = new List<int> { 1, 1, 2, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAnyAttribute<int>(1, 2);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("must be of type"));
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new DoesNotContainAnyCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        var invalid = new DoesNotContainAnyCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAnyAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesNotContainAnyAttribute<int>(null, "NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<int> { 1, 2 }, null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContainAny"));
    }
} 
