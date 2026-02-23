using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Streams;

public sealed class CanReadValidator : IValidator
{
    public static readonly CanReadValidator Instance = new();
    public string Name => CanRead.ValidatorName;
    public string DefaultFailureMessage => CanRead.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is Stream stream)
            return stream.ValidateCanRead(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a Stream", memberName, blackboard,
            [("value", value)]);
    }
}
