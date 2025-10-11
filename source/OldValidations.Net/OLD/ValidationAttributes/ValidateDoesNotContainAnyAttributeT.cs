using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection does not contain any of the specified items or satisfy any of the
///     specified predicates.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAnyAttribute<T> : ValidationAttribute
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
    ///     Initializes a new instance of the ValidateDoesNotContainAnyAttribute class with a collection of items.
    /// </summary>
    /// <param name="items">The items to search for.</param>
    public ValidateDoesNotContainAnyAttribute(params T[] items) : base("DoesNotContainAny")
    {
        this.Items = items;
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAnyAttribute class with a collection of predicate functions
    ///     from a specified group.
    /// </summary>
    /// <param name="predicateGroup">The group containing the predicate functions.</param>
    /// <param name="predicateNames">The names of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesNotContainAnyAttribute(string predicateGroup, params string[] predicateNames) : base(
        "DoesNotContainAny")
    {
        this.Predicates = new List<(string name, string? group)>();
        foreach (var name in predicateNames)
        {
            this.Predicates.Add((name, predicateGroup));
        }
    }

    /// <summary>
    ///     Initializes a new instance of the ValidateDoesNotContainAnyAttribute class with a collection of predicate
    ///     functions.
    /// </summary>
    /// <param name="predicates">The names and groups of the predicate functions to use.</param>
    /// <exception cref="ValidationException">Thrown when a predicate function is not found.</exception>
    public ValidateDoesNotContainAnyAttribute(params (string name, string group)[] predicates) : base(
        "DoesNotContainAny")
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
    /// Determines whether the specified value satisfies the validation rules, which ensure
    /// the collection does not contain any forbidden items or matching elements based on predicates.
    /// </summary>
    /// <param name="value">The value to validate, expected to be a collection of the specified type.</param>
    /// <param name="instance">The object containing the member being validated, used for context if needed.</param>
    /// <returns>True if the value adheres to the validation rules; otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.Predicates is null)
        {
            return true;
            //return typedValue.ConvertedValue.CheckDoesNotContainAny(this.Items!);
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
        return typedValue.ConvertedValue.CheckDoesNotContainAny(this._predicates!);
    }

    /// <summary>
    /// Validates the given value against specified criteria in a validation attribute context.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The object instance containing the property or field being validated.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context during validation.</param>
    /// <returns>A ValidationResult indicating whether the validation was successful or failed.</returns>
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
            //return typedValue.ConvertedValue.ValidateDoesNotContainAny(this.Items!, propertyName, blackboard);
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
        return typedValue.ConvertedValue.ValidateDoesNotContainAny(this._predicates!, propertyName, blackboard);
    }
}
