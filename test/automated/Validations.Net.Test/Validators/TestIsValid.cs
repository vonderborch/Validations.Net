using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;
using Validations.Net;

namespace Validations.Net.Test.Validators;

[TestFixture]
public class TestIsValid
{
    [ValidateIsNotNull]
    public class TestClass
    {
        [ValidateIsNull]
        public object? NullProperty { get; set; }

        [PredicateRegistration("GreaterThanZero")]
        public bool GreaterThanZero(int x)
        {
            return x > 0;
        }

        [PredicateRegistration("GreaterThanZeroObject")]
        public static Func<object, bool> GreaterThanZeroObject => x => x is not null && (int)x > 0;

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
    
    public struct TestStructWithPrivateProperty
    {
        public TestStructWithPrivateProperty()
        {
            BoundedValue = 0;
            Description = null;
        }

        [ValidateIsGreaterThan<int>(0)] private int PositiveValue { get; set; } = -10;

        [ValidateIsLessThan<int>(100)]
        public int BoundedValue { get; set; }

        [ValidateIsNotNull]
        public string Description { get; set; }
    }

    public struct TestStructWithPrivateField
    {
        [ValidateIsGreaterThan<int>(0)] 
        private int _positiveValue = -10;

        [ValidateIsLessThan<int>(100)]
        public int BoundedValue { get; set; }

        [ValidateIsNotNull]
        public string Description { get; set; }

        public TestStructWithPrivateField(int positiveValue, int boundedValue, string description)
        {
            _positiveValue = positiveValue;
            BoundedValue = boundedValue;
            Description = description;
        }
    }

    public struct TestStructWithMixedFieldValidations
    {
        [ValidateIsGreaterThan<int>(0)] 
        private int _privatePositiveValue = -10;

        [ValidateIsGreaterThan<int>(0)]
        public int PublicPositiveValue { get; set; }

        [ValidateIsLessThan<int>(100)]
        private int _privateBoundedValue = 150;

        [ValidateIsNotNull]
        public string Description { get; set; }

        public TestStructWithMixedFieldValidations(int privatePositiveValue, int publicPositiveValue, int privateBoundedValue, string description)
        {
            _privatePositiveValue = privatePositiveValue;
            PublicPositiveValue = publicPositiveValue;
            _privateBoundedValue = privateBoundedValue;
            Description = description;
        }
    }

    [Test]
    public void TestClassValidation()
    {
        var testObject = new TestClass { NotNullProperty = 1, GreaterThanZeroObjectProperty = 1, ListProperty = [1] };
        Assert.That(testObject.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => testObject.ValidateIsValid(nameof(testObject)));

        var testObject2 = new TestClass { NullProperty = 1 };
        Assert.That(testObject2.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject2.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestStructValidation()
    {
        // Valid struct with all validations passing
        var validStruct = new TestStruct 
        { 
            PositiveValue = 10, 
            BoundedValue = 50, 
            Description = "Valid struct" 
        };
        Assert.That(validStruct.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => validStruct.ValidateIsValid(nameof(validStruct)));

        // Invalid struct - negative value
        var invalidStruct1 = new TestStruct 
        { 
            PositiveValue = -5, 
            BoundedValue = 50, 
            Description = "Invalid struct" 
        };
        Assert.That(invalidStruct1.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct1.ValidateIsValid(nameof(invalidStruct1)));

        // Invalid struct - value out of bounds
        var invalidStruct2 = new TestStruct 
        { 
            PositiveValue = 5, 
            BoundedValue = 150, 
            Description = "Invalid struct" 
        };
        Assert.That(invalidStruct2.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct2.ValidateIsValid(nameof(invalidStruct2)));

        // Invalid struct - null description
        var invalidStruct3 = new TestStruct 
        { 
            PositiveValue = 5, 
            BoundedValue = 50, 
            Description = null! 
        };
        Assert.That(invalidStruct3.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct3.ValidateIsValid(nameof(invalidStruct3)));
    }

    [Test]
    public void TestStructValidationWithPrivateMembers()
    {
        var validStruct = new TestStructWithPrivateProperty 
        { 
            BoundedValue = 50, 
            Description = "Valid struct" 
        };

        // Test with includePrivateFields and includePrivateProperties parameters
        Assert.That(validStruct.CheckIsValid(includePrivateFields: false, includePrivateProperties: false), Is.True);
        Assert.DoesNotThrow(() => validStruct.ValidateIsValid(
            nameof(validStruct), 
            includePrivateFields: false, 
            includePrivateProperties: false));
        
        Assert.That(validStruct.CheckIsValid(includePrivateFields: true, includePrivateProperties: true), Is.False);
        Assert.Throws<ValidationException>(() => validStruct.ValidateIsValid(
            nameof(validStruct), 
            includePrivateFields: true, 
            includePrivateProperties: true));
    }

    [Test]
    public void TestStructValidationWithPrivateFields()
    {
        // Create struct with valid values for all fields including private field
        var validStruct = new TestStructWithPrivateField(10, 50, "Valid struct");

        // When not including private fields, it should be valid
        Assert.That(validStruct.CheckIsValid(includePrivateFields: false), Is.True);
        Assert.DoesNotThrow(() => validStruct.ValidateIsValid(
            nameof(validStruct), 
            includePrivateFields: false));

        // When including private fields, it should still be valid
        Assert.That(validStruct.CheckIsValid(includePrivateFields: true), Is.True);
        Assert.DoesNotThrow(() => validStruct.ValidateIsValid(
            nameof(validStruct), 
            includePrivateFields: true));

        // Create struct with invalid private field but valid public properties
        var invalidStruct = new TestStructWithPrivateField(-5, 50, "Invalid struct");

        // When not including private fields, it should appear valid
        Assert.That(invalidStruct.CheckIsValid(includePrivateFields: false), Is.True);
        Assert.DoesNotThrow(() => invalidStruct.ValidateIsValid(
            nameof(invalidStruct), 
            includePrivateFields: false));

        // When including private fields, it should be invalid
        Assert.That(invalidStruct.CheckIsValid(includePrivateFields: true), Is.False);
        Assert.Throws<ValidationException>(() => invalidStruct.ValidateIsValid(
            nameof(invalidStruct), 
            includePrivateFields: true));
    }

    [Test]
    public void TestStructMixedPrivatePublicFieldValidations()
    {
        // Case 1: All fields valid
        var allValidStruct = new TestStructWithMixedFieldValidations(10, 20, 50, "Valid struct");
        Assert.That(allValidStruct.CheckIsValid(includePrivateFields: true), Is.True);
        Assert.DoesNotThrow(() => allValidStruct.ValidateIsValid(
            nameof(allValidStruct),
            includePrivateFields: true));

        // Case 2: Only private fields invalid
        var privateInvalidStruct = new TestStructWithMixedFieldValidations(-5, 20, 150, "Mixed invalid struct");

        // Without checking private fields, it appears valid
        Assert.That(privateInvalidStruct.CheckIsValid(includePrivateFields: false), Is.True);
        Assert.DoesNotThrow(() => privateInvalidStruct.ValidateIsValid(
            nameof(privateInvalidStruct),
            includePrivateFields: false));

        // With checking private fields, it's invalid
        Assert.That(privateInvalidStruct.CheckIsValid(includePrivateFields: true), Is.False);
        Assert.Throws<ValidationException>(() => privateInvalidStruct.ValidateIsValid(
            nameof(privateInvalidStruct),
            includePrivateFields: true));

        // Case 3: Only public fields invalid
        var publicInvalidStruct = new TestStructWithMixedFieldValidations(10, -5, 50, "Mixed invalid struct");

        // Always invalid regardless of private field checking
        Assert.That(publicInvalidStruct.CheckIsValid(includePrivateFields: false), Is.False);
        Assert.Throws<ValidationException>(() => publicInvalidStruct.ValidateIsValid(
            nameof(publicInvalidStruct),
            includePrivateFields: false));

        Assert.That(publicInvalidStruct.CheckIsValid(includePrivateFields: true), Is.False);
        Assert.Throws<ValidationException>(() => publicInvalidStruct.ValidateIsValid(
            nameof(publicInvalidStruct),
            includePrivateFields: true));

        // Case 4: Both private and public fields invalid
        var allInvalidStruct = new TestStructWithMixedFieldValidations(-5, -10, 150, "All invalid struct");
        Assert.That(allInvalidStruct.CheckIsValid(includePrivateFields: true), Is.False);
        Assert.Throws<ValidationException>(() => allInvalidStruct.ValidateIsValid(
            nameof(allInvalidStruct),
            includePrivateFields: true));
    }

    [Test]
    public void TestClass_AllPropertiesInvalid()
    {
        var testObject = new TestClass
        {
            NullProperty = 1, // Should be null
            NotNullProperty = null!, // Should not be null
            GreaterThanZeroObjectProperty = 0, // Should be > 0
            ListProperty = new List<int> { -1, 0 } // Should contain > 0
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_OnlyNullPropertyInvalid()
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
    public void TestClass_OnlyNotNullPropertyInvalid()
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
    public void TestClass_OnlyGreaterThanZeroObjectPropertyInvalid()
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
    public void TestClass_OnlyListPropertyInvalid()
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
    public void TestClass_ListProperty_EmptyAndNull()
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
    public void TestClass_GreaterThanZeroObjectProperty_EdgeCases()
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
    public void TestClass_DefaultInstance()
    {
        var testObject = new TestClass();
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_MultipleFailures()
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
    public void TestClass_AllPropertiesValid_MultipleListEntries()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = new object(),
            GreaterThanZeroObjectProperty = 42,
            ListProperty = new List<int> { 1, 2, 3, 100 }
        };
        Assert.That(testObject.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_PartialPropertiesSet_Valid()
    {
        var testObject = new TestClass
        {
            NotNullProperty = new object(),
            GreaterThanZeroObjectProperty = 5
            // NullProperty left unset (should be null)
            // ListProperty left as default (empty, which is invalid)
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_GreaterThanZeroObjectProperty_Boundaries()
    {
        var testObjectMax = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = int.MaxValue,
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObjectMax.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => testObjectMax.ValidateIsValid(nameof(testObjectMax)));

        var testObjectMin = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = int.MinValue,
            ListProperty = new List<int> { 1 }
        };
        Assert.That(testObjectMin.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObjectMin.ValidateIsValid(nameof(testObjectMin)));
    }

    [Test]
    public void TestClass_ListProperty_MixedValues()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { -3, -1, -2, 0 }
        };
        Assert.That(testObject.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_ValidationExceptionDetails()
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
    public void TestClass_PredicateRegistration_StaticAndInstance()
    {
        var testObject = new TestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 2,
            ListProperty = new List<int> { 1, 2, 3 }
        };
        // Should use both static and instance predicate registrations
        Assert.That(testObject.CheckIsValid(), Is.True);
    }

    public class DerivedTestClass : TestClass
    {
        [ValidateIsNotNull]
        public object ExtraProperty { get; set; } = new object();
    }

    [Test]
    public void TestClass_Inheritance_ValidatesBaseAndDerived()
    {
        var testObject = new DerivedTestClass
        {
            NullProperty = null,
            NotNullProperty = 1,
            GreaterThanZeroObjectProperty = 1,
            ListProperty = new List<int> { 1 },
            ExtraProperty = 2
        };
        Assert.That(testObject.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => testObject.ValidateIsValid(nameof(testObject)));
    }

    [Test]
    public void TestClass_Inheritance_DerivedPropertyInvalid()
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
    public void TestClass_ValidationExceptionContext()
    {
        var testObject = new TestClass
        {
            NullProperty = 1,
            NotNullProperty = null!,
            GreaterThanZeroObjectProperty = 0,
            ListProperty = new List<int> { 0, -1 }
        };
        var context = new ValidationExceptionContext();
        var ex = Assert.Throws<ValidationException>(() => testObject.ValidateIsValid(nameof(testObject), false, false, context));
        Assert.That(ex!.Blackboard, Is.SameAs(context));
    }
}
