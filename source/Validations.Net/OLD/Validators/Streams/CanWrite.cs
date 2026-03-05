using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.OLD.Validators.Streams;

/// <summary>
/// The CanWrite class provides methods for validation to ensure that
/// a stream supports writing.
/// </summary>
public static class CanWrite
{
    /// <summary>
    ///     Represents the unique identifier name for the validator.
    /// </summary>
    public const string ValidatorName = "CanWrite";

    /// <summary>
    ///     Represents the default failure message used when the validator fails validation.
    /// </summary>
    public const string DefaultValidationFailureMessage = "Stream must be writable";

    /// <summary>
    /// Checks if the given stream can be written to.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CheckCanWrite(this Stream? stream)
    {
        return stream is not null && stream.CanWrite;
    }

    /// <summary>
    /// Validates whether the given stream can be written to.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValidationResult ValidateCanWrite(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        if (!stream.CheckCanWrite())
        {
            return ValidationResult.CreateFromValidationFailure(ValidatorName, validationFailureMessage, parameterName, blackboard, [("value", stream)]);
        }

        return ValidationResult.CreateFromValidationSuccess();
    }

    /// <summary>
    /// Ensures the given stream can be written to, throwing an exception if validation fails.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Stream? EnsureCanWrite(this Stream? stream, IBlackboard? blackboard = null,
        string validationFailureMessage = DefaultValidationFailureMessage,
        [CallerArgumentExpression(nameof(stream))] string? parameterName = null)
    {
        var validationResult = stream.ValidateCanWrite(blackboard, validationFailureMessage, parameterName);
        if (!validationResult.IsValid)
        {
            throw validationResult.ValidationException!;
        }

        return stream;
    }
}

public sealed class CanWriteValidator : IValidator
{
    public static readonly CanWriteValidator Instance = new();
    public string Name => CanWrite.ValidatorName;
    public string DefaultFailureMessage => CanWrite.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is Stream stream)
            return stream.ValidateCanWrite(blackboard, this.DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(this.Name, "Value is not a Stream", memberName, blackboard,
            [("value", value)]);
    }
}

/// <summary>
/// Validates that the decorated member's value is a writable stream.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateCanWriteAttribute() : ValidationAttribute(CanWrite.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is Stream s)
            return s.CheckCanWrite() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return Fail(value, memberName, blackboard);
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = this.Message ?? CanWrite.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(this.Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
