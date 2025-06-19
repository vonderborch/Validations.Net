using System.Reflection;
using Validations.Net.ValidationAttributes.Helpers;

namespace Validations.Net;

public class PredicateRegistrationException : Exception
{
    public PredicateRegistrationException(PredicateInfo predicateInfo, Type inputType,
        Exception innerException)
    {
        this.PredicateInfo = predicateInfo;
        this.InputType = inputType;
        this.InnerException = innerException;
    }

    public PredicateInfo PredicateInfo { get; }

    public new Exception InnerException { get; }

    public Type InputType { get; }
}
