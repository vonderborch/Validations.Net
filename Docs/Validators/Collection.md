# Collection

Validators for emptiness, null-or-empty, length/count, single element, and subset/superset relationships. Works with strings, `ICollection<T>`, `IEnumerable<T>`, and `ReadOnlySpan<T>` where applicable.

## Validators

### IsEmpty

**Description:** Validates that a string or collection is empty (non-null with zero elements).

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`, `ReadOnlySpan<T>`

**Methods:**
- `CheckIsEmpty` → `bool`
- `ValidateIsEmpty` → `ValidationResult`
- `EnsureIsEmpty` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? / ReadOnlySpan<T> | The value to validate |

**Matching Attribute:** `[ValidateIsEmpty]`

---

### IsNotEmpty

**Description:** Validates that a string or collection is not empty (non-null with at least one element).

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`, `ReadOnlySpan<T>`

**Methods:**
- `CheckIsNotEmpty` → `bool`
- `ValidateIsNotEmpty` → `ValidationResult`
- `EnsureIsNotEmpty` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? / ReadOnlySpan<T> | The value to validate |

**Matching Attribute:** `[ValidateIsNotEmpty]`

---

### IsNullOrEmpty

**Description:** Validates that a string or collection is null or empty.

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsNullOrEmpty` → `bool`
- `ValidateIsNullOrEmpty` → `ValidationResult`
- `EnsureIsNullOrEmpty` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? | The value to validate |

**Matching Attribute:** `[ValidateIsNullOrEmpty]`

---

### IsNotNullOrEmpty

**Description:** Validates that a string or collection is not null and not empty.

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsNotNullOrEmpty` → `bool`
- `ValidateIsNotNullOrEmpty` → `ValidationResult`
- `EnsureIsNotNullOrEmpty` → non-nullable return (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? | The value to validate |

**Matching Attribute:** `[ValidateIsNotNullOrEmpty]`

---

### IsLength

**Description:** Validates that a string or collection has a specific length. Supports exact length, `LengthCheckMode` (ExactLength, LessThan, LessThanOrEqual, GreaterThan, GreaterThanOrEqual), or min–max range.

**Target types:** `string?`, `ICollection<T>?`, `ReadOnlySpan<T>`

**Methods:**
- `CheckIsLength` → `bool` (overloads: `int length`, `int length, LengthCheckMode mode`, `int minLength, int maxLength`)
- `ValidateIsLength` → `ValidationResult`
- `EnsureIsLength` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / ReadOnlySpan<T> | The value to validate |
| length | int | Expected length (or reference for mode) |
| mode | LengthCheckMode | ExactLength, LessThan, LessThanOrEqual, GreaterThan, GreaterThanOrEqual |
| minLength, maxLength | int | Range bounds |

**Matching Attribute:** `[ValidateIsLength(length)]`

---

### IsNotLength

**Description:** Validates that a string or collection does not have a specific length. Inverse of IsLength.

**Target types:** `string?`, `ICollection<T>?`, `ReadOnlySpan<T>`

**Methods:**
- `CheckIsNotLength` → `bool` (overloads: `int length`, `int length, LengthCheckMode mode`, `int minLength, int maxLength`)
- `ValidateIsNotLength` → `ValidationResult`
- `EnsureIsNotLength` → same type (throws on failure)

**Parameters:** Same as IsLength.

**Matching Attribute:** `[ValidateIsNotLength(length)]`

---

### IsCount

**Description:** Validates that a collection or enumerable has an exact count.

**Target types:** `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsCount` → `bool`
- `ValidateIsCount` → `ValidationResult`
- `EnsureIsCount` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| collection / enumerable | ICollection<T>? / IEnumerable<T>? | The value to validate |
| count | int | The expected count |

**Matching Attribute:** `[ValidateIsCount(count)]`

---

### IsNotCount

**Description:** Validates that a collection or enumerable does not have a specific count.

**Target types:** `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsNotCount` → `bool`
- `ValidateIsNotCount` → `ValidationResult`
- `EnsureIsNotCount` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| collection / enumerable | ICollection<T>? / IEnumerable<T>? | The value to validate |
| count | int | The count that must not match |

**Matching Attribute:** `[ValidateIsNotCount(count)]`

---

### IsSingle

**Description:** Validates that a string or collection contains exactly one element.

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsSingle` → `bool`
- `ValidateIsSingle` → `ValidationResult`
- `EnsureIsSingle` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? | The value to validate |

**Matching Attribute:** `[ValidateIsSingle]`

---

### IsNotSingle

**Description:** Validates that a string or collection does not contain exactly one element.

**Target types:** `string?`, `ICollection<T>?`, `IEnumerable<T>?`

**Methods:**
- `CheckIsNotSingle` → `bool`
- `ValidateIsNotSingle` → `ValidationResult`
- `EnsureIsNotSingle` → same type (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? / ICollection<T>? / IEnumerable<T>? | The value to validate |

**Matching Attribute:** `[ValidateIsNotSingle]`

---

### IsSubsetOf

**Description:** Validates that all elements of the source collection exist in the superset.

**Target types:** `IEnumerable<T>?`

**Methods:**
- `CheckIsSubsetOf` → `bool`
- `ValidateIsSubsetOf` → `ValidationResult`
- `EnsureIsSubsetOf` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| source | IEnumerable<T>? | The collection to validate |
| superset | IEnumerable<T> | The superset (null source passes) |

---

### IsNotSubsetOf

**Description:** Validates that not all elements of the source exist in the superset.

**Target types:** `IEnumerable<T>?`

**Methods:**
- `CheckIsNotSubsetOf` → `bool`
- `ValidateIsNotSubsetOf` → `ValidationResult`
- `EnsureIsNotSubsetOf` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| source | IEnumerable<T>? | The collection to validate |
| superset | IEnumerable<T> | The superset |

---

### IsSupersetOf

**Description:** Validates that all elements of the subset exist in the source collection.

**Target types:** `IEnumerable<T>?`

**Methods:**
- `CheckIsSupersetOf` → `bool`
- `ValidateIsSupersetOf` → `ValidationResult`
- `EnsureIsSupersetOf` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| source | IEnumerable<T>? | The collection to validate |
| subset | IEnumerable<T> | The subset |

---

### IsNotSupersetOf

**Description:** Validates that not all elements of the subset exist in the source collection.

**Target types:** `IEnumerable<T>?`

**Methods:**
- `CheckIsNotSupersetOf` → `bool`
- `ValidateIsNotSupersetOf` → `ValidationResult`
- `EnsureIsNotSupersetOf` → `IEnumerable<T>?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| source | IEnumerable<T>? | The collection to validate |
| subset | IEnumerable<T> | The subset |
