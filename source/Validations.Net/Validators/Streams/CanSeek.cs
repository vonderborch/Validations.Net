using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Streams;

/// <summary>
/// The CanSeek class provides methods for validation to ensure that
/// a stream supports seeking.
/// </summary>
public static class CanSeek
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "CanSeek";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Stream must be seekable";

    /// <summary>
    /// Checks if the given stream supports seeking.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckCanSeek(this Stream? stream)
    {
        return stream is not null && stream.CanSeek;
    }

    /// <summary>
    /// Validates whether the given stream supports seeking.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateCanSeek(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        if (!stream.CheckCanSeek())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", stream)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given stream supports seeking, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream? EnsureCanSeek(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        var validationResult = stream.ValidateCanSeek(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return stream;
    }
}
