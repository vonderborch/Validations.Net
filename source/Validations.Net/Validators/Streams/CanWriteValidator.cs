using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Streams;

public sealed class CanWriteValidator : IValidator
{
    public static readonly CanWriteValidator Instance = new();
    public string Name => CanWrite.ValidatorName;
    public string DefaultFailureMessage => CanWrite.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is Stream stream)
            return stream.ValidateCanWrite(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a Stream", memberName, blackboard,
            [("value", value)]);
    }
}
