namespace Validations.Net;

/// <summary>
/// Represents a single validation failure with its associated member path.
/// </summary>
/// <param name="MemberPath">Dot-separated path to the member that failed validation (e.g. "Address.City", "Items[2].Price").</param>
/// <param name="Result">The validation result containing failure details.</param>
public readonly record struct ValidationFailure(string MemberPath, ValidationResult Result)
{
    /// <summary>
    /// The error message from the underlying validation result.
    /// </summary>
    public string? ErrorMessage => Result.ExceptionMessage;
}
