using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

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
    /// Validates whether a given value satisfies the specified containment criteria.
    /// </summary>
    /// <param name="value">The value to validate, expected to be a collection that can contain the specified items.</param>
    /// <param name="instance">The instance containing additional context or state for validation.</param>
    /// <returns>True if the value meets the specified containment conditions; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.Predicates is null)
        {
            return typedValue.ConvertedValue.CheckDoesContainAny(this.Items!);
        }
        
        if (this._predicates is null)
        {
            this._predicates = new List<Func<T, bool>>();
            foreach ((string name, string? group) predicateInfo in this.Predicates)
            {
                if (!GetPredicate(predicateInfo.name, predicateInfo.group, instance, string.Empty, null, out Func<T, bool>? predicate, out _))
                {
                    this._predicates = null;
                    return false;
                }
                this._predicates.Add(predicate!);
            }
        }
        return typedValue.ConvertedValue.CheckDoesContainAny(this._predicates!);
    }

    /// <summary>
    /// Validates the given value using the specified rules and returns a validation result.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The object instance the property belongs to.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">The blackboard object that may contain additional contextual data for validation.</param>
    /// <returns>A <see cref="ValidationResult"/> containing information about the validation outcome.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, propertyName, blackboard);;
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }
        
        if (this.Predicates is null)
        {
            return typedValue.ConvertedValue.ValidateDoesContainAny(this.Items!, propertyName, blackboard);
        }
        
        if (this._predicates is null)
        {
            this._predicates = new List<Func<T, bool>>();
            foreach ((string name, string? group) predicateInfo in this.Predicates)
            {
                if (!GetPredicate(predicateInfo.name, predicateInfo.group, instance, propertyName, blackboard, out Func<T, bool>? predicate, out ValidationException? exception))
                {
                    this._predicates = null;
                    return new ValidationResult(exception!);
                }
                this._predicates.Add(predicate!);
            }
        }
        return typedValue.ConvertedValue.ValidateDoesContainAny(this._predicates!, propertyName, blackboard);
    }
}
