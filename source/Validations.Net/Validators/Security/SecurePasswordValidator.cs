using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators.Security;

public readonly record struct SecurePasswordParams(
    int MinLength = 8,
    bool RequireUppercase = true,
    bool RequireLowercase = true,
    bool RequireDigit = true,
    bool RequireSpecialChar = true);

public sealed class SecurePasswordValidator : IValidator
{
    public SecurePasswordParams Params { get; }

    public SecurePasswordValidator(int minLength = 8, bool requireUppercase = true, bool requireLowercase = true,
        bool requireDigit = true, bool requireSpecialChar = true)
    {
        Params = new SecurePasswordParams(minLength, requireUppercase, requireLowercase, requireDigit, requireSpecialChar);
    }

    public string Name => IsSecurePassword.ValidatorName;
    public string DefaultFailureMessage => IsSecurePassword.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is string s && s.CheckIsSecurePassword(Params.MinLength, Params.RequireUppercase, Params.RequireLowercase,
                Params.RequireDigit, Params.RequireSpecialChar))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value)]);
    }
}
