using Validations.Net.Predicates;

namespace Validations.Net;

/// <summary>
/// Represents an exception that occurs when a predicate within a validation
/// operation fails to satisfy its defined condition or logic.
/// </summary>
public class PredicateException : Exception
{
    /// <summary>
    /// Represents an exception that occurs when a predicate validation fails.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="inputType">The input value type.</param>
    /// <param name="predicateInfo">Information on the predicate involved.</param>
    /// <param name="innerException">The inner exception.</param>
    private PredicateException(string message, Type inputType, PredicateInfo predicateInfo, Exception innerException) :
        base(message, innerException)
    {
        this.InputType = inputType;
        this.PredicateInfo = predicateInfo;
    }

    /// <summary>
    /// Represents an exception that occurs when a predicate validation fails.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="inputType">The input value type.</param>
    /// <param name="predicateInfo">Information on the predicate involved.</param>
    private PredicateException(string message, Type inputType, PredicateInfo predicateInfo) : base(message)
    {
        this.InputType = inputType;
        this.PredicateInfo = predicateInfo;
    }

    /// <summary>
    /// Represents the type of input data associated with a validation exception.
    /// Indicates the type of the data that failed a validation check.
    /// </summary>
    public readonly Type InputType;

    /// <summary>
    /// Represents information on a predicate used for validation logic within the validation system.
    /// </summary>
    public readonly PredicateInfo PredicateInfo;

    /// <summary>
    /// Creates a new instance of the <see cref="PredicateException"/> class
    /// using the specified message, input type, and predicate information.
    /// </summary>
    /// <param name="message">The exception message describing the failure.</param>
    /// <param name="inputType">The type of the input that caused the predicate validation to fail.</param>
    /// <param name="predicateInfo">Information on the predicate involved in the failure.</param>
    /// <param name="innerException">The inner exception that caused the predicate validation to fail.</param>
    /// <returns>A new instance of the <see cref="PredicateException"/> class.</returns>
    public static PredicateException Create(string message, Type inputType, PredicateInfo predicateInfo, Exception? innerException = null)
    {
        if (innerException != null)
        {
            return new(message, inputType, predicateInfo, innerException);
        }

        return new(message, inputType, predicateInfo);
    }
}
