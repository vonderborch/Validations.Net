using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection does not contain a specified item or satisfy a specified predicate.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a specific item.
    /// </summary>
    /// <param name="item">The item to search for.</param>
    public ValidateDoesNotContainAttribute(T item) : base("DoesNotContain")
    {
        this.Item = item;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAttribute class with a predicate function.
    /// </summary>
    /// <param name="predicateName">The name of the predicate function to use.</param>
    /// <param name="predicateGroup">The group containing the predicate function. Default is "default".</param>
    /// <exception cref="ValidationException">Thrown when the predicate function is not found.</exception>
    public ValidateDoesNotContainAttribute(string predicateName, string predicateGroup = "default") : base(
        "DoesNotContain")
    {
        // Find a method/delegate with the specified name in all loaded assemblies
        Func<T, bool>? method =
            PredicateRegistrar.GetPredicate<T>(predicateName, predicateGroup);

        method.ValidateIsNotNull(predicateName);
        this.Predicate = method!;
    }

    /// <summary>
    ///     Gets the item to search for.
    /// </summary>
    public T? Item { get; init; }

    /// <summary>
    ///     Gets the predicate function to evaluate against each item.
    /// </summary>
    public Func<T, bool>? Predicate { get; init; }

    /// <summary>
    ///     Checks if the provided value does not contain the specified item or satisfy the specified predicate.
    /// </summary>
    /// <param name="value">The value to check. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value does not contain the specified item or satisfy the specified predicate, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a collection of type T.</exception>
    public override bool Check(object? value, object? instance)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        return this.Predicate is null
            ? collection.CheckDoesNotContain(this.Item)
            : collection.CheckDoesNotContain(this.Predicate);
    }

    /// <summary>
    ///     Validates if the provided value does not contain the specified item or satisfy the specified predicate and throws a
    ///     ValidationException if it does.
    /// </summary>
    /// <param name="value">The value to validate. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value contains the specified item or satisfies the specified
    ///     predicate, or when the value is not a collection of type T.
    /// </exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.Predicate is null)
        {
            collection.ValidateDoesNotContain(this.Item, propertyName, blackboard);
        }
        else
        {
            collection.ValidateDoesNotContain(this.Predicate, propertyName, blackboard);
        }
    }
}
