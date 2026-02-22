namespace Validations.Net;

/// <summary>
/// Represents the aggregated result of running multiple validations, collecting all failures.
/// Used by IsValid/IsNotValid validators and ValidationSet execution.
/// </summary>
public sealed class AggregateValidationResult
{
    /// <summary>
    /// A shared instance representing a successful validation with no failures.
    /// </summary>
    public static readonly AggregateValidationResult Success = new(null);

    private readonly List<ValidationFailure>? _failures;

    private AggregateValidationResult(List<ValidationFailure>? failures)
    {
        _failures = failures;
    }

    /// <summary>
    /// Whether all validations passed (no failures).
    /// </summary>
    public bool IsValid => _failures is null || _failures.Count == 0;

    /// <summary>
    /// The list of validation failures. Empty if validation succeeded.
    /// </summary>
    public IReadOnlyList<ValidationFailure> Failures =>
        _failures as IReadOnlyList<ValidationFailure> ?? Array.Empty<ValidationFailure>();

    /// <summary>
    /// Converts failures into a dictionary mapping member paths to their error messages,
    /// matching the standard ASP.NET ModelState shape.
    /// </summary>
    public Dictionary<string, string[]> ToDictionary()
    {
        if (_failures is null || _failures.Count == 0)
            return new Dictionary<string, string[]>();

        var dict = new Dictionary<string, List<string>>();
        foreach (var failure in _failures)
        {
            var key = failure.MemberPath;
            if (!dict.TryGetValue(key, out var list))
            {
                list = new List<string>();
                dict[key] = list;
            }

            if (failure.ErrorMessage is not null)
                list.Add(failure.ErrorMessage);
        }

        var result = new Dictionary<string, string[]>(dict.Count);
        foreach (var kvp in dict)
        {
            result[kvp.Key] = kvp.Value.ToArray();
        }

        return result;
    }

    /// <summary>
    /// Returns all error messages as a flat list.
    /// </summary>
    public IReadOnlyList<string> GetAllErrorMessages()
    {
        if (_failures is null || _failures.Count == 0)
            return Array.Empty<string>();

        var messages = new List<string>(_failures.Count);
        foreach (var failure in _failures)
        {
            if (failure.ErrorMessage is not null)
                messages.Add(failure.ErrorMessage);
        }

        return messages;
    }

    /// <summary>
    /// Creates a builder for constructing an aggregate result. Uses lazy allocation -
    /// the failure list is only created when the first failure is added.
    /// </summary>
    internal static Builder CreateBuilder() => new();

    /// <summary>
    /// Mutable builder for constructing AggregateValidationResult with lazy failure list allocation.
    /// </summary>
    internal class Builder
    {
        private List<ValidationFailure>? _failures;

        /// <summary>
        /// Adds a failure to the result.
        /// </summary>
        public void AddFailure(string memberPath, ValidationResult result)
        {
            _failures ??= new List<ValidationFailure>();
            _failures.Add(new ValidationFailure(memberPath, result));
        }

        /// <summary>
        /// Adds all failures from another aggregate result, prepending a path prefix.
        /// </summary>
        public void AddFailures(string pathPrefix, AggregateValidationResult other)
        {
            if (other._failures is null || other._failures.Count == 0)
                return;

            _failures ??= new List<ValidationFailure>(other._failures.Count);
            foreach (var failure in other._failures)
            {
                var fullPath = string.IsNullOrEmpty(pathPrefix)
                    ? failure.MemberPath
                    : string.IsNullOrEmpty(failure.MemberPath)
                        ? pathPrefix
                        : $"{pathPrefix}.{failure.MemberPath}";
                _failures.Add(new ValidationFailure(fullPath, failure.Result));
            }
        }

        /// <summary>
        /// Whether any failures have been recorded.
        /// </summary>
        public bool HasFailures => _failures is not null && _failures.Count > 0;

        /// <summary>
        /// Builds the final result. Returns the shared Success instance if no failures.
        /// </summary>
        public AggregateValidationResult Build()
        {
            return _failures is null || _failures.Count == 0
                ? Success
                : new AggregateValidationResult(_failures);
        }
    }
}
