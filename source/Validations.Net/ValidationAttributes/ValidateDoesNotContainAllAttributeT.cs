using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
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
    ///     Represents a collection of dynamically generated predicates used to validate
    ///     whether a collection satisfies specified conditions.
    /// </summary>
    /// <remarks>
    ///     The predicate functions are dynamically resolved based on the provided predicate name and group
    ///     during the validation process. They is used to enforce specific rules or constraints on the input value.
    /// </remarks>
    private List<Func<T, bool>>? _predicates;

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
        this.Predicates = new List<(string name, string? group)>();
        foreach (var name in predicateNames)
        {
            this.Predicates.Add((name, predicateGroup));
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
    /// Evaluates whether the given value does not contain all specified items or matches the provided predicate conditions.
    /// </summary>
    /// <param name="value">The value being validated, which is expected to represent a collection.</param>
    /// <param name="instance">The instance containing the value being validated, if applicable.</param>
    /// <returns>Returns true if the validation passes (the value does not contain all the specified items or matching predicates); otherwise, false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.Predicates is null)
        {
            return typedValue.ConvertedValue.CheckDoesNotContainAll(this.Items!);
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
        return typedValue.ConvertedValue.CheckDoesNotContainAll(this._predicates!);
    }

    /// <summary>
    /// Validates the supplied value against the corresponding rules and predicates.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The instance containing the property or field to validate.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">Optional blackboard instance for additional context during validation.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, propertyName, blackboard);;
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }
        
        if (this.Predicates is null)
        {
            return typedValue.ConvertedValue.ValidateDoesNotContainAll(this.Items!, propertyName, blackboard);
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
        return typedValue.ConvertedValue.ValidateDoesNotContainAll(this._predicates!, propertyName, blackboard);
    }
}
