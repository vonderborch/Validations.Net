using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
/// Provides validation methods to check if a string is not a valid GUID.
/// </summary>
public static class IsNotValidGuid
{
    private const string ValidatorName = "IsNotValidGuid";

    #region Check Methods

    /// <summary>
    /// Checks if a string is not a valid GUID.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid GUID; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidGuid(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        return !Guid.TryParse(value, out _);
    }

    /// <summary>
    /// Checks if a string is not a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="format">The format to validate against.</param>
    /// <returns>True if the string is not a valid GUID in the specified format; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidGuid(this string? value, string format)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(format))
            return true;

        return !Guid.TryParseExact(value, format, out _);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    /// Validates if a string is not a valid GUID.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is not a valid GUID.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidGuid(this string? value, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotValidGuid(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid GUID.",
            fieldName,
            blackboard,
            contextList);
    }

    /// <summary>
    /// Validates if a string is not a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="format">The format to validate against.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <returns>A ValidationResult indicating whether the string is not a valid GUID in the specified format.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidGuid(this string? value, string format, IBlackboard? blackboard = null, [CallerArgumentExpression(nameof(value))] string? fieldName = null)
    {
        if (CheckIsNotValidGuid(value, format))
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
            $"The value must not be a valid GUID in format '{format}'.",
            fieldName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    /// Ensures that a string is not a valid GUID.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>The original string if it is not a valid GUID.</returns>
    /// <exception cref="ValidationException">Thrown when the string is a valid GUID.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidGuid(this string? value)
    {
        if (!CheckIsNotValidGuid(value))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid GUID.", null, null, contextList);
        }

        return value;
    }

    /// <summary>
    /// Ensures that a string is not a valid GUID with a specific format.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="format">The format to validate against.</param>
    /// <returns>The original string if it is not a valid GUID in the specified format.</returns>
    /// <exception cref="ValidationException">Thrown when the string is a valid GUID in the specified format.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidGuid(this string? value, string format)
    {
        if (!CheckIsNotValidGuid(value, format))
        {
            var contextList = new List<(string, object?)>
            {
                ("value", value),
                ("format", format)
            };
            throw ValidationException.Create(ValidatorName, $"The value must not be a valid GUID in format '{format}'.", null, null, contextList);
        }

        return value;
    }

    #endregion
}
