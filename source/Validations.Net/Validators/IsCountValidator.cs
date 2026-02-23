using System.Runtime.CompilerServices;
using SimpleBlackboard.Net;

namespace Validations.Net.Validators;

public readonly record struct IsCountParams(int Count);

public sealed class IsCountValidator : IValidator
{
    public IsCountParams Params { get; }

    public IsCountValidator(int count)
    {
        Params = new IsCountParams(count);
    }

    public string Name => IsCount.ValidatorName;
    public string DefaultFailureMessage => IsCount.DefaultValidationFailureMessage;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValidationResult Validate(object? value, string? memberName = null, IBlackboard? blackboard = null)
    {
        if (value is System.Collections.IEnumerable enumerable && enumerable.CheckIsCount(Params.Count))
            return ValidationResult.CreateFromValidationSuccess();
        int actualCount = value switch
        {
            System.Collections.ICollection c => c.Count,
            System.Collections.IEnumerable e => CountEnumerable(e),
            _ => -1
        };
        return ValidationResult.CreateFromValidationFailure(Name, DefaultFailureMessage, memberName, blackboard,
            [("value", value), ("expectedCount", Params.Count), ("actualCount", actualCount)]);
    }

    private static int CountEnumerable(System.Collections.IEnumerable enumerable)
    {
        if (enumerable is System.Collections.ICollection c) return c.Count;
        int count = 0;
        var enumerator = enumerable.GetEnumerator();
        try
        {
            while (enumerator.MoveNext()) count++;
            return count;
        }
        finally { (enumerator as IDisposable)?.Dispose(); }
    }
}
