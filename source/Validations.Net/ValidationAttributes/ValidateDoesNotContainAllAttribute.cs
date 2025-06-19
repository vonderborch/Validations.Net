using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection does not contain all of the specified items or satisfy all of the
///     specified predicates.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAllAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of items.
    /// </summary>
    /// <param name="items">The items to search for.</param>
    public ValidateDoesNotContainAllAttribute(params T[] items) : base("DoesNotContainAll")
    {
        this.Items = items;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of predicate functions
    ///     from a specified group.
    /// </summary>
    /// <param name="predicateGroup">The group containing the predicate functions.</param>
    /// <param name="predicateNames">The names of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesNotContainAllAttribute(string predicateGroup, params string[] predicateNames) : base(
        "DoesNotContainAll")
    {
        this.Predicates = new List<Func<T, bool>>();
        foreach (var name in predicateNames)
        {
            Func<T, bool>? method = PredicateRegistrar.GetPredicate<T>(name, predicateGroup);
            method.ValidateIsNotNull(name);
            this.Predicates.Add(method!);
        }
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAllAttribute class with a collection of predicate
    ///     functions.
    /// </summary>
    /// <param name="predicates">The names and groups of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesNotContainAllAttribute(params (string name, string group)[] predicates) : base(
        "DoesNotContainAll")
    {
        this.Predicates = new List<Func<T, bool>>();
        foreach ((string name, string group) predicate in predicates)
        {
            Func<T, bool>? method = PredicateRegistrar.GetPredicate<T>(predicate.name, predicate.group);
            method.ValidateIsNotNull(predicate.name);
            this.Predicates.Add(method!);
        }
    }

    /// <summary>
    ///     Gets the collection of items to search for.
    /// </summary>
    public ICollection<T>? Items { get; init; }

    /// <summary>
    ///     Gets the collection of predicate functions to evaluate against each item.
    /// </summary>
    public ICollection<Func<T, bool>>? Predicates { get; init; }

    /// <summary>
    ///     Checks if the provided value does not contain all of the specified items or satisfy all of the specified
    ///     predicates.
    /// </summary>
    /// <param name="value">The value to check. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>
    ///     True if the value does not contain all of the specified items or satisfy all of the specified predicates,
    ///     false otherwise.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when the value is not a collection of type T.</exception>
    public override bool Check(object? value, object? instance)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.Predicates is null)
        {
            return collection.CheckDoesNotContainAll(this.Items!);
        }

        return collection.CheckDoesNotContainAll(this.Predicates);
    }

    /// <summary>
    ///     Validates if the provided value does not contain all of the specified items or satisfy all of the specified
    ///     predicates and throws a ValidationException if it does.
    /// </summary>
    /// <param name="value">The value to validate. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value contains all of the specified items or satisfies all of the
    ///     specified predicates, or when the value is not a collection of type T.
    /// </exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.Predicates is null)
        {
            collection.ValidateDoesNotContainAll(this.Items!, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesNotContainAll(this.Predicates, propertyName, blackboard);
        }
    }
}
