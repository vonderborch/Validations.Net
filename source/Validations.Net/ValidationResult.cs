using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Represents the result of one or more validation operations. A single <see cref="ValidationResult"/>
/// can represent a leaf outcome (success or individual failure), or a composite outcome containing
/// child <see cref="ValidationResult"/> entries with member-path tracking.
/// <para>
/// Failures with <see cref="ValidationSeverity.Error"/> severity affect <see cref="IsValid"/>;
/// failures with <see cref="ValidationSeverity.Warning"/> severity are collected in <see cref="Warnings"/>
/// without blocking validity.
/// </para>
/// </summary>
public record struct ValidationResult
{
    /// <summary>
    /// The exception produced by a validation rule failure, if any.
    /// </summary>
    public readonly ValidationException? ValidationException;

    /// <summary>
    /// The exception produced by a predicate failure, if any.
    /// </summary>
    public readonly PredicateException? PredicateException;

    /// <summary>
    /// The error message from the underlying exception, if any.
    /// </summary>
    public readonly string? ExceptionMessage;

    /// <summary>
    /// Dot-separated path to the member that failed validation (e.g. "Address.City", "Items[2].Price").
    /// Null for leaf results returned by individual validators; set when the result is a child of a composite.
    /// </summary>
    public readonly string? MemberPath;

    /// <summary>
    /// Whether this entry is a blocking error or a non-blocking warning.
    /// Only meaningful on child entries inside <see cref="Failures"/> or <see cref="Warnings"/>.
    /// </summary>
    public readonly ValidationSeverity Severity;

    private readonly List<ValidationResult>? _failures;
    private readonly List<ValidationResult>? _warnings;

    /// <summary>
    /// Whether the validation passed. True when there is no exception and no error-severity child failures.
    /// Warning-severity failures do not affect this value.
    /// </summary>
    public bool IsValid => ValidationException is null
        && PredicateException is null
        && (_failures is null || _failures.Count == 0);

    /// <summary>
    /// Child results with <see cref="ValidationSeverity.Error"/> severity.
    /// Empty when the result is a leaf or a success.
    /// </summary>
    public IReadOnlyList<ValidationResult> Failures =>
        _failures as IReadOnlyList<ValidationResult> ?? Array.Empty<ValidationResult>();

    /// <summary>
    /// Child results with <see cref="ValidationSeverity.Warning"/> severity.
    /// These do not affect <see cref="IsValid"/>. Empty when there are no warnings.
    /// </summary>
    public IReadOnlyList<ValidationResult> Warnings =>
        _warnings as IReadOnlyList<ValidationResult> ?? Array.Empty<ValidationResult>();

    /// <summary>
    /// Whether any warning-severity results have been recorded.
    /// </summary>
    public bool HasWarnings => _warnings is not null && _warnings.Count > 0;

    private ValidationResult(ValidationException? validationException, PredicateException? predicateException)
    {
        ValidationException = validationException;
        PredicateException = predicateException;
        ExceptionMessage = validationException?.Message ?? predicateException?.Message;
        MemberPath = null;
        Severity = ValidationSeverity.Error;
        _failures = null;
        _warnings = null;
    }

    private ValidationResult(List<ValidationResult>? failures, List<ValidationResult>? warnings)
    {
        ValidationException = null;
        PredicateException = null;
        ExceptionMessage = null;
        MemberPath = null;
        Severity = ValidationSeverity.Error;
        _failures = failures;
        _warnings = warnings;
    }

    private ValidationResult(string memberPath, ValidationSeverity severity, ValidationResult leaf)
    {
        ValidationException = leaf.ValidationException;
        PredicateException = leaf.PredicateException;
        ExceptionMessage = leaf.ExceptionMessage;
        MemberPath = memberPath;
        Severity = severity;
        _failures = null;
        _warnings = null;
    }

    /// <summary>
    /// Creates a result representing a successful validation.
    /// </summary>
    public static ValidationResult CreateFromValidationSuccess()
    {
        return new ValidationResult(validationException: null, predicateException: null);
    }

    /// <summary>
    /// Creates a result representing a validation failure.
    /// </summary>
    /// <param name="validationException">The exception describing the failure.</param>
    public static ValidationResult CreateFromValidationFailure(ValidationException validationException)
    {
        return new ValidationResult(validationException, null);
    }

    /// <summary>
    /// Creates a result representing a validation failure from constituent parts.
    /// </summary>
    public static ValidationResult CreateFromValidationFailure(
        string validator, string message, string? parameterName, IBlackboard? blackboard,
        IEnumerable<(string key, object? value)> context)
    {
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);
        return CreateFromValidationFailure(exception);
    }

    /// <summary>
    /// Creates a result representing a validation failure from constituent parts.
    /// </summary>
    public static ValidationResult CreateFromValidationFailure(
        string validator, string message, string? parameterName, IBlackboard? blackboard,
        ValidationContext context)
    {
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);
        return CreateFromValidationFailure(exception);
    }

    /// <summary>
    /// Creates a result representing a predicate failure.
    /// </summary>
    /// <param name="predicateException">The exception describing the predicate failure.</param>
    public static ValidationResult CreateFromPredicateFailure(PredicateException predicateException)
    {
        return new ValidationResult(null, predicateException);
    }

    /// <summary>
    /// Converts child failures into a dictionary mapping member paths to their error messages,
    /// matching the standard ASP.NET ModelState shape.
    /// </summary>
    /// <param name="includeWarnings">When true, warning-severity entries are included alongside errors.</param>
    public Dictionary<string, string[]> ToDictionary(bool includeWarnings = false)
    {
        var hasFailures = _failures is not null && _failures.Count > 0;
        var hasWarnings = includeWarnings && _warnings is not null && _warnings.Count > 0;

        if (!hasFailures && !hasWarnings)
            return new Dictionary<string, string[]>();

        var dict = new Dictionary<string, List<string>>();

        if (hasFailures)
            CollectMessages(dict, _failures!);

        if (hasWarnings)
            CollectMessages(dict, _warnings!);

        var result = new Dictionary<string, string[]>(dict.Count);
        foreach (var kvp in dict)
            result[kvp.Key] = kvp.Value.ToArray();

        return result;
    }

    /// <summary>
    /// Returns all error messages from error-severity children as a flat list.
    /// </summary>
    public IReadOnlyList<string> GetAllErrorMessages()
    {
        if (_failures is null || _failures.Count == 0)
            return Array.Empty<string>();

        return ExtractMessages(_failures);
    }

    /// <summary>
    /// Returns all messages from warning-severity children as a flat list.
    /// </summary>
    public IReadOnlyList<string> GetAllWarningMessages()
    {
        if (_warnings is null || _warnings.Count == 0)
            return Array.Empty<string>();

        return ExtractMessages(_warnings);
    }

    /// <summary>
    /// Creates a builder for constructing a composite result with child entries.
    /// Uses lazy allocation -- lists are only created when the first entry is added.
    /// </summary>
    internal static Builder CreateBuilder() => new();

    private static void CollectMessages(Dictionary<string, List<string>> dict, List<ValidationResult> entries)
    {
        foreach (var entry in entries)
        {
            var key = entry.MemberPath ?? string.Empty;
            if (!dict.TryGetValue(key, out var list))
            {
                list = new List<string>();
                dict[key] = list;
            }

            if (entry.ExceptionMessage is not null)
                list.Add(entry.ExceptionMessage);
        }
    }

    private static List<string> ExtractMessages(List<ValidationResult> entries)
    {
        var messages = new List<string>(entries.Count);
        foreach (var entry in entries)
        {
            if (entry.ExceptionMessage is not null)
                messages.Add(entry.ExceptionMessage);
        }
        return messages;
    }

    /// <summary>
    /// Mutable builder for constructing a composite <see cref="ValidationResult"/> with lazy list allocation.
    /// </summary>
    internal sealed class Builder
    {
        private List<ValidationResult>? _failures;
        private List<ValidationResult>? _warnings;

        /// <summary>
        /// Adds a single error-severity child entry.
        /// </summary>
        public void AddFailure(string memberPath, ValidationResult result)
        {
            _failures ??= new List<ValidationResult>();
            _failures.Add(new ValidationResult(memberPath, ValidationSeverity.Error, result));
        }

        /// <summary>
        /// Adds a single warning-severity child entry.
        /// </summary>
        public void AddWarning(string memberPath, ValidationResult result)
        {
            _warnings ??= new List<ValidationResult>();
            _warnings.Add(new ValidationResult(memberPath, ValidationSeverity.Warning, result));
        }

        /// <summary>
        /// Merges all error-severity children from another composite result, prepending a path prefix.
        /// </summary>
        public void AddFailures(string pathPrefix, ValidationResult other)
        {
            var otherFailures = other.Failures;
            if (otherFailures.Count == 0)
                return;

            _failures ??= new List<ValidationResult>(otherFailures.Count);
            foreach (var failure in otherFailures)
            {
                var fullPath = CombinePaths(pathPrefix, failure.MemberPath);
                _failures.Add(new ValidationResult(fullPath, ValidationSeverity.Error, failure));
            }
        }

        /// <summary>
        /// Merges all warning-severity children from another composite result, prepending a path prefix.
        /// </summary>
        public void AddWarnings(string pathPrefix, ValidationResult other)
        {
            var otherWarnings = other.Warnings;
            if (otherWarnings.Count == 0)
                return;

            _warnings ??= new List<ValidationResult>(otherWarnings.Count);
            foreach (var warning in otherWarnings)
            {
                var fullPath = CombinePaths(pathPrefix, warning.MemberPath);
                _warnings.Add(new ValidationResult(fullPath, ValidationSeverity.Warning, warning));
            }
        }

        /// <summary>
        /// Whether any error-severity entries have been recorded.
        /// </summary>
        public bool HasFailures => _failures is not null && _failures.Count > 0;

        /// <summary>
        /// Builds the final composite result. Returns a success if no entries were added.
        /// </summary>
        public ValidationResult Build()
        {
            var hasFailures = _failures is not null && _failures.Count > 0;
            var hasWarnings = _warnings is not null && _warnings.Count > 0;

            if (!hasFailures && !hasWarnings)
                return CreateFromValidationSuccess();

            return new ValidationResult(
                hasFailures ? _failures : null,
                hasWarnings ? _warnings : null);
        }

        private static string CombinePaths(string prefix, string? path)
        {
            if (string.IsNullOrEmpty(prefix))
                return path ?? string.Empty;
            if (string.IsNullOrEmpty(path))
                return prefix;
            return $"{prefix}.{path}";
        }
    }
}
