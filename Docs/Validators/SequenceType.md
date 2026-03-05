# Sequence and Type

Validators for sequence ordering (sorted, distinct) and type compatibility (assignable, instance of).

## Validators

### IsSorted

**Description:** Validates that a sequence is sorted in ascending or descending order.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsSorted` → `bool`
- `ValidateIsSorted` → `ValidationResult`
- `EnsureIsSorted` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| enumerable | IEnumerable<T>? | The sequence to validate |
| descending | bool | If true, checks descending order (default: false) |

**Matching Attribute:** `[ValidateIsSorted]`

---

### IsNotSorted

**Description:** Validates that a sequence is not sorted.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsNotSorted` → `bool`
- `ValidateIsNotSorted` → `ValidationResult`
- `EnsureIsNotSorted` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| enumerable | IEnumerable<T>? | The sequence to validate |
| descending | bool | If true, checks that it is not descending-sorted (default: false) |

**Matching Attribute:** `[ValidateIsNotSorted]`

---

### IsDistinct

**Description:** Validates that a sequence contains only distinct elements (no duplicates).

**Methods:**
- `CheckIsDistinct` → `bool`
- `ValidateIsDistinct` → `ValidationResult`
- `EnsureIsDistinct` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| enumerable | IEnumerable<T>? | The sequence to validate |

**Matching Attribute:** `[ValidateIsDistinct]`

---

### IsNotDistinct

**Description:** Validates that a sequence contains at least one duplicate.

**Methods:**
- `CheckIsNotDistinct` → `bool`
- `ValidateIsNotDistinct` → `ValidationResult`
- `EnsureIsNotDistinct` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| enumerable | IEnumerable<T>? | The sequence to validate |

**Matching Attribute:** `[ValidateIsNotDistinct]`

---

### IsAssignableTo

**Description:** Validates that a value is assignable to a target type (value's type is compatible with target).

**Methods:**
- `CheckIsAssignableTo` → `bool` (overloads: `CheckIsAssignableTo<TTarget>()`, `CheckIsAssignableTo(Type targetType)`)
- `ValidateIsAssignableTo` → `ValidationResult`
- `EnsureIsAssignableTo` → `object?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | object? | The value to validate |
| targetType / TTarget | Type or generic | The target type |

---

### IsInstanceOf

**Description:** Validates that a value is an exact instance of the target type (runtime type equals target, not a subclass).

**Methods:**
- `CheckIsInstanceOf` → `bool` (overloads: `CheckIsInstanceOf<TTarget>()`, `CheckIsInstanceOf(Type targetType)`)
- `ValidateIsInstanceOf` → `ValidationResult`
- `EnsureIsInstanceOf` → `object?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | object? | The value to validate |
| targetType / TTarget | Type or generic | The exact type required |
