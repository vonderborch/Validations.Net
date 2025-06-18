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

To use the library, simply use one of the available extension methods on the type you want to validate. For example:

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
        var validationResult = person.Validate();

        if (validationResult.IsValid)
        {
            Console.WriteLine("Person is valid!");
        }
        else
        {
            Console.WriteLine("Validation failed: " + validationResult.ErrorMessage);
        }
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
