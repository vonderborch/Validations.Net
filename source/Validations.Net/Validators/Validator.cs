namespace Validations.Net.Validators;

public static class Validator
{
    public static bool CheckIsValidClass<T>(this T value) where T : class
    {
        value.ValidateIsNotNull(nameof(value));
    }
    
    public static bool CheckIsValidStruct<T>(this T value) where T : struct
    {
        value.ValidateIsNotNull(nameof(value));
    }
    
    public static T ValidateIsValidClass<T>(this T value) where T : class
    {
    }
    
    public static T ValidateIsValidStruct<T>(this T value) where T : struct
    {
    }
}
