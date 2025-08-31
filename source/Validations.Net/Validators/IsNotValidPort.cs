using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is not a valid port number.
/// </summary>
public static class IsNotValidPort
{
    private const string ValidatorName = nameof(IsNotValidPort);
    private const int MaxPortNumber = 65535;
    private const int WellKnownPortsMin = 0;
    private const int WellKnownPortsMax = 1023;
    private const int RegisteredPortsMin = 1024;
    private const int RegisteredPortsMax = 49151;
    private const int DynamicPortsMin = 49152;
    private const int DynamicPortsMax = 65535;

    #region Check Methods

    /// <summary>
    ///     Checks if the value is not a valid port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is not a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidPort(int value)
    {
        return !IsValidPort.CheckIsValidPort(value);
    }

    /// <summary>
    ///     Checks if the value is not a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is not a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidPort(int? value)
    {
        return !IsValidPort.CheckIsValidPort(value);
    }

    /// <summary>
    ///     Checks if the string does not represent a valid port number.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string does not represent a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotValidPort(string? value)
    {
        return !IsValidPort.CheckIsValidPort(value);
    }

    /// <summary>
    ///     Checks if the value is not a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is not a valid well-known port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotWellKnownPort(int value)
    {
        return !IsValidPort.CheckIsValidWellKnownPort(value);
    }

    /// <summary>
    ///     Checks if the value is not a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is not a valid registered port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotRegisteredPort(int value)
    {
        return !IsValidPort.CheckIsValidRegisteredPort(value);
    }

    /// <summary>
    ///     Checks if the value is not a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is not a valid dynamic/private port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsNotDynamicOrPrivatePort(int value)
    {
        return !IsValidPort.CheckIsValidDynamicPort(value);
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the value is not a valid port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("maxPort", MaxPortNumber)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is not a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidPort(int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("maxPort", MaxPortNumber)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string does not represent a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotValidPort(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotValidPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("maxPort", MaxPortNumber)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is not a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotWellKnownPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotWellKnownPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minPort", WellKnownPortsMin),
            ("maxPort", WellKnownPortsMax)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid well-known port number ({WellKnownPortsMin}-{WellKnownPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is not a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotRegisteredPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotRegisteredPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minPort", RegisteredPortsMin),
            ("maxPort", RegisteredPortsMax)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid registered port number ({RegisteredPortsMin}-{RegisteredPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is not a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsNotDynamicOrPrivatePort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsNotDynamicOrPrivatePort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minPort", DynamicPortsMin),
            ("maxPort", DynamicPortsMax)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must not be a valid dynamic/private port number ({DynamicPortsMin}-{DynamicPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the value is not a valid port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotValidPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is not a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int? EnsureIsNotValidPort(int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string does not represent a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it does not represent a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value represents a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsNotValidPort(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is not a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid well-known port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid well-known port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotWellKnownPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotWellKnownPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is not a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid registered port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid registered port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotRegisteredPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotRegisteredPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is not a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is not a valid dynamic/private port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is a valid dynamic/private port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsNotDynamicOrPrivatePort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsNotDynamicOrPrivatePort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
