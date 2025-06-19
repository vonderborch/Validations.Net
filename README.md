# Validations.Net

![Logo](https://raw.githubusercontent.com/vonderborch/Validations.Net/refs/heads/main/logo.png)

A library enabling validations for various types of objects in .NET as extensions to base types.

## Installation

### Nuget

[![NuGet version (Validations.Net)](https://img.shields.io/nuget/v/Validations.Net.svg?style=flat-square)](https://www.nuget.org/packages/Validations.Net/)

The recommended installation approach is to use the available nuget
package: [Validations.Net](https://www.nuget.org/packages/Validations.Net/)

### Clone

Alternatively, you can clone this repo and reference the Validations.Net project in your project.

## How to Use

There are three approaches to using the validation functionality in this library (and they can, of course, be mixed):

- `Check` or `Validate` Extension Methods: extension methods prefixed with `Check` or `Validate`. `Check` methods return
    a boolean representing the success of the validation. `Validate` methods return the value tested if successful, or
    raise a `ValidationException` error if not successful.
- `GetValidationResultFor` Extension Methods: extension methods prefixed with `GetValidationResultFor`. Returns a
    `ValidationResult` struct with the result of the validation.
- Validation Attributes: attributes that can be added to the members of a class or struct to determine if instances are
    valid. Validation against classes or structs with these attributes is handled via the `IsValid` or `IsNotValid`
    extension methods.

**Documentation on each available validation method is available in the [Docs](Docs/index.md)**

### Check/Validate Extension Methods

To use the `Check` or `Validate` extension methods approach, simply use one of the available `Check` or `Validate`
extension methods on the type you want to validate. For example:

```csharp
using Validations.Net;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        var person = new Person { Name = "John", Age = 30 };

        // Validate the person object
        if (!person.CheckIsNotNull()) 
        {
            Console.WriteLine("Person is null");
        }
        person.ValidateIsNotNull(nameof(person));
        
        person.Name.ValidateIsEquals("John", nameof(person));
        person.Age.ValidateGreaterThan(18, nameof(person));
    }
}

```

### GetValidationResultFor Extension Methods


To use the `GetValidationResultFor` extension methods approach, simply use one of the available `GetValidationResultFor` extension methods
on the type you want to validate. For example:

```csharp
using Validations.Net;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        var person = new Person { Name = "John", Age = 30 };

        // Validate the person object
        if (!person.GetValidationResultForIsNotNull(nameof(person)).IsValid) 
        {
            Console.WriteLine("Person is null");
        }
        
        person.Name.GetValidationResultForIsEquals("John", nameof(person));
        person.Age.GetValidationResultForGreaterThan(18, nameof(person));
    }
}

```

### Validation Attributes

To use the extension methods approach, simply use one of the available extension methods on the type you want to validate. For example:

```csharp
using Validations.Net;

[ValidateIsNotNull]
public class Person
{
    [ValidateIsEquals("John")]
    public string Name { get; set; }
    
    [ValidateGreaterThan(18)]
    public int Age { get; set; }
}

public class Program
{
    public static void Main()
    {
        var person = new Person { Name = "John", Age = 30 };

        // Validate the person object
        if (!person.CheckIsValid()) 
        {
            Console.WriteLine("Person is not valid");
        }
        person.ValidateIsValid(nameof(person));
    }
}

```

## Development

1. Clone or fork the repo
2. Create a new branch
3. Code!
4. Push your changes and open a PR
5. Once approved, they'll be merged in
6. Profit!

## Future Plans

See list of issues under the Milestones: https://github.com/vonderborch/Validations.Net/milestones
