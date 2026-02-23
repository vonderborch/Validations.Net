using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.FileSystem;

public sealed class FileExistsValidator : IValidator
{
    public static readonly FileExistsValidator Instance = new();
    public string Name => DoesFileExist.ValidatorName;
    public string DefaultFailureMessage => DoesFileExist.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string path)
            return path.ValidateDoesFileExist(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
