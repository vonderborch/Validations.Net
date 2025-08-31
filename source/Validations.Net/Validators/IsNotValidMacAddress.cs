using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is not a valid MAC address.
/// </summary>
public static class IsNotValidMacAddress
{
    private const string ValidatorName = nameof(IsNotValidMacAddress);
    private static readonly Regex MacAddressPattern = new(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", RegexOptions.Compiled);
    private static readonly Regex MacAddressNoSeparatorPattern = new(@"^[0-9A-Fa-f]{12}$", RegexOptions.Compiled);

    #region Check Methods

    /// <summary>
    ///     Checks if the string is not a valid MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid MAC address with colons; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidColonSeparatedMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidColonSeparatedMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid MAC address with hyphens; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidHyphenSeparatedMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidHyphenSeparatedMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid MAC address without separators; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidNoSeparatorMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidNoSeparatorMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid unicast MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidUnicastMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidUnicastMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid multicast MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidMulticastMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidMulticastMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid locally administered MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidLocallyAdministeredMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidLocallyAdministeredMacAddress(value);
    }

    /// <summary>
    ///     Checks if the string is not a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string is not a valid globally unique MAC address; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidGloballyUniqueMacAddress(string? value)
    {
        return !IsValidMacAddress.CheckIsValidGloballyUniqueMacAddress(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the string is not a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidColonSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidColonSeparatedMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid MAC address with colons (XX:XX:XX:XX:XX:XX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidHyphenSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidHyphenSeparatedMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid MAC address with hyphens (XX-XX-XX-XX-XX-XX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidNoSeparatorMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidNoSeparatorMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid MAC address without separators (XXXXXXXXXXXX)",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidUnicastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidUnicastMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid unicast MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidMulticastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidMulticastMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid multicast MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidLocallyAdministeredMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidLocallyAdministeredMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid locally administered MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string is not a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidGloballyUniqueMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidGloballyUniqueMacAddress(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            "The value must not be a valid globally unique MAC address",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the string is not a valid MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid MAC address with colons.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid MAC address with colons.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid MAC address with colons.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidColonSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidColonSeparatedMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid MAC address with hyphens.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid MAC address with hyphens.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid MAC address with hyphens.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidHyphenSeparatedMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidHyphenSeparatedMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid MAC address without separators.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid MAC address without separators.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid MAC address without separators.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidNoSeparatorMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidNoSeparatorMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid unicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid unicast MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid unicast MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidUnicastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidUnicastMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid multicast MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid multicast MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid multicast MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidMulticastMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidMulticastMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid locally administered MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid locally administered MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid locally administered MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidLocallyAdministeredMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidLocallyAdministeredMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string is not a valid globally unique MAC address.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid globally unique MAC address.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid globally unique MAC address.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidGloballyUniqueMacAddress(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidGloballyUniqueMacAddress(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
