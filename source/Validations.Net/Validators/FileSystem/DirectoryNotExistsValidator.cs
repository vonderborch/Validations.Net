using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.FileSystem;

public sealed class DirectoryNotExistsValidator : IValidator
{
    public static readonly DirectoryNotExistsValidator Instance = new();
    public string Name => DoesDirectoryNotExist.ValidatorName;
    public string DefaultFailureMessage => DoesDirectoryNotExist.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string path)
            return path.ValidateDoesDirectoryNotExist(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
