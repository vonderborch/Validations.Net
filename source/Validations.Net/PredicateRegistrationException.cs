using System.Reflection;

namespace Validations.Net;

public class PredicateRegistrationException : Exception
{
    public PredicateRegistrationException(string name, string group, Type inputType, MethodInfo methodInfo,
        Exception innerException)
    {
        this.Name = name;
        this.Group = group;
        this.InputType = inputType;
        this.MethodInfo = methodInfo;
        this.InnerException = innerException;
    }

    public string Group { get; }

    public Exception InnerException { get; }

    public Type InputType { get; }

    public MethodInfo MethodInfo { get; }

    public string Name { get; }
}
