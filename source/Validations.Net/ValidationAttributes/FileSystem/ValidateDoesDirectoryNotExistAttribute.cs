using SimpleBlackboard.Net;
using Validations.Net.Validators.FileSystem;

namespace Validations.Net.ValidationAttributes.FileSystem;

/// <summary>
/// Validates that the decorated member's value is a path to a non-existing directory.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class ValidateDoesDirectoryNotExistAttribute() : ValidationAttribute(DoesDirectoryNotExist.ValidatorName)
{
    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s)
            return s.CheckDoesDirectoryNotExist() ? ValidationResult.CreateFromValidationSuccess() : Fail(value, memberName, blackboard);
        return ValidationResult.CreateFromValidationSuccess();
    }

    private ValidationResult Fail(object? value, string? memberName, IBlackboard? blackboard)
    {
        var message = Message ?? DoesDirectoryNotExist.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(Name, message, memberName, blackboard,
            new List<(string key, object? value)> { ("value", value) });
    }
}
