using NUnit.Framework;
using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net;
using System.Collections.Generic;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsNotValid
{
    [ValidateIsNotNull]
    public class TestClass
    {
        [ValidateIsNull]
        public object? NullProperty { get; set; }

        [PredicateRegistration("GreaterThanZero")]
        public bool GreaterThanZero(int x) => x > 0;

        [PredicateRegistration("GreaterThanZeroObject")]
        public static System.Func<object, bool> GreaterThanZeroObject => x => x is not null && (int)x > 0;

        [ValidateIsNotNull]
        public object NotNullProperty { get; set; } = new object();

        [ValidateAgainstPredicate<object>("GreaterThanZeroObject")]
        public object? GreaterThanZeroObjectProperty { get; set; }

        [ValidateDoesContain<int>("GreaterThanZero")]
        public List<int> ListProperty { get; set; } = new List<int>();
    }

    public struct TestStruct
    {
        [ValidateIsGreaterThan<int>(0)]
        public int PositiveValue { get; set; }

        [ValidateIsLessThan<int>(100)]
        public int BoundedValue { get; set; }

        [ValidateIsNotNull]
        public string Description { get; set; }
    }

    [Test]
    public void TestClass_IsNotValid_WhenPropertiesInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = 1, // Should be null
            NotNullProperty = null!, // Should not be null
            GreaterThanZeroObjectProperty = 0, // Should be > 0
            ListProperty = new List<int> { 0, -1 } // Should contain > 0
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_WhenListIsEmptyOrNull()
    {
        var testObjectEmpty = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int>() // Empty list
        };
        Assert.That(testObjectEmpty.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObjectEmpty.ValidateIsValid(nameof(testObjectEmpty)));

        var testObjectNull = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = null! // Null list
        };
        Assert.That(testObjectNull.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObjectNull.ValidateIsValid(nameof(testObjectNull)));
    }

    [Test]
    public void TestStruct_IsNotValid_WhenPropertiesInvalid()
    {
        var invalidStruct = new TestStruct
        {
            PositiveValue = -5, // Should be > 0
            BoundedValue = 150, // Should be < 100
            Description = null! // Should not be null
        };
        Assert.That(invalidStruct.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct.ValidateIsValid(nameof(invalidStruct)));
    }

    [Test]
    public void TestClass_IsNotValid_DefaultInstance()
    {
        var testObject = new TestClass();
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_OnlyNullPropertyInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = 1, // Should be null
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_OnlyNotNullPropertyInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = null!, // Should not be null
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_OnlyGreaterThanZeroObjectPropertyInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 0, // Should be > 0
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_OnlyListPropertyInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { 0, -1 } // Should contain > 0
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_GreaterThanZeroObjectProperty_Boundaries()
    {
        var testObjectZero = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 0, // Edge: zero
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObjectZero.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObjectZero.ValidateIsValid(nameof(testObjectZero)));

        var testObjectNegative = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = -1, // Edge: negative
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObjectNegative.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObjectNegative.ValidateIsValid(nameof(testObjectNegative)));
    }

    [Test]
    public void TestClass_IsNotValid_MultipleFailures()
    {
        var testObject = new TestClass
        {
            NullProperty = 1, // Should be null
            NotNullProperty = null!, // Should not be null
            GreaterThanZeroObjectProperty = 0, // Should be > 0
            ListProperty = new List<int> { 0, -1 } // Should contain > 0
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    public class DerivedTestClass : TestClass
    {
        [ValidateIsNotNull]
        public object ExtraProperty { get; set; } = new object();
    }

    [Test]
    public void TestClass_IsNotValid_Inheritance_DerivedPropertyInvalid()
    {
        var testObject = new DerivedTestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { 1 },
            ExtraProperty = null!
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_IsNotValid_ValidationExceptionDetails()
    {
        var testObject = new TestClass
        {
            NullProperty = 1, // Should be null
            NotNullProperty = null!, // Should not be null
            GreaterThanZeroObjectProperty = 0, // Should be > 0
            ListProperty = new List<int> { 0, -1 } // Should contain > 0
        };
        var ex = Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex!.ParameterName, Is.EqualTo(nameof(testObject)));
        Assert.That(ex.Message, Does.Contain("is not valid").Or.Contain("must be null").Or.Contain("must not be null").Or.Contain("must satisfy predicate").Or.Contain("must contain"));
    }

    [Test]
    public void TestStruct_IsNotValid_OnlyPositiveValueInvalid()
    {
        var invalidStruct = new TestStruct
        {
            PositiveValue = -5, // Should be > 0
            BoundedValue = 50,
            Description = "desc"
        };
        Assert.That(invalidStruct.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct.ValidateIsValid(nameof(invalidStruct)));
    }

    [Test]
    public void TestStruct_IsNotValid_OnlyBoundedValueInvalid()
    {
        var invalidStruct = new TestStruct
        {
            PositiveValue = 10,
            BoundedValue = 150, // Should be < 100
            Description = "desc"
        };
        Assert.That(invalidStruct.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct.ValidateIsValid(nameof(invalidStruct)));
    }

    [Test]
    public void TestStruct_IsNotValid_OnlyDescriptionInvalid()
    {
        var invalidStruct = new TestStruct
        {
            PositiveValue = 10,
            BoundedValue = 50,
            Description = null! // Should not be null
        };
        Assert.That(invalidStruct.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct.ValidateIsValid(nameof(invalidStruct)));
    }
}
