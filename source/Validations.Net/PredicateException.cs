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
    public PredicateException(string message, Type inputType, PredicateInfo predicateInfo, Exception innerException) :
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
    public PredicateException(string message, Type inputType, PredicateInfo predicateInfo) : base(message)
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
}
