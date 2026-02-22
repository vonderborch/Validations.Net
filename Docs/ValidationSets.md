# Validation Sets

ValidationSets provide a fluent builder API for composing multiple validation steps into a single, reusable validation pipeline. All steps are executed (no short-circuit), and failures are collected into an `AggregateValidationResult`.

## Entry Point

Create a validation set with `ValidationSet.For<T>()`:

```csharp
using Validations.Net.ValidationSets;

var set = ValidationSet.For<User>().Build();
```

## Builder Methods

### Add Custom Steps

Add custom validation logic with a delegate:

```csharp
var set = ValidationSet
    .For<User>()
    .Add((user, blackboard) =>
    {
        if (user.Age >= 18 && string.IsNullOrEmpty(user.GuardianName))
            return ValidationResult.CreateFromValidationSuccess();
        if (user.Age < 18 && !string.IsNullOrEmpty(user.GuardianName))
            return ValidationResult.CreateFromValidationSuccess();
        return ValidationResult.CreateFromValidationFailure(
            "GuardianRequired", "Minors must have a guardian name", null, blackboard,
            new List<(string, object?)> { ("value", user) });
    })
    .Build();
```

### Convenience Methods

The builder includes convenience methods for common validators:

| Method | Description |
|--------|-------------|
| `AddIsNotNull(string? message)` | Value must not be null |
| `AddIsNotNull<TMember>(Func<T, TMember?> selector, string? message)` | Selected member must not be null |
| `AddIsInRange<TValue>(Func<T, TValue> selector, TValue min, TValue max, ...)` | Selected value in range |
| `AddIsNotNullOrEmpty(Func<T, string?> selector, string? message)` | Selected string not null/empty |
| `AddIsNotNullOrWhiteSpace(Func<T, string?> selector, string? message)` | Selected string not null/whitespace |
| `AddFromType()` | Run all ValidationAttributes on the type |

### AddFromType

`AddFromType()` integrates attribute-based validation into the set. It runs `ValidationRunner.Validate` on the value and adds any failures to the aggregate result.

```csharp
var set = ValidationSet
    .For<User>()
    .AddIsNotNull()
    .AddFromType()   // Also validates [ValidateIsNotNullOrWhiteSpace] on Name, etc.
    .Build();
```

## Conditional Validation with When

Use `.When()` to run subsequent steps only when a condition is true. Call `.EndWhen()` to clear the condition.

```csharp
var set = ValidationSet
    .For<Order>()
    .AddIsNotNull()
    .When(o => o.ShippingAddress is not null)
    .AddIsNotNullOrWhiteSpace(o => o.ShippingAddress!.Street, "Street is required")
    .AddIsNotNullOrWhiteSpace(o => o.ShippingAddress!.City, "City is required")
    .EndWhen()
    .AddFromType()
    .Build();
```

Steps added after `When(predicate)` are wrapped in a conditional: if the predicate returns `false`, the step is skipped (returns success).

## Building and Executing

Call `.Build()` to create an immutable `ValidationSet<T>`:

```csharp
var set = ValidationSet
    .For<User>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(u => u.Name, "Name is required")
    .AddIsInRange(u => u.Age, 0, 150)
    .Build();

// Execute — returns AggregateValidationResult with all failures
var result = set.Execute(user);

// Check — returns true if all steps pass
bool ok = set.Check(user);

// Ensure — returns value if valid, throws ValidationException if any step fails
User validated = set.Ensure(user);
```

### Execute

`Execute(T value, IBlackboard? blackboard = null)` runs all steps and collects every failure into an `AggregateValidationResult`:

```csharp
var result = set.Execute(user);
if (!result.IsValid)
{
    foreach (var failure in result.Failures)
        Console.WriteLine($"{failure.MemberPath}: {failure.ErrorMessage}");
}
```

### Check

`Check(T value, IBlackboard? blackboard = null)` returns `true` if all steps pass, `false` otherwise. Useful when you only need a boolean.

### Ensure

`Ensure(T value, IBlackboard? blackboard = null, string validationFailureMessage = "Validation failed", string? parameterName = null)` returns the value if valid. If any step fails, it throws the first failure's `ValidationException`, or a generic `ValidationException` if needed.

## Complete Example

```csharp
public class RegistrationRequest
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public int Age { get; set; }
}

var registrationSet = ValidationSet
    .For<RegistrationRequest>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(r => r.Username, "Username is required")
    .AddIsNotNullOrWhiteSpace(r => r.Email, "Email is required")
    .AddIsNotNullOrWhiteSpace(r => r.Password, "Password is required")
    .AddIsInRange(r => r.Age, 13, 120, message: "Age must be between 13 and 120")
    .AddFromType()  // If RegistrationRequest has ValidationAttributes
    .Build();

var request = new RegistrationRequest { Username = "alice", Email = "alice@example.com", Password = "secret", Age = 25 };
var result = registrationSet.Execute(request);

if (result.IsValid)
{
    // Proceed with registration
}
else
{
    var errors = result.ToDictionary();  // For ASP.NET ModelState
    return BadRequest(errors);
}
```

## Combining with ValidationAttributes

ValidationSets and ValidationAttributes work together:

1. Use `AddFromType()` to run all attributes on the type.
2. Add custom steps before or after `AddFromType()`.
3. Use `When()` for conditional attribute-based validation (e.g., validate shipping address only when `RequiresShipping` is true).

```csharp
var set = ValidationSet
    .For<Order>()
    .AddIsNotNull()
    .When(o => o.Type == OrderType.Shipped)
    .AddIsNotNull(o => o.ShippingAddress, "Shipping address required for shipped orders")
    .AddFromType()
    .EndWhen()
    .Build();
```
