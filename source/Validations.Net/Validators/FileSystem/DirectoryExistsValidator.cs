using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.FileSystem;

public sealed class DirectoryExistsValidator : IValidator
{
    public static readonly DirectoryExistsValidator Instance = new();
    public string Name => DoesDirectoryExist.ValidatorName;
    public string DefaultFailureMessage => DoesDirectoryExist.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string path)
            return path.ValidateDoesDirectoryExist(blackboard, DefaultFailureMessage, memberName);
        return ValidationResult.CreateFromValidationFailure(Name, "Value is not a string", memberName, blackboard,
            [("value", value)]);
    }
}
