using System.Linq.Expressions;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAttribute<T> : ValidationAttribute
{
    public T? Item { get; init; } = default;

    public Func<T, bool>? Predicate { get; init; } = null;
    
    public ValidateDoesContainAttribute(T item) : base("DoesContain")
    {
        Item = item;
    }

    public ValidateDoesContainAttribute(string predicateName, string predicateGroup = "default") : base("DoesContain")
    {
        // Find a method/delegate with the specified name in all loaded assemblies
        var method =
            PredicateRegistrar.GetPredicate<T>(predicateName, predicateGroup);

        method.ValidateIsNotNull(predicateName);
        Predicate = method!;
    }

    public override bool Check(object? value)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        return Predicate is null ? collection.CheckDoesContain(Item) : collection.CheckDoesContain(Predicate);
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicate is null)
        {
            collection.ValidateDoesContain(Item, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesContain(Predicate, propertyName, blackboard);
        }
    }
}
