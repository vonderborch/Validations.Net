using Validations.Net.Predicates;

namespace Validations.Net;

/// <summary>
/// Represents an exception that occurs when a predicate registration fails.
/// </summary>
public class PredicateRegistrationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PredicateRegistrationException"/> class.
    /// </summary>
    /// <param name="predicateInfo">The predicate information.</param>
    /// <param name="inputType">The input type.</param>
    /// <param name="innerException">The inner exception.</param>
    public PredicateRegistrationException(PredicateInfo predicateInfo, Type inputType,
        Exception innerException) : base($"Failed to register predicate '{predicateInfo.Name}' for type '{inputType.FullName}'", innerException)
    {
        this.PredicateInfo = predicateInfo;
        this.InputType = inputType;
    }

    /// <summary>
    /// Gets the input type.
    /// </summary>
    public Type InputType { get; }

    /// <summary>
    /// Gets the predicate information.
    /// </summary>
    public PredicateInfo PredicateInfo { get; }
}