using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAllAttribute<T> : ValidationAttribute
{
    public ICollection<T>? Items { get; init; } = null;

    public ICollection<Func<T, bool>>? Predicates { get; init; } = null;

    public ValidateDoesNotContainAllAttribute(params T[] items) : base("DoesNotContainAll")
    {
        Items = items;
    }

    public ValidateDoesNotContainAllAttribute(string predicateGroup, params string[] predicateNames) : base("DoesNotContainAll")
    {
        Predicates = new List<Func<T, bool>>();
        foreach (var name in predicateNames)
        {
            var method = PredicateRegistrar.GetPredicate<T>(name, predicateGroup);
            method.ValidateIsNotNull(name);
            Predicates.Add(method!);
        }
    }

    public ValidateDoesNotContainAllAttribute(params (string name, string group)[] predicates) : base("DoesNotContainAll")
    {
        Predicates = new List<Func<T, bool>>();
        foreach (var predicate in predicates)
        {
            var method = PredicateRegistrar.GetPredicate<T>(predicate.name, predicate.group);
            method.ValidateIsNotNull(predicate.name);
            Predicates.Add(method!);
        }
    }

    public override bool Check(object? value)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicates is null)
        {
            return collection.CheckDoesNotContainAll(Items!);
        }
        else
        {
            return collection.CheckDoesNotContainAll(Predicates);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicates is null)
        {
            collection.ValidateDoesNotContainAll(Items!, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesNotContainAll(Predicates, propertyName, blackboard);
        }
    }
}
