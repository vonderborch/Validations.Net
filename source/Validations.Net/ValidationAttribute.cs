using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net.ValidationAttributes.Helpers;
using Validations.Net.Validators;

namespace Validations.Net;

/// <summary>
///     Base class for validation attributes
///     Provides validation functionality to check and validate property values against specific rules.
/// </summary>
/// <param name="name">The name of the validator.</param>
public abstract class ValidationAttribute(string name) : Attribute
{
    /// <summary>
    ///     The name of the validator.
    /// </summary>
    public string ValidatorName { get; } = name;

    /// <summary>
    ///     Checks if the provided value meets the validation criteria.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value is valid; otherwise, false.</returns>
    public abstract bool Check(object? value, object? instance);

    /// <summary>
    /// Converts the specified value to the desired type if possible, and validates its compatibility.
    /// </summary>
    /// <typeparam name="T">The target type to which the value should be converted.</typeparam>
    /// <param name="value">The value to be converted and validated.</param>
    /// <param name="parameterName">The name of the parameter associated with the value.</param>
    /// <param name="allowNull">Specifies whether null is an acceptable value.</param>
    /// <param name="instance">The instance associated with the validation process.</param>
    /// <param name="propertyName">The name of the property being validated, if applicable.</param>
    /// <param name="blackboard">The blackboard context for additional state or dependencies.</param>
    /// <returns>An instance of <see cref="TypeInfo{T}"/> containing the conversion result and validation status.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected TypeInfo<T> GetCorrectType<T>(object? value, string parameterName, object? instance, string? propertyName = null, IBlackboard? blackboard = null, bool allowNull = true)
    {
        // Return success with default value if null is allowed and value is null
        if (value is null && allowNull)
        {
            return new TypeInfo<T>(default);
        }

        // Return success with converted value if the type matches
        if (value is T typedValue)
        {            
            return new TypeInfo<T>(typedValue);
        }

        // Return failure if type doesn't match or null isn't allowed
        return new TypeInfo<T>(this.ValidatorName, parameterName, value, allowNull, instance, propertyName, blackboard);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected bool GetPredicate<T>(string predicateName, string? predicateGroup, object? instance, string propertyName, IBlackboard? blackboard, out Func<T, bool>? predicate, out ValidationException? exception)
    {
        exception = null;
        predicate =
            PredicateRegistrar.GetPredicate<T>(predicateName, predicateGroup, instance);
        if (predicate is null)
        {
            exception = ValidationException.CreateFromFetchPredicateFailure(
                ValidatorName, propertyName, predicateName, predicateGroup, instance, blackboard);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Validates the specified value against defined validation rules and returns the result.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance associated with the value being validated.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard instance providing context for validation.</param>
    /// <returns>A <see cref="ValidationResult"/> representing the outcome of the validation.</returns>
    public abstract ValidationResult Validate(object? value, object? instance, string propertyName,
        IBlackboard? blackboard = null);
}
