using Validations.Net.OLD.ValidationAttributes.Helpers;

namespace Validations.Net.OLD;

public class PredicateRegistrationException : Exception
{
    public PredicateRegistrationException(PredicateInfo predicateInfo, Type inputType,
        Exception innerException)
    {
        this.PredicateInfo = predicateInfo;
        this.InputType = inputType;
        this.InnerException = innerException;
    }

    public new Exception InnerException { get; }

    public Type InputType { get; }

    public PredicateInfo PredicateInfo { get; }
}
