using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection contains any of the specified items or satisfies any of the specified
///     predicates.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesContainAnyAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Represents a collection of dynamically generated predicates used to validate
    ///     whether a collection satisfies specified conditions.
    /// </summary>
    /// <remarks>
    ///     The predicate functions are dynamically resolved based on the provided predicate name and group
    ///     during the validation process. They is used to enforce specific rules or constraints on the input value.
    /// </remarks>
    private List<Func<T, bool>>? _predicates;

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAnyAttribute class with a collection of items.
    /// </summary>
    /// <param name="items">The items to search for.</param>
    public ValidateDoesContainAnyAttribute(params T[] items) : base("DoesContainAny")
    {
        this.Items = items;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAnyAttribute class with a collection of predicate functions
    ///     from a specified group.
    /// </summary>
    /// <param name="predicateGroup">The group containing the predicate functions.</param>
    /// <param name="predicateNames">The names of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesContainAnyAttribute(string? predicateGroup, params string[] predicateNames) : base(
        "DoesContainAny")
    {
        this.Predicates = new List<(string name, string? group)>();
        foreach (var name in predicateNames)
        {
            this.Predicates.Add((name, predicateGroup));
        }
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesContainAnyAttribute class with a collection of predicate functions.
    /// </summary>
    /// <param name="predicates">The names and groups of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesContainAnyAttribute(params (string name, string group)[] predicates) : base("DoesContainAny")
    {
        this.Predicates = new List<(string name, string? group)>();
        foreach ((string name, string group) predicate in predicates)
        {
            this.Predicates.Add((predicate.name, predicate.group));
        }
    }

    /// <summary>
    ///     Gets the collection of items to search for.
    /// </summary>
    public ICollection<T>? Items { get; init; }

    /// <summary>
    ///     Gets the collection of predicate functions to evaluate against each item.
    /// </summary>
    public ICollection<(string name, string? group)>? Predicates { get; init; }

    /// <summary>
    ///     Checks if the provided value contains any of the specified items or satisfies any of the specified predicates.
    /// </summary>
    /// <param name="value">The value to check. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>
    ///     True if the value contains any of the specified items or satisfies any of the specified predicates, false
    ///     otherwise.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when the value is not a collection of type T.</exception>
    public override bool Check(object? value, object? instance)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.Predicates is null)
        {
            return collection.CheckDoesContainAny(this.Items!);
        }

        if (this._predicates is null)
        {
            this._predicates = new List<Func<T, bool>>();
            foreach ((string name, string? group) predicateInfo in this.Predicates)
            {
                Func<T, bool> predicate = GetPredicate<T>(predicateInfo.name, predicateInfo.group, instance);
                this._predicates.Add(predicate);
            }
        }

        return collection.CheckDoesContainAny(this._predicates!);
    }

    /// <summary>
    ///     Validates if the provided value contains any of the specified items or satisfies any of the specified predicates
    ///     and throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate. Must be a collection of type T.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">
    ///     Thrown when the value does not contain any of the specified items or satisfy any
    ///     of the specified predicates, or when the value is not a collection of type T.
    /// </exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        ICollection<T> collection = GetCorrectType<ICollection<T>>(value, nameof(value));
        if (this.Predicates is null)
        {
            collection.ValidateDoesContainAny(this.Items!, propertyName, blackboard);
        }
        else
        {
            if (this._predicates is null)
            {
                this._predicates = new List<Func<T, bool>>();
                foreach ((string name, string? group) predicateInfo in this.Predicates)
                {
                    Func<T, bool> predicate = GetPredicate<T>(predicateInfo.name, predicateInfo.group, instance);
                    this._predicates.Add(predicate);
                }
            }

            collection.ValidateDoesContainAny(this._predicates!, propertyName, blackboard);
        }
    }
}
