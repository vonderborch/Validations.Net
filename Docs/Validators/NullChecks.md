# Null Checks

Validators for checking whether a value is null or not null.

## Validators

### IsNull

**Description:** Validates that a value is null.

**Type constraints:** None (generic `T?`)

**Methods:**
- `CheckIsNull` → `bool`
- `ValidateIsNull` → `ValidationResult`
- `EnsureIsNull` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |

**Example:**
```csharp
// Check
bool result = someValue.CheckIsNull();

// Validate
var validationResult = someValue.ValidateIsNull();

// Ensure (throws on failure)
var safe = someValue.EnsureIsNull();
```

**Matching Attribute:** `[ValidateIsNull]`

---

### IsNotNull

**Description:** Validates that a value is not null.

**Type constraints:** None (generic `T?`)

**Methods:**
- `CheckIsNotNull` → `bool`
- `ValidateIsNotNull` → `ValidationResult`
- `EnsureIsNotNull` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |

**Example:**
```csharp
// Check
bool result = someValue.CheckIsNotNull();

// Validate
var validationResult = someValue.ValidateIsNotNull();

// Ensure (throws on failure)
var safe = someValue.EnsureIsNotNull();
```

**Matching Attribute:** `[ValidateIsNotNull]`
