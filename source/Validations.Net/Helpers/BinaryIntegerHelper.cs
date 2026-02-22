using System.Numerics;

namespace Validations.Net.Helpers;

/// <summary>
/// Provides a generic dispatch mechanism for <see cref="IBinaryInteger{T}"/> operations
/// on boxed numeric values. Used by validation attributes that receive <c>object?</c>
/// and need to delegate to generic-math–constrained validators.
/// </summary>
internal static class BinaryIntegerHelper
{
    /// <summary>
    /// Contract for an operation that can be executed against any <see cref="IBinaryInteger{T}"/>.
    /// Implement as a struct to allow JIT devirtualization.
    /// </summary>
    internal interface IBinaryIntegerOperation<TResult>
    {
        TResult Execute<T>(T value) where T : IBinaryInteger<T>;
    }

    /// <summary>
    /// Dispatches a boxed value to the supplied <paramref name="operation"/> after matching
    /// it to a concrete <see cref="IBinaryInteger{T}"/> type. Returns <paramref name="fallback"/>
    /// when the runtime type is not a recognised built-in integer type.
    /// </summary>
    internal static TResult Dispatch<TResult>(
        object value,
        IBinaryIntegerOperation<TResult> operation,
        TResult fallback)
    {
        return value switch
        {
            int v => operation.Execute(v),
            long v => operation.Execute(v),
            short v => operation.Execute(v),
            byte v => operation.Execute(v),
            uint v => operation.Execute(v),
            ulong v => operation.Execute(v),
            ushort v => operation.Execute(v),
            sbyte v => operation.Execute(v),
            nint v => operation.Execute(v),
            nuint v => operation.Execute(v),
            Int128 v => operation.Execute(v),
            UInt128 v => operation.Execute(v),
            BigInteger v => operation.Execute(v),
            _ => fallback
        };
    }
}
