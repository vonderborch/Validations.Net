# Containment

Validators for checking whether collections or strings contain specified items or substrings.

## Validators

### DoesContain

**Description:** Validates that a collection contains a specified item, or that a string contains a specified substring.

**Target types:** `IEnumerable<T>?` (item), `string?` (substring)

**Methods:**
- `CheckDoesContain` → `bool`
- `ValidateDoesContain` → `ValidationResult`
- `EnsureDoesContain` → same type (throws on failure)

**Parameters (collection):**

| Parameter | Type | Description |
|-----------|------|-------------|
| collection | IEnumerable<T>? | The collection to validate |
| item | T | The item to find |

**Parameters (string):**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| substring | string | The substring to find |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateDoesContain(substring)]` (string only; no attribute for collection containment)

---

### DoesNotContain

**Description:** Validates that a collection does not contain a specified item, or that a string does not contain a specified substring.

**Target types:** `IEnumerable<T>?` (item), `string?` (substring)

**Methods:**
- `CheckDoesNotContain` → `bool`
- `ValidateDoesNotContain` → `ValidationResult`
- `EnsureDoesNotContain` → same type (throws on failure)

**Parameters:** Same as DoesContain.

**Matching Attribute:** `[ValidateDoesNotContain(substring)]` (string only; no attribute for collection containment)

---

### DoesContainAny

**Description:** Validates that a collection contains at least one of the specified candidates, or that a string contains at least one of the specified substrings.

**Target types:** `IEnumerable<T>?` (candidates), `string?` (substrings)

**Methods:**
- `CheckDoesContainAny` → `bool`
- `ValidateDoesContainAny` → `ValidationResult`
- `EnsureDoesContainAny` → same type (throws on failure)

**Parameters (collection):**

| Parameter | Type | Description |
|-----------|------|-------------|
| collection | IEnumerable<T>? | The collection to validate |
| candidates | IEnumerable<T> | The candidates to search for |

**Parameters (string):**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| substrings | IEnumerable<string> | The substrings to search for |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateDoesContainAny(...)]`

---

### DoesNotContainAny

**Description:** Validates that a collection does not contain any of the specified candidates, or that a string does not contain any of the specified substrings.

**Target types:** `IEnumerable<T>?` (candidates), `string?` (substrings)

**Methods:**
- `CheckDoesNotContainAny` → `bool`
- `ValidateDoesNotContainAny` → `ValidationResult`
- `EnsureDoesNotContainAny` → same type (throws on failure)

**Parameters:** Same as DoesContainAny.

**Matching Attribute:** `[ValidateDoesNotContainAny(...)]`

---

### DoesContainAll

**Description:** Validates that a collection contains all of the specified required items, or that a string contains all of the specified substrings.

**Target types:** `IEnumerable<T>?` (required), `string?` (substrings)

**Methods:**
- `CheckDoesContainAll` → `bool`
- `ValidateDoesContainAll` → `ValidationResult`
- `EnsureDoesContainAll` → same type (throws on failure)

**Parameters (collection):**

| Parameter | Type | Description |
|-----------|------|-------------|
| collection | IEnumerable<T>? | The collection to validate |
| required | IEnumerable<T> | The required items |

**Parameters (string):**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| substrings | IEnumerable<string> | The required substrings |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateDoesContainAll(...)]`

---

### DoesNotContainAll

**Description:** Validates that a collection does not contain all of the specified required items, or that a string does not contain all of the specified substrings.

**Target types:** `IEnumerable<T>?` (required), `string?` (substrings)

**Methods:**
- `CheckDoesNotContainAll` → `bool`
- `ValidateDoesNotContainAll` → `ValidationResult`
- `EnsureDoesNotContainAll` → same type (throws on failure)

**Parameters:** Same as DoesContainAll.

**Matching Attribute:** `[ValidateDoesNotContainAll(...)]`
