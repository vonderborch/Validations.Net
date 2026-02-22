using SimpleBlackboard.Net;
using Validations.Net.Validators;
using System.Reflection;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates the decorated member's value against a named predicate registered via PredicateManager.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateAgainstPredicateAttribute : ValidationAttribute
{
    private static readonly MethodInfo ValidateTypedMethod = typeof(ValidateAgainstPredicateAttribute)
        .GetMethod(nameof(ValidateTyped), BindingFlags.NonPublic | BindingFlags.Static)!;

    public string PredicateName { get; }
    public string? PredicateGroup { get; set; }

    public ValidateAgainstPredicateAttribute(string predicateName) : base(AgainstPredicate.ValidatorName)
    {
        PredicateName = predicateName;
    }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        var message = Message ?? AgainstPredicate.DefaultValidationFailureMessage;
        var valueType = value?.GetType() ?? typeof(object);
        var validateMethod = ValidateTypedMethod.MakeGenericMethod(valueType);

        return (ValidationResult)validateMethod.Invoke(null,
        [
            value, PredicateName, PredicateGroup, blackboard, message, memberName
        ])!;
    }

    private static ValidationResult ValidateTyped<T>(
        object? value,
        string predicateName,
        string? predicateGroup,
        IBlackboard? blackboard,
        string validationFailureMessage,
        string? memberName)
    {
        var typedValue = value is T converted ? converted : default;
        return typedValue.ValidateAgainstPredicate(
            predicateName,
            predicateGroup,
            predicateInstance: null,
            blackboard,
            validationFailureMessage,
            memberName);
    }
}
