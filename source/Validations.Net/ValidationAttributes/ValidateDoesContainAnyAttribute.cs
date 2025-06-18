using System.Reflection.Metadata.Ecma335;
using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateDoesContainAnyAttribute<T> : ValidationAttribute
{
    public ICollection<T>? Items { get; init; } = null;

    public ICollection<Func<T, bool>>? Predicates { get; init; } = null;

    public ValidateDoesContainAnyAttribute(params T[] items) : base("DoesContainAny")
    {
        Items = items;
    }
    
    public ValidateDoesContainAnyAttribute(string predicateGroup, params string[] predicateNames) : base("DoesContainAny")
    {
        Predicates = new List<Func<T, bool>>();
        foreach (var name in predicateNames)
        {
            var method = PredicateRegistrar.GetPredicate<T>(name, predicateGroup);
            method.ValidateIsNotNull(name);
            Predicates.Add(method!);
        }
    }
    
    public ValidateDoesContainAnyAttribute(params (string name, string group)[] predicates) : base("DoesContainAny")
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
            return collection.CheckDoesContainAny(Items!);
        }
        else
        {
            return collection.CheckDoesContainAny(Predicates);
        }
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (Predicates is null)
        {
            collection.ValidateDoesContainAny(Items!, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesContainAny(Predicates, propertyName, blackboard);
        }
    }
}
