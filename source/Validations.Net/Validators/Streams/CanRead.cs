using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Streams;

/// <summary>
/// The CanRead class provides methods for validation to ensure that
/// a stream supports reading.
/// </summary>
public static class CanRead
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "CanRead";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Stream must be readable";

    /// <summary>
    /// Checks if the given stream can be read from.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckCanRead(this Stream? stream)
    {
        return stream is not null && stream.CanRead;
    }

    /// <summary>
    /// Validates whether the given stream can be read from.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateCanRead(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        if (!stream.CheckCanRead())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", stream)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given stream can be read from, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream? EnsureCanRead(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        var validationResult = stream.ValidateCanRead(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return stream;
    }
}
