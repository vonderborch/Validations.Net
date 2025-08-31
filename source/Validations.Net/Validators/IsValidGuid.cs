using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods to check if a string is a valid GUID.
/// </summary>
public static class IsValidGuid
{
    private const string ValidatorName = "IsValidGuid";

    #region Check Methods

    /// <summary>
    ///     Checks if a string is a valid GUID.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid GUID; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidGuid(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Guid.TryParse(value, out _);
    }

    /// <summary>
    ///     Checks if a string is a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="format">The format to validate against.</param>
    /// <returns>True if the string is a valid GUID in the specified format; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidGuid(this string? value, string format)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(format))
        {
            return false;
        }

        return Guid.TryParseExact(value, format, out _);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates if a string is a valid GUID.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid GUID.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidGuid(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidGuid(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid GUID.",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates if a string is a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="format">The format to validate against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is a valid GUID in the specified format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidGuid(this string? value, string format,
        IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidGuid(value, format))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("format", format)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be a valid GUID in format '{format}'.",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that a string is a valid GUID.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it is a valid GUID.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid GUID.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidGuid(this string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsValidGuid(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid GUID.", parameterName, blackboard, contextList);
        }

        return value;
    }

    /// <summary>
    ///     Ensures that a string is a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="format">The format to validate against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original string if it is a valid GUID in the specified format.</returns>
    /// <exception cref="ValidationException">Thrown when the string is not a valid GUID in the specified format.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidGuid(this string? value, string format, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (!CheckIsValidGuid(value, format))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("format", format)
            };
            throw ValidationException.Create(ValidatorName, $"The value must be a valid GUID in format '{format}'.",
                parameterName, blackboard, contextList);
        }

        return value;
    }

    #endregion
}
