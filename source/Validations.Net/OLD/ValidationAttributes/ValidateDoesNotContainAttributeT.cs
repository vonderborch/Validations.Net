using SimpleBlackboard.Net;
using Validations.Net.OLD.ValidationAttributes.Helpers;
using Validations.Net.OLD.Validators;

namespace Validations.Net.OLD.ValidationAttributes;

/// <summary>
///     Attribute that validates if a collection does not contain a specified item or satisfy a specified predicate.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateDoesNotContainAttribute<T> : ValidationAttribute
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
    /// Checks whether the provided value adheres to the validation logic defined by this attribute.
    /// </summary>
    /// <param name="value">The object to validate, expected to be of a compatible collection type.</param>
    /// <param name="instance">The instance of the object being validated, used for additional context if needed.</param>
    /// <returns>True if the validation passes, otherwise false.</returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, allowNull: false);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.PredicateName is null)
        {
            return typedValue.ConvertedValue!.CheckDoesNotContain(this.Item);
        }
        
        if (this._predicate is null)
        {
            if (!GetPredicate<T>(this.PredicateName, this.PredicateGroup, instance, string.Empty, null,
                    out Func<T, bool>? predicate, out _))
            {
                return false;
            }

            this._predicate = predicate!;
        }

        return typedValue.ConvertedValue.CheckDoesNotContain(this._predicate!);
    }

    /// <summary>
    /// Validates the specified value against the given criteria defined in the attribute implementation.
    /// </summary>
    /// <param name="value">The value of the property or field being validated.</param>
    /// <param name="instance">The instance of the object containing the property or field.</param>
    /// <param name="propertyName">The name of the property or field being validated.</param>
    /// <param name="blackboard">An optional blackboard containing shared information for validation.</param>
    /// <returns>A ValidationResult indicating the outcome of the validation process.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, allowNull: false);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        if (this.PredicateName is null)
        {
            return typedValue.ConvertedValue!.ValidateDoesNotContain(this.Item, propertyName, blackboard);
        }
        
        if (this._predicate is null)
        {
            if (!GetPredicate<T>(this.PredicateName, this.PredicateGroup, instance, string.Empty, null,
                    out Func<T, bool>? predicate, out ValidationException? exception))
            {
                return new ValidationResult(exception!);
            }

            this._predicate = predicate!;
        }

        return typedValue.ConvertedValue.ValidateDoesNotContain(this._predicate!, propertyName, blackboard);
    }
}
