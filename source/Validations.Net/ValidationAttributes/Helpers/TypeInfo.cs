namespace Validations.Net.ValidationAttributes.Helpers;

/// <summary>
/// Represents information about a type conversion attempt, including success status and any conversion errors.
/// Used in validation scenarios to handle type conversion safely.
/// </summary>
/// <typeparam name="T">The target type for conversion.</typeparam>
public record struct TypeInfo<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypeInfo{T}"/> struct representing a successful type conversion.
    /// </summary>
    /// <param name="value">The successfully converted value of type <typeparamref name="T"/>.</param>
    public TypeInfo(T? value)
    {
        IsCorrectType = true;
        ConvertedValue = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeInfo{T}"/> struct representing a failed type conversion.
    /// Creates an appropriate validation exception with detailed information about the type mismatch.
    /// </summary>
    /// <param name="validatorName">The name of the validator where the type mismatch occurred.</param>
    /// <param name="parameterName">The name of the parameter with the incorrect type.</param>
    /// <param name="originalValue">The original value that could not be converted to type <typeparamref name="T"/>.</param>
    public TypeInfo(string validatorName, string parameterName, object? originalValue)
    {
        IsCorrectType = false;
        Exception = ValidationException.CreateFromTypeMisMatch<T>(validatorName, parameterName, originalValue);
    }
    
    /// <summary>
    /// Gets a value indicating whether the type conversion was successful.
    /// </summary>
    /// <value><c>true</c> if the original value was successfully converted to type <typeparamref name="T"/>; otherwise, <c>false</c>.</value>
    public bool IsCorrectType { get; }

    /// <summary>
    /// Gets the value converted to type <typeparamref name="T"/> if the conversion was successful.
    /// </summary>
    /// <value>The converted value if <see cref="IsCorrectType"/> is <c>true</c>; otherwise, the default value for type <typeparamref name="T"/>.</value>
    public T? ConvertedValue { get; init; } = default;

    /// <summary>
    /// Gets the validation exception containing details about the type conversion failure.
    /// </summary>
    /// <value>A <see cref="ValidationException"/> if <see cref="IsCorrectType"/> is <c>false</c>; otherwise, <c>null</c>.</value>
    public ValidationException? Exception { get; init; } = null;
}
