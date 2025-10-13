using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is NOT a power of a specified base.
/// </summary>
public static class IsNotPowerOf
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsNotPowerOf";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must not be a power of the specified value";

    /// <summary>
    /// Checks if the specified numeric value is NOT a power of the given base.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is NOT a power of the base; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotPowerOf<T>(T value, T @base) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        return !IsPowerOf.CheckIsPowerOf(value, @base);
    }

    /// <summary>
    /// Checks if the specified binary integer value is NOT a power of 2.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 2; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotPowerOf2<T>(T value) where T : IBinaryInteger<T>
    {
        return !IsPowerOf.CheckIsPowerOf2(value);
    }

    /// <summary>
    /// Checks if the specified numeric value is NOT a power of 10.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is NOT a power of 10; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotPowerOf10<T>(T value) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        return !IsPowerOf.CheckIsPowerOf10(value);
    }

    /// <summary>
    /// Ensures that the specified numeric value is NOT a power of the given base, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of the base.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotPowerOf<T>(T value, T @base, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var result = ValidateIsNotPowerOf(value, @base, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Ensures that the specified binary integer value is NOT a power of 2, throwing a ValidationException if it is.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 2.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotPowerOf2<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IBinaryInteger<T>
    {
        var result = ValidateIsNotPowerOf2(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Ensures that the specified numeric value is NOT a power of 10, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is a power of 10.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsNotPowerOf10<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var result = ValidateIsNotPowerOf10(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Validates if the specified numeric value is NOT a power of the given base.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of the base.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPowerOf<T>(T value, T @base, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var isValid = CheckIsNotPowerOf(value, @base);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("base", @base)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified binary integer value is NOT a power of 2.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 2.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPowerOf2<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IBinaryInteger<T>
    {
        var isValid = CheckIsNotPowerOf2(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }

    /// <summary>
    /// Validates if the specified numeric value is NOT a power of 10.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is NOT a power of 10.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotPowerOf10<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var isValid = CheckIsNotPowerOf10(value);
        if (isValid)
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage,
            parameterName, blackboard, contextList);
    }
}
