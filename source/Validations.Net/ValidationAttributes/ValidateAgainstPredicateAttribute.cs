using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateAgainstPredicateAttribute<T> : ValidationAttribute
{
    public ValidateAgainstPredicateAttribute(string predicateName, string predicateGroup = "default") : base("AgainstPredicate")
    {
        // Find a method/delegate with the specified name in all loaded assemblies
        var method =
            PredicateRegistrar.GetPredicate<T>(predicateName, predicateGroup);

        method.ValidateIsNotNull(nameof(method));
        Predicate = method!;
    }
    
    public Func<T?, bool> Predicate { get; }
    
    public override bool Check(object? value)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckAgainstPredicate(Predicate);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateAgainstPredicate(Predicate, propertyName, blackboard);
    }
}
