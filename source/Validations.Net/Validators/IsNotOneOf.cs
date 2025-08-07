using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides methods for validating if a value is not one of the specified options.
/// </summary>
public static class IsNotOneOf
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotOneOf";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string ValidationFailureMessage = "Parameter must not be any of the specified values";
    
    /// <summary>
    /// Checks if the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>True if the value is not one of the specified options, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, params T[] options)
    {
        if (value is null || options.Length == 0)
        {
            return false;
        }
        
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Checks if the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>True if the value is not one of the specified options, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotOneOf<T>(this T? value, ICollection<T> options)
    {
        if (value is null || options.Count == 0)
        {
            return false;
        }
        
        foreach (T option in options)
        {
            if (value?.Equals(option) ?? false)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Ensures that the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="propertyName">The name of the property to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>The value if it is not one of the specified options, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T? EnsureIsNotOneOf<T>(this T? value, string propertyName, IBlackboard? blackboard = null,
        params T[] options)
    {
        ValidationResult result = value.ValidateIsNotOneOf(propertyName, blackboard, options);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
    
    /// <summary>
    /// Ensures that the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <param name="propertyName">The name of the property to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>The value if it is not one of the specified options, otherwise throws a <see cref="ValidationException"/>.</returns>
    /// <exception cref="ValidationException">Thrown when the value is one of the specified options.</exception>
    public static T? EnsureIsNotOneOf<T>(this T? value, ICollection<T> options, string propertyName,
        IBlackboard? blackboard = null)
    {
        ValidationResult result = value.ValidateIsNotOneOf(options, propertyName, blackboard);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }
    
    /// <summary>
    /// Validates if the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="variableName">The name of the variable to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, string variableName, IBlackboard? blackboard = null,
        params T[] options)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, variableName, blackboard, [("value", value), ("options", options)] );
            return result;
        }

        return new ValidationResult();
    }
    
    /// <summary>
    /// Validates if the value is not one of the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="options">The options to check against.</param>
    /// <param name="variableName">The name of the variable to check.</param>
    /// <param name="blackboard">The blackboard to check.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the result of the validation.</returns>
    public static ValidationResult ValidateIsNotOneOf<T>(this T? value, ICollection<T> options, string variableName,
        IBlackboard? blackboard = null)
    {
        if (!value.CheckIsNotOneOf(options))
        {
            ValidationResult result = ValidationResult.CreateFromValidationFailure(ValidatorName, ValidationFailureMessage, variableName, blackboard, [("value", value), ("options", options)] );
            return result;
        }

        return new ValidationResult();
    }
}
