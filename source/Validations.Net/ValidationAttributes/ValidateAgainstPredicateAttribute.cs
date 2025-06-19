using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

/// <summary>
///     Attribute that validates a value against a registered predicate function.
/// </summary>
/// <typeparam name="T">The type of the value to validate.</typeparam>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ValidateAgainstPredicateAttribute<T> : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the ValidateAgainstPredicateAttribute class.
    /// </summary>
    /// <param name="predicateName">The name of the registered predicate function to use for validation.</param>
    /// <param name="predicateGroup">The group name of the predicate function. Default is "default".</param>
    /// <exception cref="ValidationException">Thrown when the specified predicate function is not found.</exception>
    public ValidateAgainstPredicateAttribute(string predicateName, string predicateGroup = "default") : base(
        "AgainstPredicate")
    {
        // Find a method/delegate with the specified name in all loaded assemblies
        Func<T, bool>? method =
            PredicateRegistrar.GetPredicate<T>(predicateName, predicateGroup);

        method.ValidateIsNotNull(nameof(method));
        this.Predicate = method!;
    }

    /// <summary>
    ///     Gets the predicate function used for validation.
    /// </summary>
    public Func<T?, bool> Predicate { get; }

    /// <summary>
    ///     Checks if the provided value satisfies the predicate function.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <returns>True if the value satisfies the predicate function, false otherwise.</returns>
    public override bool Check(object? value, object? instance)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        return typedValue.CheckAgainstPredicate(this.Predicate);
    }

    /// <summary>
    ///     Validates if the provided value satisfies the predicate function and throws a ValidationException if it does not.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="instance">The instance the value is associated with.</param>
    /// <param name="propertyName">The name of the property being validated.</param>
    /// <param name="blackboard">Optional blackboard for storing validation context.</param>
    /// <exception cref="ValidationException">Thrown when the value does not satisfy the predicate function.</exception>
    public override void Validate(object? value, object? instance, string propertyName, Blackboard? blackboard = null)
    {
        T typedValue = GetCorrectType<T>(value, nameof(value));
        typedValue.ValidateAgainstPredicate(this.Predicate, propertyName, blackboard);
    }
}
