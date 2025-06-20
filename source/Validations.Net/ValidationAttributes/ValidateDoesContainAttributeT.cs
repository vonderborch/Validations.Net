using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
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
    /// Checks if a given value satisfies the rules defined by the validation attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The object instance that contains the value being validated.</param>
    /// <returns>
    /// <c>true</c> if the value meets the validation rules; otherwise, <c>false</c>.
    /// </returns>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, allowNull: false);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }

        if (this.PredicateName is null)
        {
            return typedValue.ConvertedValue!.CheckDoesContain(this.Item);
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

        return typedValue.ConvertedValue.CheckDoesContain(this._predicate!);
    }

    /// <summary>
    /// Validates the given value against the specified property and context parameters using the defined logic.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance of the class containing the property being validated.</param>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="blackboard">An optional blackboard instance used during validation.</param>
    /// <returns>A ValidationResult indicating the success or failure of the validation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<ICollection<T>> typedValue = GetCorrectType<ICollection<T>>(value, nameof(value), instance, allowNull: false);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }

        if (this.PredicateName is null)
        {
            return typedValue.ConvertedValue!.ValidateDoesContain(this.Item, propertyName, blackboard);
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

        return typedValue.ConvertedValue.ValidateDoesContain(this._predicate!, propertyName, blackboard);
    }
}
