namespace Validations.Net.OLD;

/// <summary>
/// Thrown when multiple validation failures occur. Carries the full composite
/// <see cref="ValidationResult"/> so callers can inspect all failures.
/// </summary>
public class AggregateValidationException : ValidationException
{
    /// <summary>
    /// The composite validation result containing all failures and warnings.
    /// </summary>
    public ValidationResult Result { get; }

    /// <summary>
    /// All error-severity failures from the result.
    /// </summary>
    public IReadOnlyList<ValidationResult> AllFailures => this.Result.Failures;

    internal AggregateValidationException(string message, string validator, string? parameterName,
        ValidationContext context, SimpleBlackboard.Net.IBlackboard? blackboard, ValidationResult result)
        : base(message, validator, parameterName, context, blackboard)
    {
        this.Result = result;
    }
}
