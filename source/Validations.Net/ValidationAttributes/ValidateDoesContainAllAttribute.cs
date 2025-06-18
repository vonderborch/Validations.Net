using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAllAttribute<T> : ValidationAttribute
{
    public ICollection<T>? Items { get; init; } = null;

    public ICollection<Func<T, bool>>? Predicates { get; init; } = null;

    public ValidateDoesContainAllAttribute(params T[] items) : base("DoesContainAll")
    {
        Items = items;
    }

    public ValidateDoesContainAllAttribute(string predicateGroup, params string[] predicateNames) : base("DoesContainAll")
    {
        Predicates = new List<Func<T, bool>>();
        foreach (var name in predicateNames)
        {
            var method = PredicateRegistrar.GetPredicate<T>(name, predicateGroup);
            method.ValidateIsNotNull(name);
            Predicates.Add(method!);
        }
    }

    public ValidateDoesContainAllAttribute(params (string name, string group)[] predicates) : base("DoesContainAll")
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
            return collection.CheckDoesContainAll(Items!);
        }
        else
        {
            return collection.CheckDoesContainAll(Predicates);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicates is null)
        {
            collection.ValidateDoesContainAll(Items!, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesContainAll(Predicates, propertyName, blackboard);
        }
    }
}
