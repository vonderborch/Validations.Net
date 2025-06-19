using Validations.Net.ValidationAttributes;
using Validations.Net.Validators;

namespace Validations.Net.Test;

[TestFixture]
public class TestValidator
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
    
    [Test]
    public void TestNullValidation()
    {
        var testObject = new TestClass { NotNullProperty = 1, GreaterThanZeroObjectProperty = 1, ListProperty = [1] };
        Assert.That(testObject.CheckIsValid(), Is.True);
        Assert.DoesNotThrow(() => testObject.ValidateIsValid(nameof(testObject)));
        
        var testObject2 = new TestClass { NullProperty = 1 };
        Assert.That(testObject2.CheckIsValid(), Is.False);
        Assert.Throws<ValidationException>(() => testObject2.ValidateIsValid(nameof(testObject)));
    }
}
