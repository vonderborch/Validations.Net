using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesNotContainAnyAttribute<T> : ValidationAttribute
{
    public ICollection<T>? Items { get; init; } = null;

    public ICollection<Func<T, bool>>? Predicates { get; init; } = null;

    public ValidateDoesNotContainAnyAttribute(params T[] items) : base("DoesNotContainAny")
    {
        Items = items;
    }
    
    public ValidateDoesNotContainAnyAttribute(string predicateGroup, params string[] predicateNames) : base("DoesNotContainAny")
    {
        Predicates = new List<Func<T, bool>>();
        foreach (var name in predicateNames)
        {
            var method = PredicateRegistrar.GetPredicate<T>(name, predicateGroup);
            method.ValidateIsNotNull(name);
            Predicates.Add(method!);
        }
    }
    
    public ValidateDoesNotContainAnyAttribute(params (string name, string group)[] predicates) : base("DoesNotContainAny")
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
            return collection.CheckDoesNotContainAny(Items!);
        }
        else
        {
            return collection.CheckDoesNotContainAny(Predicates);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicates is null)
        {
            collection.ValidateDoesNotContainAny(Items!, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesNotContainAny(Predicates, propertyName, blackboard);
        }
    }
}
