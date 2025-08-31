using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;
using Validations.Net;

namespace Validations.Net.Validators;

/// <summary>
///     Provides validation methods for checking if a value is a valid port number.
/// </summary>
public static class IsValidPort
{
    private const string ValidatorName = nameof(IsValidPort);
    private const int MaxPortNumber = 65535;
    private const int WellKnownPortsMin = 0;
    private const int WellKnownPortsMax = 1023;
    private const int RegisteredPortsMin = 1024;
    private const int RegisteredPortsMax = 49151;
    private const int DynamicPortsMin = 49152;
    private const int DynamicPortsMax = 65535;

    #region Check Methods

    /// <summary>
    ///     Checks if the value is a valid port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidPort(int value)
    {
        return value >= 0 && value <= MaxPortNumber;
    }

    /// <summary>
    ///     Checks if the value is a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to check.</param>
    /// <returns>True if the value is a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidPort(int? value)
    {
        return value.HasValue && CheckIsValidPort(value.Value);
    }

    /// <summary>
    ///     Checks if the string represents a valid port number.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>True if the string represents a valid port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidPort(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return int.TryParse(value, out var port) && CheckIsValidPort(port);
    }

    /// <summary>
    ///     Checks if the value is a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a valid well-known port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidWellKnownPort(int value)
    {
        return value >= WellKnownPortsMin && value <= WellKnownPortsMax;
    }

    /// <summary>
    ///     Checks if the value is a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a valid registered port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidRegisteredPort(int value)
    {
        return value >= RegisteredPortsMin && value <= RegisteredPortsMax;
    }

    /// <summary>
    ///     Checks if the value is a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a valid dynamic/private port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidDynamicPort(int value)
    {
        return value >= DynamicPortsMin && value <= DynamicPortsMax;
    }

    /// <summary>
    ///     Checks if the value is a valid non-privileged port number.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <returns>True if the value is a valid non-privileged port number; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidNonPrivilegedPort(int value)
    {
        return value >= RegisteredPortsMin && value <= MaxPortNumber;
    }

    /// <summary>
    ///     Checks if the value is a valid port number within the specified range.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="minPort">The minimum port number.</param>
    /// <param name="maxPort">The maximum port number.</param>
    /// <returns>True if the value is a valid port number within the range; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckIsValidPortInRange(int value, int minPort, int maxPort)
    {
        return CheckIsValidPort(value) && value >= minPort && value <= maxPort;
    }

    #endregion

    #region Validate Methods

    /// <summary>
    ///     Validates that the value is a valid port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidPort(value))
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
            $"The value must be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidPort(int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidPort(value))
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
            $"The value must be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the string represents a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidPort(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidPort(value))
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
            $"The value must be a valid port number (0-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidWellKnownPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidWellKnownPort(value))
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
            $"The value must be a valid well-known port number ({WellKnownPortsMin}-{WellKnownPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidRegisteredPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidRegisteredPort(value))
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
            $"The value must be a valid registered port number ({RegisteredPortsMin}-{RegisteredPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidDynamicPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidDynamicPort(value))
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
            $"The value must be a valid dynamic/private port number ({DynamicPortsMin}-{DynamicPortsMax})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid non-privileged port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidNonPrivilegedPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidNonPrivilegedPort(value))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minPort", RegisteredPortsMin),
            ("maxPort", MaxPortNumber)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be a valid non-privileged port number ({RegisteredPortsMin}-{MaxPortNumber})",
            parameterName,
            blackboard,
            contextList);
    }

    /// <summary>
    ///     Validates that the value is a valid port number within the specified range.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="minPort">The minimum port number.</param>
    /// <param name="maxPort">The maximum port number.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>A ValidationResult indicating success or failure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateIsValidPortInRange(int value, int minPort, int maxPort, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        if (CheckIsValidPortInRange(value, minPort, maxPort))
        {
            return ValidationResult.CreateFromValidationSuccess();
        }

        var contextList = new List<(string, object?)>
        {
            ("value", value),
            ("minPort", minPort),
            ("maxPort", maxPort)
        };

        return ValidationResult.CreateFromValidationFailure(
            ValidatorName,
            $"The value must be a valid port number within the range {minPort}-{maxPort}",
            parameterName,
            blackboard,
            contextList);
    }

    #endregion

    #region Ensure Methods

    /// <summary>
    ///     Ensures that the value is a valid port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid port number.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int? EnsureIsValidPort(int? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the string represents a valid port number.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it represents a valid port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value does not represent a valid port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? EnsureIsValidPort(string? value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid well-known port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid well-known port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid well-known port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidWellKnownPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidWellKnownPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid registered port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid registered port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid registered port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidRegisteredPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidRegisteredPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid dynamic/private port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid dynamic/private port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid dynamic/private port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidDynamicPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidDynamicPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid non-privileged port number.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid non-privileged port number.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid non-privileged port number.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidNonPrivilegedPort(int value, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidNonPrivilegedPort(value, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    /// <summary>
    ///     Ensures that the value is a valid port number within the specified range.
    /// </summary>
    /// <param name="value">The integer value to validate.</param>
    /// <param name="minPort">The minimum port number.</param>
    /// <param name="maxPort">The maximum port number.</param>
    /// <param name="blackboard">Optional blackboard for additional context.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The original value if it is a valid port number within the range.</returns>
    /// <exception cref="ValidationException">Thrown when the value is not a valid port number within the range.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int EnsureIsValidPortInRange(int value, int minPort, int maxPort, IBlackboard? blackboard = null,
        [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        var result = ValidateIsValidPortInRange(value, minPort, maxPort, blackboard, parameterName);
        if (!result.IsValid)
        {
            throw result.ValidationException!;
        }

        return value;
    }

    #endregion
}
