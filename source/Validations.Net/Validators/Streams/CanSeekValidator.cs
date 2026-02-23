using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Streams;

public sealed class CanSeekValidator : IValidator
{
    public static readonly CanSeekValidator Instance = new();
    public string Name => CanSeek.ValidatorName;
    public string DefaultFailureMessage => CanSeek.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is Stream stream)
            return stream.ValidateCanSeek(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a Stream", memberName, blackboard,
            [("value", value)]);
    }
}
