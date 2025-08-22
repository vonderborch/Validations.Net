using System.Text.RegularExpressions;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is a valid MAC address.
/// </summary>
public static class IsValidMacAddress
{
    private const string ValidatorName = nameof(IsValidMacAddress);

    // Common MAC address patterns
    private static readonly Regex ColonSeparatedPattern = new(
        @"^([0-9A-Fa-f]{2}[:]){5}([0-9A-Fa-f]{2})$",
        RegexOptions.Compiled
    );

    private static readonly Regex HyphenSeparatedPattern = new(
        @"^([0-9A-Fa-f]{2}[-]){5}([0-9A-Fa-f]{2})$",
        RegexOptions.Compiled
    );

    private static readonly Regex DotSeparatedPattern = new(
        @"^([0-9A-Fa-f]{4}[.]){2}([0-9A-Fa-f]{4})$",
        RegexOptions.Compiled
    );

    private static readonly Regex NoSeparatorPattern = new(
        @"^[0-9A-Fa-f]{12}$",
        RegexOptions.Compiled
    );

    /// <summary>
    ///     Checks if the string is a valid MAC address with colon separators (e.g., 00:11:22:33:44:55).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid colon-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsValidColonSeparatedMacAddress(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return ColonSeparatedPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address with dot separators (e.g., 0011.2233.4455).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid dot-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsValidDotSeparatedMacAddress(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return DotSeparatedPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address with hyphen separators (e.g., 00-11-22-33-44-55).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid hyphen-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsValidHyphenSeparatedMacAddress(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return HyphenSeparatedPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are allowed.</param>
    /// <returns>True if the string is a valid MAC address; otherwise, false.</returns>
    public static bool CheckIsValidMacAddress(this string? value, MacAddressFormat[]? allowedFormats = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (allowedFormats == null || allowedFormats.Length == 0)
        {
            // Check all common formats
            return ColonSeparatedPattern.IsMatch(value) ||
                   HyphenSeparatedPattern.IsMatch(value) ||
                   DotSeparatedPattern.IsMatch(value) ||
                   NoSeparatorPattern.IsMatch(value);
        }

        foreach (var format in allowedFormats)
        {
            var isValid = format switch
            {
                MacAddressFormat.ColonSeparated => ColonSeparatedPattern.IsMatch(value),
                MacAddressFormat.HyphenSeparated => HyphenSeparatedPattern.IsMatch(value),
                MacAddressFormat.DotSeparated => DotSeparatedPattern.IsMatch(value),
                MacAddressFormat.NoSeparator => NoSeparatorPattern.IsMatch(value),
                _ => false
            };

            if (isValid)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address without separators (e.g., 001122334455).
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is a valid MAC address without separators; otherwise, false.</returns>
    public static bool CheckIsValidNoSeparatorMacAddress(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return NoSeparatorPattern.IsMatch(value);
    }

    /// <summary>
    ///     Ensures that the string is a valid colon-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid colon-separated MAC address.</exception>
    public static void EnsureIsValidColonSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidColonSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid colon-separated MAC address",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid dot-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid dot-separated MAC address.</exception>
    public static void EnsureIsValidDotSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidDotSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid dot-separated MAC address", null,
                null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid hyphen-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid hyphen-separated MAC address.</exception>
    public static void EnsureIsValidHyphenSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidHyphenSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid hyphen-separated MAC address",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are allowed.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid MAC address.</exception>
    public static void EnsureIsValidMacAddress(this string? value, string fieldName, IBlackboard? blackboard,
        MacAddressFormat[]? allowedFormats = null)
    {
        var isValid = CheckIsValidMacAddress(value, allowedFormats);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("AllowedFormats", allowedFormats),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid MAC address", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is a valid MAC address without separators, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is not a valid MAC address without separators.</exception>
    public static void EnsureIsValidNoSeparatorMacAddress(this string? value, string fieldName, IBlackboard? blackboard)
    {
        var isValid = CheckIsValidNoSeparatorMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must be a valid MAC address without separators",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is a valid colon-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidColonSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidColonSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid colon-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid dot-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidDotSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidDotSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid dot-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid hyphen-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidHyphenSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidHyphenSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid hyphen-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are allowed.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard, MacAddressFormat[]? allowedFormats = null)
    {
        var isValid = CheckIsValidMacAddress(value, allowedFormats);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("AllowedFormats", allowedFormats),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must be a valid MAC address",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsValidNoSeparatorMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsValidNoSeparatorMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must be a valid MAC address without separators", fieldName, blackboard, contextList);
    }
}

/// <summary>
///     Enumeration of MAC address formats.
/// </summary>
public enum MacAddressFormat
{
    /// <summary>
    ///     Colon-separated format (e.g., 00:11:22:33:44:55).
    /// </summary>
    ColonSeparated,

    /// <summary>
    ///     Hyphen-separated format (e.g., 00-11-22-33-44-55).
    /// </summary>
    HyphenSeparated,

    /// <summary>
    ///     Dot-separated format (e.g., 0011.2233.4455).
    /// </summary>
    DotSeparated,

    /// <summary>
    ///     No separator format (e.g., 001122334455).
    /// </summary>
    NoSeparator
}
