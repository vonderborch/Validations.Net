using SimpleBlackboard.Net;

namespace Validations.Net;

/// <summary>
/// Base class for validation attributes
/// Provides validation functionality to check and validate property values against specific rules.
/// </summary>
/// <param name="name">The name of the validator.</param>
public abstract class ValidationAttribute(string name) : Attribute
{
    /// <summary>
    /// The name of the validator.
    /// </summary>
    public string ValidatorName { get; } = name;
    
    /// <summary>
    /// Checks if the provided value meets the validation criteria.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is valid; otherwise, false.</returns>
    public abstract bool Check(object? value);

    /// <summary>
    /// Attempts to validate the provided value against the validation criteria.
    /// If validation fails, the exception is caught and returned instead of being thrown.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard for additional context.</param>
    /// <returns>An instance of <see cref="ValidationException"/> if validation fails; otherwise, null.</returns>
    public Exception? SafeValidate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        try
        {
            Validate(value, propertyName, blackboard);
            return null;
        }
        catch (ValidationException ex)
        {
            return ex;
        }
    }

    /// <summary>
    /// Validates the provided value against the validation criteria.
    /// Throws a <see cref="ValidationException"/> if the validation fails.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">An optional blackboard for additional context.</param>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public abstract void Validate(object? value, string propertyName, Blackboard? blackboard = null);

    /// <summary>
    /// Attempts to cast the provided value to the specified type.
    /// Throws a <see cref="ValidationException"/> if the conversion fails.
    /// </summary>
    /// <typeparam name="T">The target type to convert the value into.</typeparam>
    /// <param name="value">The value to cast to the specified type.</param>
    /// <param name="parameterName">The name of the parameter being validated.</param>
    /// <returns>The value cast to the specified type.</returns>
    /// <exception cref="ValidationException">Thrown when the value cannot be cast to the specified type.</exception>
    public T GetCorrectType<T>(object? value, string parameterName)
    {
        if (value is not T typedValue)
        {
            throw ValidationException.CreateFromTypeMisMatch<T>(ValidatorName, parameterName, value);
        }

        return typedValue;
    }
}
