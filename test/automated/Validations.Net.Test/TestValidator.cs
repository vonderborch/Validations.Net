using Validations.Net.ValidationAttributes;

namespace Validations.Net.Test;

[TestFixture]
public class TestValidator
{
    private class TestClass
    {
        [ValidateAgainstPredicate<object>("GreaterThanZero")]
        public object? NullProperty { get; set; }

        [RegisterAgainstPredicateValidationFunction("GreaterThanZero")]
        public Func<int, bool> GreaterThanZero => x => x > 0;

        [ValidateIsNotNull]
        public object NotNullProperty { get; set; } = new object();
        
        public List<int> ListProperty { get; set; } = new List<int>();
    }
    
    [Test]
    public void TestNullValidation()
    {
        var testObject = new TestClass { NotNullProperty = 1 };
        Assert.That(testObject.CheckIsValidClass(), Is.True);
        Assert.DoesNotThrow(() => testObject.ValidateIsValidClass());
        
        var testObject2 = new TestClass { NullProperty = 1 };
        Assert.That(testObject2.CheckIsValidClass(), Is.False);
        Assert.Throws<ValidationException>(() => testObject2.ValidateIsValidClass());
    }
}
