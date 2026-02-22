using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
/// Validates the decorated member's value against a named predicate registered via PredicateManager.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public sealed class ValidateAgainstPredicateAttribute : ValidationAttribute
{
    public string PredicateName { get; }
    public string? PredicateGroup { get; set; }

    public ValidateAgainstPredicateAttribute(string predicateName) : base(AgainstPredicate.ValidatorName)
    {
        PredicateName = predicateName;
    }

    public override ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        var message = Message ?? AgainstPredicate.DefaultValidationFailureMessage;
        return ValidationResult.CreateFromValidationFailure(
            Name, message, memberName, blackboard,
            new List<(string key, object? value)>
            {
                ("value", value),
                ("predicateName", PredicateName),
                ("predicateGroup", PredicateGroup)
            });
    }
}
