using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Validates that a string is not a valid MAC address.
/// </summary>
public static class IsNotValidMacAddress
{
    private const string ValidatorName = nameof(IsNotValidMacAddress);

    /// <summary>
    ///     Checks if the string is not a valid colon-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid colon-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsNotValidColonSeparatedMacAddress(this string? value)
    {
        return !value.CheckIsValidColonSeparatedMacAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid dot-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid dot-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsNotValidDotSeparatedMacAddress(this string? value)
    {
        return !value.CheckIsValidDotSeparatedMacAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid hyphen-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid hyphen-separated MAC address; otherwise, false.</returns>
    public static bool CheckIsNotValidHyphenSeparatedMacAddress(this string? value)
    {
        return !value.CheckIsValidHyphenSeparatedMacAddress();
    }

    /// <summary>
    ///     Checks if the string is not a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are checked.</param>
    /// <returns>True if the string is not a valid MAC address; otherwise, false.</returns>
    public static bool CheckIsNotValidMacAddress(this string? value, MacAddressFormat[]? allowedFormats = null)
    {
        return !value.CheckIsValidMacAddress(allowedFormats);
    }

    /// <summary>
    ///     Checks if the string is not a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <returns>True if the string is not a valid MAC address without separators; otherwise, false.</returns>
    public static bool CheckIsNotValidNoSeparatorMacAddress(this string? value)
    {
        return !value.CheckIsValidNoSeparatorMacAddress();
    }

    /// <summary>
    ///     Ensures that the string is not a valid colon-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid colon-separated MAC address.</exception>
    public static void EnsureIsNotValidColonSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidColonSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid colon-separated MAC address",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid dot-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid dot-separated MAC address.</exception>
    public static void EnsureIsNotValidDotSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidDotSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid dot-separated MAC address",
                null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid hyphen-separated MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid hyphen-separated MAC address.</exception>
    public static void EnsureIsNotValidHyphenSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidHyphenSeparatedMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName,
                "The value must not be a valid hyphen-separated MAC address", null, null, contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid MAC address, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are checked.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid MAC address.</exception>
    public static void EnsureIsNotValidMacAddress(this string? value, string fieldName, IBlackboard? blackboard,
        MacAddressFormat[]? allowedFormats = null)
    {
        var isValid = CheckIsNotValidMacAddress(value, allowedFormats);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("AllowedFormats", allowedFormats),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName, "The value must not be a valid MAC address", null, null,
                contextList);
        }
    }

    /// <summary>
    ///     Ensures that the string is not a valid MAC address without separators, throwing an exception if validation fails.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when the string is a valid MAC address without separators.</exception>
    public static void EnsureIsNotValidNoSeparatorMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidNoSeparatorMacAddress(value);
        if (!isValid)
        {
            var contextList = new List<(string, object?)>
            {
                ("FieldName", fieldName),
                ("Value", value)
            };
            throw ValidationException.Create(ValidatorName,
                "The value must not be a valid MAC address without separators", null, null, contextList);
        }
    }

    /// <summary>
    ///     Validates that the string is not a valid colon-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidColonSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidColonSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid colon-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid dot-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidDotSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidDotSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid dot-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid hyphen-separated MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidHyphenSeparatedMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidHyphenSeparatedMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid hyphen-separated MAC address", fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="allowedFormats">The allowed MAC address formats. If null, all common formats are checked.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard, MacAddressFormat[]? allowedFormats = null)
    {
        var isValid = CheckIsNotValidMacAddress(value, allowedFormats);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("AllowedFormats", allowedFormats),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName, "The value must not be a valid MAC address",
                fieldName, blackboard, contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="fieldName">The name of the field being validated.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    public static ValidationResult ValidateIsNotValidNoSeparatorMacAddress(this string? value, string fieldName,
        IBlackboard? blackboard)
    {
        var isValid = CheckIsNotValidNoSeparatorMacAddress(value);
        var contextList = new List<(string, object?)>
        {
            ("FieldName", fieldName),
            ("Value", value)
        };

        return isValid
            ? ValidationResult.CreateFromValidationSuccess()
            : ValidationResult.CreateFromValidationFailure(ValidatorName,
                "The value must not be a valid MAC address without separators", fieldName, blackboard, contextList);
    }
}
