using System.Numerics;
using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a numeric value is a power of a specified base.
/// </summary>
public static class IsPowerOf
{
    /// <summary>
    /// Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "IsPowerOf";

    /// <summary>
    /// Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Parameter must be a power of the specified value";

    /// <summary>
    /// Checks if the specified numeric value is a power of the given base.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="base">The base to check against.</param>
    /// <returns>True if the value is a power of the base; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPowerOf<T>(T value, T @base) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        if (value <= T.Zero || @base <= T.One) return false;
        if (value == T.One) return true;

        while (value > T.One)
        {
            if (value % @base != T.Zero) return false;
            value /= @base;
        }

        return value == T.One;
    }

    /// <summary>
    /// Checks if the specified binary integer value is a power of 2.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 2; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPowerOf2<T>(T value) where T : IBinaryInteger<T>
    {
        return value > T.Zero && (value & (value - T.One)) == T.Zero;
    }

    /// <summary>
    /// Checks if the specified numeric value is a power of 10.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is a power of 10; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsPowerOf10<T>(T value) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        return CheckIsPowerOf(value, T.CreateChecked(10));
    }

    /// <summary>
    /// Ensures that the specified numeric value is a power of the given base, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of the base.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsPowerOf<T>(T value, T @base, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var result = ValidateIsPowerOf(value, @base, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Ensures that the specified binary integer value is a power of 2, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 2.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsPowerOf2<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IBinaryInteger<T>
    {
        var result = ValidateIsPowerOf2(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Ensures that the specified numeric value is a power of 10, throwing a ValidationException if it is not.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <exception cref="ValidationException">Thrown when the value is not a power of 10.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void EnsureIsPowerOf10<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var result = ValidateIsPowerOf10(value, blackboard, validationFailureMessage, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }
    }

    /// <summary>
    /// Validates if the specified numeric value is a power of the given base.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="base">The base to check against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of the base.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsPowerOf<T>(T value, T @base, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var isValid = CheckIsPowerOf(value, @base);
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
    /// Validates if the specified binary integer value is a power of 2.
    /// </summary>
    /// <typeparam name="T">The binary integer type.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 2.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsPowerOf2<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : IBinaryInteger<T>
    {
        var isValid = CheckIsPowerOf2(value);
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
    /// Validates if the specified numeric value is a power of 10.
    /// </summary>
    /// <typeparam name="T">The numeric type that implements INumber.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="validationFailureMessage">A custom failure message to use if validation fails.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A validation result indicating whether the value is a power of 10.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsPowerOf10<T>(T value, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null) 
        where T : INumber<T>, IModulusOperators<T, T, T>, IComparisonOperators<T, T, bool>, IDivisionOperators<T, T, T>
    {
        var isValid = CheckIsPowerOf10(value);
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
