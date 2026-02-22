# Equality and Comparison

Validators for equality, ordering, ranges, membership, default values, and reference equality.

## Validators

### IsEquals

**Description:** Validates that a value equals an expected value.

**Type constraints:** `T : IEquatable<T>`

**Methods:**
- `CheckIsEquals` → `bool`
- `ValidateIsEquals` → `ValidationResult`
- `EnsureIsEquals` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| expected | T? | The expected value |

**Matching Attribute:** `[ValidateIsEquals(expected)]`

---

### IsNotEquals

**Description:** Validates that a value does not equal an expected value.

**Type constraints:** `T : IEquatable<T>`

**Methods:**
- `CheckIsNotEquals` → `bool`
- `ValidateIsNotEquals` → `ValidationResult`
- `EnsureIsNotEquals` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| expected | T? | The value that must not match |

**Matching Attribute:** `[ValidateIsNotEquals(expected)]`

---

### IsGreaterThan

**Description:** Validates that a value is greater than a comparand.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsGreaterThan` → `bool`
- `ValidateIsGreaterThan` → `ValidationResult`
- `EnsureIsGreaterThan` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| comparand | T | The value to compare against |

**Matching Attribute:** `[ValidateIsGreaterThan(comparand)]`

---

### IsLessThan

**Description:** Validates that a value is less than a comparand.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsLessThan` → `bool`
- `ValidateIsLessThan` → `ValidationResult`
- `EnsureIsLessThan` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| comparand | T | The value to compare against |

**Matching Attribute:** `[ValidateIsLessThan(comparand)]`

---

### IsGreaterThanOrEquals

**Description:** Validates that a value is greater than or equal to a comparand.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsGreaterThanOrEquals` → `bool`
- `ValidateIsGreaterThanOrEquals` → `ValidationResult`
- `EnsureIsGreaterThanOrEquals` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| comparand | T | The value to compare against |

**Matching Attribute:** `[ValidateIsGreaterThanOrEquals(comparand)]`

---

### IsLessThanOrEquals

**Description:** Validates that a value is less than or equal to a comparand.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsLessThanOrEquals` → `bool`
- `ValidateIsLessThanOrEquals` → `ValidationResult`
- `EnsureIsLessThanOrEquals` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| comparand | T | The value to compare against |

**Matching Attribute:** `[ValidateIsLessThanOrEquals(comparand)]`

---

### IsInRange

**Description:** Validates that a value is within a specified range.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsInRange` → `bool`
- `ValidateIsInRange` → `ValidationResult`
- `EnsureIsInRange` → `T` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T | The value to validate |
| min | T | Minimum bound |
| max | T | Maximum bound |
| minInclusive | bool | If true, min is inclusive (default: true) |
| maxInclusive | bool | If true, max is inclusive (default: true) |

**Matching Attribute:** `[ValidateIsInRange(min, max)]`

---

### IsNotInRange

**Description:** Validates that a value is not within a specified range.

**Type constraints:** `T : IComparable<T>`

**Methods:**
- `CheckIsNotInRange` → `bool`
- `ValidateIsNotInRange` → `ValidationResult`
- `EnsureIsNotInRange` → `T` (throws on failure)

**Parameters:** Same as IsInRange.

**Matching Attribute:** `[ValidateIsNotInRange(min, max)]`

---

### IsOneOf

**Description:** Validates that a value is one of a set of allowed values.

**Type constraints:** None

**Methods:**
- `CheckIsOneOf` → `bool` (overloads: `params T[]`, `IEnumerable<T>`)
- `ValidateIsOneOf` → `ValidationResult`
- `EnsureIsOneOf` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| allowedValues | params T[] or IEnumerable<T> | The allowed values |

**Matching Attribute:** `[ValidateIsOneOf(...)]`

---

### IsNotOneOf

**Description:** Validates that a value is not one of a set of disallowed values.

**Type constraints:** None

**Methods:**
- `CheckIsNotOneOf` → `bool` (overloads: `params T[]`, `IEnumerable<T>`)
- `ValidateIsNotOneOf` → `ValidationResult`
- `EnsureIsNotOneOf` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |
| disallowedValues | params T[] or IEnumerable<T> | The disallowed values |

**Matching Attribute:** `[ValidateIsNotOneOf(...)]`

---

### IsDefault

**Description:** Validates that a value equals the default value for its type.

**Type constraints:** None

**Methods:**
- `CheckIsDefault` → `bool`
- `ValidateIsDefault` → `ValidationResult`
- `EnsureIsDefault` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |

**Matching Attribute:** `[ValidateIsDefault]`

---

### IsNotDefault

**Description:** Validates that a value does not equal the default value for its type.

**Type constraints:** None

**Methods:**
- `CheckIsNotDefault` → `bool`
- `ValidateIsNotDefault` → `ValidationResult`
- `EnsureIsNotDefault` → `T?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | T? | The value to validate |

**Matching Attribute:** `[ValidateIsNotDefault]`

---

### IsSameAs

**Description:** Validates that a value is the same object reference as another (reference equality).

**Type constraints:** `object?` (reference types)

**Methods:**
- `CheckIsSameAs` → `bool`
- `ValidateIsSameAs` → `ValidationResult`
- `EnsureIsSameAs` → `object?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | object? | The value to validate |
| other | object? | The reference to compare against |

**Matching Attribute:** `[ValidateIsSameAs(other)]`

---

### IsNotSameAs

**Description:** Validates that a value is not the same object reference as another.

**Type constraints:** `object?` (reference types)

**Methods:**
- `CheckIsNotSameAs` → `bool`
- `ValidateIsNotSameAs` → `ValidationResult`
- `EnsureIsNotSameAs` → `object?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | object? | The value to validate |
| other | object? | The reference to compare against |

**Matching Attribute:** `[ValidateIsNotSameAs(other)]`
