using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net.ValidationAttributes.Helpers;
using System.Collections.Generic;
using Validations.Net.Test.ValidationAttributes;

namespace Validations.Net.Test.ValidationAttributes;

public class DoesNotContainTestClass
{
    [ValidateDoesNotContain<int>(1)]
    public List<int> Values { get; set; } = new();
}

public class DoesNotContainTestClassWithPredicate
{
    [ValidateDoesNotContain<int>("IsOdd")]
    public List<int> Values { get; set; } = new();
}

public class DoesNotContainStringTestClass
{
    [ValidateDoesNotContain<string>("foo")]
    public List<string> Values { get; set; } = new();
}

public class DoesNotContainCustomTestClass
{
    [ValidateDoesNotContain<CustomComparable>("IsSpecial")]
    public List<CustomComparable> Values { get; set; } = new();
}

[TestFixture]
public class TestValidateDoesNotContainAttribute
{
    [Test]
    public void ValidateDoesNotContainAttribute_Valid()
    {
        var valid = new DoesNotContainTestClass { Values = new List<int> { 2, 3 } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Invalid()
    {
        var invalid = new DoesNotContainTestClass { Values = new List<int> { 1, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Predicate_ValidAndInvalid()
    {
        // Register a predicate for demonstration
        PredicateCache<int>.Cache.GlobalPredicates["IsOdd"] = x => x % 2 != 0;

        var valid = new DoesNotContainTestClassWithPredicate { Values = new List<int> { 2, 4 } };
        var invalid = new DoesNotContainTestClassWithPredicate { Values = new List<int> { 1, 2 } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_String_Valid()
    {
        var valid = new DoesNotContainStringTestClass { Values = new List<string> { "bar", "baz" } };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_String_Invalid()
    {
        var invalid = new DoesNotContainStringTestClass { Values = new List<string> { "foo", "bar" } };
        Assert.That(invalid.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalid.ValidateIsValid(nameof(invalid)));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Null_Valid()
    {
        var attr = new ValidateDoesNotContainAttribute<int>(1);
        Assert.That(attr.Check(null, null), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Empty_Valid()
    {
        var valid = new DoesNotContainTestClass { Values = new List<int>() };
        Assert.That(valid.CheckIsValid(), Is.True);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_MultipleMatches_Invalid()
    {
        var invalid = new DoesNotContainTestClass { Values = new List<int> { 1, 1, 2 } };
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_InvalidType_Throws()
    {
        var attr = new ValidateDoesNotContainAttribute<int>(1);
        var ex = Assert.Throws<ValidationException>(() => attr.Check("not a list", null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContain"));
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Predicate_CustomClass_ValidAndInvalid()
    {
        PredicateCache<CustomComparable>.Cache.GlobalPredicates["IsSpecial"] = x => x.X == 42;
        var valid = new DoesNotContainCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(1) } };
        var invalid = new DoesNotContainCustomTestClass { Values = new List<CustomComparable> { new CustomComparable(42) } };
        Assert.That(valid.CheckIsValid(), Is.True);
        Assert.That(invalid.CheckIsValid(), Is.False);
    }

    [Test]
    public void ValidateDoesNotContainAttribute_Predicate_NotRegistered_Throws()
    {
        var attr = new ValidateDoesNotContainAttribute<int>("NotRegistered");
        var ex = Assert.Throws<ValidationException>(() => attr.Check(new List<int> { 1, 2 }, null));
        Assert.That(ex!.Message, Does.Contain("DoesNotContain"));
    }
} 