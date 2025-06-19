using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection contains a specified item or satisfies a specified predicate.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Represents the cached predicate function used to validate a value against custom criteria.
    /// </summary>
    /// <remarks>
    ///     The predicate function is dynamically resolved based on the provided predicate name and group
    ///     during the validation process. It is used to enforce specific rules or constraints on the input value.
    /// </remarks>
    private Func<T, bool>? _predicate;

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a specific item.
    /// </summary>
    /// <param name="item">The item to search for.</param>
    public ValidateDoesContainAttribute(T item) : base("DoesContain")
    {
        this.Item = item;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAttribute class with a predicate function.
    /// </summary>
    /// <param name="predicateName">The name of the predicate function to use.</param>
    /// <param name="predicateGroup">The group containing the predicate function. Default is null.</param>
    /// <exception cref="ValidationException">Thrown when the predicate function is not found.</exception>
    public ValidateDoesContainAttribute(string predicateName, string? predicateGroup = null) : base("DoesContain")
    {
        this.PredicateName = predicateName;
        this.PredicateGroup = predicateGroup;
    }

    /// <summary>
    ///     Gets the item to search for.
    /// </summary>
    public T? Item { get; init; }

    /// <summary>
    ///     Gets or sets the group containing the predicate function used for validation.
    /// </summary>
    public string? PredicateGroup { get; init; }

    /// <summary>
    ///     Gets or sets the name of the predicate function used for validation.
    /// </summary>
    public string? PredicateName { get; init; }

    /// <summary>
    ///     Checks if the provided value contains the specified item or satisfies the specified predicate.
    /// </summary>
    /// <param name="value">The value to check. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value contains the specified item or satisfies the specified predicate, false otherwise.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a collection of type T.</exception>
    public override bool Check(object? value, object? instance)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.PredicateName is null)
        {
            return collection.CheckDoesContain(this.Item);
        }

        if (this._predicate is null)
        {
            this._predicate = GetPredicate<T>(this.PredicateName, this.PredicateGroup, instance);
        }

        return collection.CheckDoesContain(this._predicate!);
    }

    /// <summary>
    ///     Validates if the provided value contains the specified item or satisfies the specified predicate and throws a
    ///     ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value does not contain the specified item or satisfy the
    ///     specified predicate, or when the value is not a collection of type T.
    /// </exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));

        if (this.PredicateName is null)
        {
            collection.ValidateDoesContain(this.Item, propertyName, blackboard);
        }
        else
        {
            if (this._predicate is null)
            {
                this._predicate = GetPredicate<T>(this.PredicateName, this.PredicateGroup, instance);
            }

            collection.ValidateDoesContain(this._predicate!, propertyName, blackboard);
        }
    }
}
