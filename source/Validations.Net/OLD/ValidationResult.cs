using SimpleBlackboard.Net;

namespace Validations.Net.OLD;

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
public record struct ValidationResult : IEquatable<ValidationResult>
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
    public readonly bool IsValid => this.ValidationException is null
        && this.PredicateException is null
        && (this._failures is null || this._failures.Count == 0);

    /// <summary>
    /// A cached successful validation result. Prefer this over <see cref="CreateFromValidationSuccess"/>
    /// for zero-allocation success returns.
    /// </summary>
    public static readonly ValidationResult Success = new(validationException: null, predicateException: null);

    /// <summary>
    /// Child results with <see cref="ValidationSeverity.Error"/> severity.
    /// Empty when the result is a leaf or a success.
    /// </summary>
    public IReadOnlyList<ValidationResult> Failures =>
        this._failures as IReadOnlyList<ValidationResult> ?? Array.Empty<ValidationResult>();

    /// <summary>
    /// Child results with <see cref="ValidationSeverity.Warning"/> severity.
    /// These do not affect <see cref="IsValid"/>. Empty when there are no warnings.
    /// </summary>
    public IReadOnlyList<ValidationResult> Warnings =>
        this._warnings as IReadOnlyList<ValidationResult> ?? Array.Empty<ValidationResult>();

    /// <summary>
    /// Whether any warning-severity results have been recorded.
    /// </summary>
    public readonly bool HasWarnings => this._warnings is not null && this._warnings.Count > 0;

    /// <summary>
    /// Allows a <see cref="ValidationResult"/> to be used directly in boolean expressions.
    /// Returns <see cref="IsValid"/>.
    /// </summary>
    public static implicit operator bool(ValidationResult result) => result.IsValid;

    /// <inheritdoc />
    public override readonly string ToString()
    {
        if (this.IsValid)
        {
            if (!this.HasWarnings)
                return "Valid";

            var wc = this._warnings!.Count;
            return $"Valid ({wc} warning{(wc != 1 ? "s" : "")})";
        }

        if (this.ExceptionMessage is not null && (this._failures is null || this._failures.Count == 0))
            return this.ExceptionMessage;

        var ec = this._failures?.Count ?? 0;
        var ewc = this._warnings?.Count ?? 0;

        if (ewc > 0)
            return $"Invalid ({ec} error{(ec != 1 ? "s" : "")}, {ewc} warning{(ewc != 1 ? "s" : "")})";

        return $"Invalid ({ec} error{(ec != 1 ? "s" : "")})";
    }

    /// <summary>
    /// Equality is based on validity state and the root exceptions, not the child lists.
    /// Deep structural comparison of nested failure trees is expensive and rarely needed.
    /// </summary>
    public readonly bool Equals(ValidationResult other) =>
        this.IsValid == other.IsValid
        && this.ValidationException == other.ValidationException
        && this.PredicateException == other.PredicateException;

    /// <inheritdoc />
    public override readonly int GetHashCode() =>
        HashCode.Combine(this.IsValid, this.ValidationException, this.PredicateException);

    private ValidationResult(ValidationException? validationException, PredicateException? predicateException)
    {
        this.ValidationException = validationException;
        this.PredicateException = predicateException;
        this.ExceptionMessage = validationException?.Message ?? predicateException?.Message;
        this.MemberPath = null;
        this.Severity = ValidationSeverity.Error;
        this._failures = null;
        this._warnings = null;
    }

    private ValidationResult(List<ValidationResult>? failures, List<ValidationResult>? warnings)
    {
        this.ValidationException = null;
        this.PredicateException = null;
        this.ExceptionMessage = null;
        this.MemberPath = null;
        this.Severity = ValidationSeverity.Error;
        this._failures = failures;
        this._warnings = warnings;
    }

    private ValidationResult(string memberPath, ValidationSeverity severity, ValidationResult leaf)
    {
        this.ValidationException = leaf.ValidationException;
        this.PredicateException = leaf.PredicateException;
        this.ExceptionMessage = leaf.ExceptionMessage;
        this.MemberPath = memberPath;
        this.Severity = severity;
        this._failures = null;
        this._warnings = null;
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
        object? propertyValue = null;
        foreach (var (key, val) in context)
        {
            if (key == "value") { propertyValue = val; break; }
        }
        message = ReplaceMessageTemplates(message, parameterName, propertyValue);
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
        object? propertyValue = null;
        if (context.TryGetValue<object?>("value", out var val))
            propertyValue = val;
        message = ReplaceMessageTemplates(message, parameterName, propertyValue);
        var exception = ValidationException.Create(validator, message, parameterName, blackboard, context);
        return CreateFromValidationFailure(exception);
    }

    private static string ReplaceMessageTemplates(string message, string? parameterName, object? propertyValue)
    {
        if (!message.Contains('{'))
            return message;

        if (message.Contains("{PropertyName}") && parameterName is not null)
            message = message.Replace("{PropertyName}", parameterName);

        if (message.Contains("{PropertyValue}") && propertyValue is not null)
            message = message.Replace("{PropertyValue}", propertyValue.ToString() ?? string.Empty);

        return message;
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
    /// Merges multiple <see cref="ValidationResult"/> instances into a single composite result.
    /// Leaf failures are added as error-severity children; composite results have their
    /// children and warnings merged in.
    /// </summary>
    /// <param name="results">The results to combine.</param>
    /// <returns>A composite <see cref="ValidationResult"/> containing all failures and warnings.</returns>
    public static ValidationResult Combine(params ValidationResult[] results)
    {
        var builder = CreateBuilder();
        foreach (var result in results)
        {
            builder.AddFailures("", result);
            builder.AddWarnings("", result);

            if (result.ValidationException is not null || result.PredicateException is not null)
                builder.AddFailure(result.MemberPath ?? string.Empty, result);
        }
        return builder.Build();
    }

    /// <summary>
    /// Throws the underlying exception if this result represents a failure.
    /// No-op when <see cref="IsValid"/> is true.
    /// </summary>
    /// <exception cref="ValidationException">Thrown when the result contains a validation failure.</exception>
    /// <exception cref="PredicateException">Thrown when the result contains a predicate failure.</exception>
    public readonly void ThrowIfInvalid()
    {
        if (this.IsValid)
            return;

        if (this.ValidationException is not null)
            throw this.ValidationException;
        if (this.PredicateException is not null)
            throw this.PredicateException;

        if (this._failures is not null && this._failures.Count > 0)
        {
            if (this._failures.Count == 1)
            {
                var first = this._failures[0];
                if (first.ValidationException is not null)
                    throw first.ValidationException;
                if (first.PredicateException is not null)
                    throw first.PredicateException;
            }

            var result = new ValidationResult(this._failures, null);
            throw ValidationException.CreateAggregate(
                "Validation", "Validation failed", null, null, result);
        }

        throw new InvalidOperationException("Validation failed");
    }

    /// <summary>
    /// Returns all error-severity children whose <see cref="MemberPath"/> matches the given path exactly.
    /// </summary>
    /// <param name="memberPath">The member path to match (e.g. "Name", "Address.City").</param>
    public readonly IReadOnlyList<ValidationResult> GetFailuresForMember(string memberPath)
    {
        return FilterByMemberPath(this._failures, memberPath);
    }

    /// <summary>
    /// Returns all warning-severity children whose <see cref="MemberPath"/> matches the given path exactly.
    /// </summary>
    /// <param name="memberPath">The member path to match (e.g. "Name", "Address.City").</param>
    public readonly IReadOnlyList<ValidationResult> GetWarningsForMember(string memberPath)
    {
        return FilterByMemberPath(this._warnings, memberPath);
    }

    /// <summary>
    /// Converts child failures into a dictionary mapping member paths to their error messages,
    /// matching the standard ASP.NET ModelState shape.
    /// </summary>
    /// <param name="includeWarnings">When true, warning-severity entries are included alongside errors.</param>
    public Dictionary<string, string[]> ToDictionary(bool includeWarnings = false)
    {
        var hasFailures = this._failures is not null && this._failures.Count > 0;
        var hasWarnings = includeWarnings && this._warnings is not null && this._warnings.Count > 0;

        if (!hasFailures && !hasWarnings)
            return new Dictionary<string, string[]>();

        var dict = new Dictionary<string, List<string>>();

        if (hasFailures)
            CollectMessages(dict, this._failures!);

        if (hasWarnings)
            CollectMessages(dict, this._warnings!);

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
        if (this._failures is null || this._failures.Count == 0)
            return Array.Empty<string>();

        return ExtractMessages(this._failures);
    }

    /// <summary>
    /// Returns all messages from warning-severity children as a flat list.
    /// </summary>
    public IReadOnlyList<string> GetAllWarningMessages()
    {
        if (this._warnings is null || this._warnings.Count == 0)
            return Array.Empty<string>();

        return ExtractMessages(this._warnings);
    }

    /// <summary>
    /// Returns all error-severity children as flattened (path, message) tuples.
    /// </summary>
    public readonly IReadOnlyList<(string Path, string Message)> GetAllErrors()
    {
        if (this._failures is null || this._failures.Count == 0)
            return Array.Empty<(string, string)>();

        var errors = new List<(string, string)>(this._failures.Count);
        foreach (var f in this._failures)
            if (f.ExceptionMessage is not null)
                errors.Add((f.MemberPath ?? string.Empty, f.ExceptionMessage));
        return errors;
    }

    /// <summary>
    /// Returns all warning-severity children as flattened (path, message) tuples.
    /// </summary>
    public readonly IReadOnlyList<(string Path, string Message)> GetAllWarningEntries()
    {
        if (this._warnings is null || this._warnings.Count == 0)
            return Array.Empty<(string, string)>();

        var entries = new List<(string, string)>(this._warnings.Count);
        foreach (var w in this._warnings)
            if (w.ExceptionMessage is not null)
                entries.Add((w.MemberPath ?? string.Empty, w.ExceptionMessage));
        return entries;
    }

    /// <summary>
    /// Creates a builder for constructing a composite result with child entries.
    /// Uses lazy allocation -- lists are only created when the first entry is added.
    /// </summary>
    internal static Builder CreateBuilder() => new();

    private static IReadOnlyList<ValidationResult> FilterByMemberPath(List<ValidationResult>? entries, string memberPath)
    {
        if (entries is null || entries.Count == 0)
            return Array.Empty<ValidationResult>();

        List<ValidationResult>? matches = null;
        foreach (var entry in entries)
        {
            if (string.Equals(entry.MemberPath, memberPath, StringComparison.Ordinal))
            {
                matches ??= new List<ValidationResult>();
                matches.Add(entry);
            }
        }

        return matches as IReadOnlyList<ValidationResult> ?? Array.Empty<ValidationResult>();
    }

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
            this._failures ??= new List<ValidationResult>();
            this._failures.Add(new ValidationResult(memberPath, ValidationSeverity.Error, result));
        }

        /// <summary>
        /// Adds a single warning-severity child entry.
        /// </summary>
        public void AddWarning(string memberPath, ValidationResult result)
        {
            this._warnings ??= new List<ValidationResult>();
            this._warnings.Add(new ValidationResult(memberPath, ValidationSeverity.Warning, result));
        }

        /// <summary>
        /// Merges all error-severity children from another composite result, prepending a path prefix.
        /// </summary>
        public void AddFailures(string pathPrefix, ValidationResult other)
        {
            var otherFailures = other.Failures;
            if (otherFailures.Count == 0)
                return;

            this._failures ??= new List<ValidationResult>(otherFailures.Count);
            foreach (var failure in otherFailures)
            {
                var fullPath = CombinePaths(pathPrefix, failure.MemberPath);
                this._failures.Add(new ValidationResult(fullPath, ValidationSeverity.Error, failure));
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

            this._warnings ??= new List<ValidationResult>(otherWarnings.Count);
            foreach (var warning in otherWarnings)
            {
                var fullPath = CombinePaths(pathPrefix, warning.MemberPath);
                this._warnings.Add(new ValidationResult(fullPath, ValidationSeverity.Warning, warning));
            }
        }

        /// <summary>
        /// Whether any error-severity entries have been recorded.
        /// </summary>
        public bool HasFailures => this._failures is not null && this._failures.Count > 0;

        /// <summary>
        /// Builds the final composite result. Returns a success if no entries were added.
        /// </summary>
        public ValidationResult Build()
        {
            var hasFailures = this._failures is not null && this._failures.Count > 0;
            var hasWarnings = this._warnings is not null && this._warnings.Count > 0;

            if (!hasFailures && !hasWarnings)
                return CreateFromValidationSuccess();

            return new ValidationResult(
                hasFailures ? this._failures : null,
                hasWarnings ? this._warnings : null);
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
