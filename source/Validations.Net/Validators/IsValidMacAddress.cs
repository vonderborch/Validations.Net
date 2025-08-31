using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is a valid MAC address.
/// </summary>
public static class IsValidMacAddress
{
    private const string ValidatorName = nameof(IsValidMacAddress);
    private static readonly Regex MacAddressPattern = new(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", RegexOptions.Compiled);
    private static readonly Regex MacAddressNoSeparatorPattern = new(@"^[0-9A-Fa-f]{12}$", RegexOptions.Compiled);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is a valid MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidMacAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return MacAddressPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid MAC address with colons; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidColonSeparatedMacAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Regex.IsMatch(value, @"^([0-9A-Fa-f]{2}:){5}([0-9A-Fa-f]{2})$");
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid MAC address with hyphens; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidHyphenSeparatedMacAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Regex.IsMatch(value, @"^([0-9A-Fa-f]{2}-){5}([0-9A-Fa-f]{2})$");
    }

    /// <summary>
    ///     Checks if the string is a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid MAC address without separators; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidNoSeparatorMacAddress(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return MacAddressNoSeparatorPattern.IsMatch(value);
    }

    /// <summary>
    ///     Checks if the string is a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid unicast MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidUnicastMacAddress(string? value)
    {
        if (!CheckIsValidMacAddress(value))
        {
            return false;
        }

        var cleanValue = value!.Replace(":", "").Replace("-", "");
        var firstByte = Convert.ToByte(cleanValue.Substring(0, 2), 16);
        return (firstByte & 0x01) == 0; // Unicast bit is 0
    }

    /// <summary>
    ///     Checks if the string is a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid multicast MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidMulticastMacAddress(string? value)
    {
        if (!CheckIsValidMacAddress(value))
        {
            return false;
        }

        var cleanValue = value!.Replace(":", "").Replace("-", "");
        var firstByte = Convert.ToByte(cleanValue.Substring(0, 2), 16);
        return (firstByte & 0x01) == 1; // Multicast bit is 1
    }

    /// <summary>
    ///     Checks if the string is a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid locally administered MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidLocallyAdministeredMacAddress(string? value)
    {
        if (!CheckIsValidMacAddress(value))
        {
            return false;
        }

        var cleanValue = value!.Replace(":", "").Replace("-", "");
        var firstByte = Convert.ToByte(cleanValue.Substring(0, 2), 16);
        return (firstByte & 0x02) == 2; // Locally administered bit is 1
    }

    /// <summary>
    ///     Checks if the string is a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is a valid globally unique MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidGloballyUniqueMacAddress(string? value)
    {
        if (!CheckIsValidMacAddress(value))
        {
            return false;
        }

        var cleanValue = value!.Replace(":", "").Replace("-", "");
        var firstByte = Convert.ToByte(cleanValue.Substring(0, 2), 16);
        return (firstByte & 0x02) == 0; // Globally unique bit is 0
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidColonSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidColonSeparatedMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid MAC address with colons (XX:XX:XX:XX:XX:XX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidHyphenSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidHyphenSeparatedMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid MAC address with hyphens (XX-XX-XX-XX-XX-XX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidNoSeparatorMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidNoSeparatorMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid MAC address without separators (XXXXXXXXXXXX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidUnicastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidUnicastMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid unicast MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidMulticastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidMulticastMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid multicast MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidLocallyAdministeredMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidLocallyAdministeredMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid locally administered MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidGloballyUniqueMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidGloballyUniqueMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must be a valid globally unique MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid MAC address with colons.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid MAC address with colons.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidColonSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidColonSeparatedMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid MAC address with hyphens.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid MAC address with hyphens.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidHyphenSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidHyphenSeparatedMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid MAC address without separators.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid MAC address without separators.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidNoSeparatorMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidNoSeparatorMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid unicast MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid unicast MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidUnicastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidUnicastMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid multicast MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid multicast MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidMulticastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidMulticastMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid locally administered MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid locally administered MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidLocallyAdministeredMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidLocallyAdministeredMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid globally unique MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid globally unique MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidGloballyUniqueMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidGloballyUniqueMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
