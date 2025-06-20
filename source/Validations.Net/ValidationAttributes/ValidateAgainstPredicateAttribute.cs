using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates a value against a registered predicate function.
/// </summary>
/// <typeparam name="T">The type of the value to validate.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateAgainstPredicateAttribute<T> : ValidationAttribute
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
    ///     Initializes a new instance of the ValidateAgainstPredicateAttribute class.
    /// </summary>
    /// <param name="predicateName">The name of the registered predicate function to use for validation.</param>
    /// <param name="predicateGroup">The group name of the predicate function. Default is null.</param>
    /// <exception cref="ValidationException">Thrown when the specified predicate function is not found.</exception>
    public ValidateAgainstPredicateAttribute(string predicateName, string? predicateGroup = null) : base(
        "AgainstPredicate")
    {
        this.PredicateName = predicateName;
        this.PredicateGroup = predicateGroup;
    }

    /// <summary>
    ///     Represents the group name associated with a registered predicate function.
    /// </summary>
    /// <remarks>
    ///     The group name serves as an identifier to categorize or namespace predicate functions.
    ///     When using validation attributes, such as <c>ValidateAgainstPredicateAttribute</c>, the group name
    ///     is used to locate and differentiate predicates with the same name across different groups.
    /// </remarks>
    public string? PredicateGroup { get; }

    /// <summary>
    ///     Represents the name of the registered predicate function used for validation.
    /// </summary>
    /// <remarks>
    ///     The predicate name uniquely identifies a function within a specified group
    ///     that is used to validate a value. It is typically used in conjunction with
    ///     the <c>ValidateAgainstPredicateAttribute</c> to ensure values meet custom criteria.
    /// </remarks>
    public string PredicateName { get; }

    /// <summary>
    /// Checks whether the provided value satisfies the specified predicate function for validation.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="instance">The object instance containing the value to be validated.</param>
    /// <returns>A boolean value indicating whether the validation is successful.</returns>
    /// <exception cref="ValidationException">Thrown if the predicate function is not found or the value type is invalid.</exception>
    public override bool Check(object? value, object? instance)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance);
        if (!typedValue.IsCorrectType)
        {
            return false;
        }
        if (this._predicate is null)
        {
            if (!GetPredicate(PredicateName, PredicateGroup, instance, string.Empty, null, out Func<T, bool>? predicate, out _))
            {
                return false;
            }
            this._predicate = predicate;
        }
        
        bool result = typedValue.ConvertedValue.CheckAgainstPredicate(this._predicate!);
        return result;
    }

    /// <summary>
    /// Validates the specified value against a registered predicate function.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance containing the property being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard object for additional validation context. Default is null.</param>
    /// <returns>The result of the validation operation.</returns>
    public override ValidationResult Validate(object? value, object? instance, string propertyName,
        Blackboard? blackboard = null)
    {
        TypeInfo<T> typedValue = GetCorrectType<T>(value, nameof(value), instance, propertyName, blackboard);
        if (!typedValue.IsCorrectType)
        {
            return new ValidationResult(typedValue.Exception!);
        }
        if (this._predicate is null)
        {
            if (!GetPredicate(PredicateName, PredicateGroup, instance, propertyName, blackboard, out Func<T, bool>? predicate, out ValidationException? exception))
            {
                return new ValidationResult(exception!);
            }
            this._predicate = predicate;
        }
        
        ValidationResult result = typedValue.ConvertedValue.ValidateAgainstPredicate(this._predicate!, propertyName, blackboard);
        return result;
    }
}
