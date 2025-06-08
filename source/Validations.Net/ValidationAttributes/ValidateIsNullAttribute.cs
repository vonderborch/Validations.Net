using SimpleBlackboard.Net;
using Validations.Net.Validators;

namespace Validations.Net.ValidationAttributes;

public class ValidateIsNullAttribute() : ValidationAttribute("IsNull")
{
    public override bool Check(object? value)
    {
        bool result = value.CheckIsNull();
        return result;
    }

    public override void Validate(object? value, string propertyName, Blackboard? blackboard = null)
    {
        value.ValidateIsNull(propertyName, blackboard);
    }
}
