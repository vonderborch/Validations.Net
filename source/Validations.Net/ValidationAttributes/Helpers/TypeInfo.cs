using SimpleBlackboard.Net;

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
    /// Represents structured type information, including validation results and exception details
    /// for type mismatches during type conversion operations.
    /// </summary>
    /// <typeparam name="T">The expected type for the conversion process.</typeparam>
    /// <param name="validatorName">The name of the validator associated with the GetCorrectType request.</param>
    /// <param name="parameterName">The parameter name.</param>
    /// <param name="originalValue">The original value.</param>
    /// <param name="allowNull">Whether nulls were allowed.</param>
    /// <param name="instance">The instance associated with the GetCorrectType request.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blackboard">The blackboard.</param>
    public TypeInfo(string validatorName, string parameterName, object? originalValue, bool allowNull, object? instance,
        string? propertyName, Blackboard? blackboard)
    {
        IsCorrectType = false;
        Exception = ValidationException.CreateFromTypeMisMatch<T>(validatorName, parameterName, originalValue,
            blackboard);
        Exception.ExceptionContext.SetValue("allowNull", allowNull);
        Exception.ExceptionContext.SetValue("instance", instance);
        Exception.ExceptionContext.SetValue("propertyName", propertyName);
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
