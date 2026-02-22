# Aggregate Validation Result

When running type-level validation with `ValidateIsValid` or executing a `ValidationSet`, the result is an `AggregateValidationResult` that collects all failures across the entire object graph.

## AggregateValidationResult

```csharp
public sealed class AggregateValidationResult
{
    public bool IsValid { get; }
    public IReadOnlyList<ValidationFailure> Failures { get; }

    public Dictionary<string, string[]> ToDictionary();
    public IReadOnlyList<string> GetAllErrorMessages();
}
```

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `IsValid` | `bool` | `true` if no failures were recorded |
| `Failures` | `IReadOnlyList<ValidationFailure>` | All validation failures with member paths |

### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `ToDictionary()` | `Dictionary<string, string[]>` | Groups error messages by member path (ASP.NET ModelState shape) |
| `GetAllErrorMessages()` | `IReadOnlyList<string>` | Flat list of all error messages |

### Example

```csharp
var result = user.ValidateIsValid();

if (!result.IsValid)
{
    // Iterate failures individually
    foreach (var failure in result.Failures)
    {
        Console.WriteLine($"{failure.MemberPath}: {failure.ErrorMessage}");
    }

    // Get all messages as a flat list
    IReadOnlyList<string> messages = result.GetAllErrorMessages();

    // Get as dictionary (for ModelState or API responses)
    Dictionary<string, string[]> errors = result.ToDictionary();
}
```

## ValidationFailure

Each failure is a `ValidationFailure` record struct that pairs a member path with the underlying `ValidationResult`.

```csharp
public readonly record struct ValidationFailure(string MemberPath, ValidationResult Result)
{
    public string? ErrorMessage => Result.ExceptionMessage;
}
```

| Property | Type | Description |
|----------|------|-------------|
| `MemberPath` | `string` | Dot-separated path to the failing member |
| `Result` | `ValidationResult` | The underlying validation result with exception details |
| `ErrorMessage` | `string?` | Convenience property for `Result.ExceptionMessage` |

## Member Path Format

Member paths use dot notation for nested objects and bracket notation for collection indices:

| Scenario | Example Path |
|----------|-------------|
| Direct property | `"Name"` |
| Nested object property | `"Address.City"` |
| Deeply nested | `"Address.Country.Code"` |
| Collection element property | `"Items[0].ProductId"` |
| Nested within collection | `"Items[2].Address.City"` |
| Class-level attribute | `""` (empty string) |

## ASP.NET Integration

`ToDictionary()` returns a `Dictionary<string, string[]>` that matches the shape used by ASP.NET ModelState, making it straightforward to return structured validation errors from API controllers:

```csharp
[HttpPost]
public IActionResult CreateUser([FromBody] CreateUserRequest request)
{
    var result = request.ValidateIsValid();
    if (!result.IsValid)
    {
        return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
    }

    // Proceed with valid request
    return Ok();
}
```

The dictionary maps member paths to arrays of error messages:

```json
{
    "Name": ["Name must not be null or whitespace"],
    "Age": ["Value must be in range [0, 150]"],
    "Address.City": ["City must not be null or whitespace"]
}
```

## ValidationSet Integration

`ValidationSet.Execute()` also returns an `AggregateValidationResult`:

```csharp
var set = ValidationSet
    .For<Order>()
    .AddIsNotNull()
    .AddIsNotNullOrWhiteSpace(o => o.CustomerId, "Customer ID is required")
    .AddFromType()
    .Build();

var result = set.Execute(order);
if (!result.IsValid)
{
    var errors = result.ToDictionary();
}
```

## Performance Notes

- **Lazy allocation**: The internal failure list is only created when the first failure is added. Successful validations allocate nothing beyond the shared `AggregateValidationResult.Success` singleton.
- **Zero-allocation success path**: `CheckIsValid()` returns `bool` directly and `AggregateValidationResult.Success` is a pre-allocated static instance.
