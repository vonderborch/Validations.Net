# String

Validators for string content, whitespace, pattern matching, URI validation, prefix/suffix, and length.

## Validators

### IsNullOrWhiteSpace

**Description:** Validates that a string is null or whitespace.

**Methods:**
- `CheckIsNullOrWhiteSpace` → `bool`
- `ValidateIsNullOrWhiteSpace` → `ValidationResult`
- `EnsureIsNullOrWhiteSpace` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNullOrWhiteSpace]`

---

### IsNotNullOrWhiteSpace

**Description:** Validates that a string is not null or whitespace.

**Methods:**
- `CheckIsNotNullOrWhiteSpace` → `bool`
- `ValidateIsNotNullOrWhiteSpace` → `ValidationResult`
- `EnsureIsNotNullOrWhiteSpace` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotNullOrWhiteSpace]`

---

### IsWhiteSpace

**Description:** Validates that a string is whitespace (non-null, non-empty, contains only whitespace).

**Methods:**
- `CheckIsWhiteSpace` → `bool`
- `ValidateIsWhiteSpace` → `ValidationResult`
- `EnsureIsWhiteSpace` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsWhiteSpace]`

---

### IsNotWhiteSpace

**Description:** Validates that a string is not whitespace (null, empty, or contains at least one non-whitespace character).

**Methods:**
- `CheckIsNotWhiteSpace` → `bool`
- `ValidateIsNotWhiteSpace` → `ValidationResult`
- `EnsureIsNotWhiteSpace` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |

**Matching Attribute:** `[ValidateIsNotWhiteSpace]`

---

### IsMatch

**Description:** Validates that a string matches a regular expression pattern.

**Methods:**
- `CheckIsMatch` → `bool` (overloads: `Regex`, `string pattern`)
- `ValidateIsMatch` → `ValidationResult`
- `EnsureIsMatch` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| regex / pattern | Regex or string | The pattern to match against |

**Matching Attribute:** `[ValidateIsMatch(pattern)]`

---

### IsNotMatch

**Description:** Validates that a string does not match a regular expression pattern.

**Methods:**
- `CheckIsNotMatch` → `bool` (overloads: `Regex`, `string pattern`)
- `ValidateIsNotMatch` → `ValidationResult`
- `EnsureIsNotMatch` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| regex / pattern | Regex or string | The pattern to match against |

**Matching Attribute:** `[ValidateIsNotMatch(pattern)]`

---

### IsValidUri

**Description:** Validates that a string represents a valid URI.

**Methods:**
- `CheckIsValidUri` → `bool`
- `ValidateIsValidUri` → `ValidationResult`
- `EnsureIsValidUri` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| uriKind | UriKind | Absolute, Relative, or RelativeOrAbsolute (default: Absolute) |

**Matching Attribute:** `[ValidateIsValidUri]`

---

### StartsWith

**Description:** Validates that a string starts with a specified prefix.

**Methods:**
- `CheckStartsWith` → `bool`
- `ValidateStartsWith` → `ValidationResult`
- `EnsureStartsWith` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| prefix | string | The prefix to check for |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateStartsWith(prefix)]`

---

### DoesNotStartWith

**Description:** Validates that a string does not start with a specified prefix.

**Methods:**
- `CheckDoesNotStartWith` → `bool`
- `ValidateDoesNotStartWith` → `ValidationResult`
- `EnsureDoesNotStartWith` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| prefix | string | The prefix to check for |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateDoesNotStartWith(prefix)]`

---

### EndsWith

**Description:** Validates that a string ends with a specified suffix.

**Methods:**
- `CheckEndsWith` → `bool`
- `ValidateEndsWith` → `ValidationResult`
- `EnsureEndsWith` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| suffix | string | The suffix to check for |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateEndsWith(suffix)]`

---

### DoesNotEndWith

**Description:** Validates that a string does not end with a specified suffix.

**Methods:**
- `CheckDoesNotEndWith` → `bool`
- `ValidateDoesNotEndWith` → `ValidationResult`
- `EnsureDoesNotEndWith` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| suffix | string | The suffix to check for |
| comparison | StringComparison | Ordinal by default |

**Matching Attribute:** `[ValidateDoesNotEndWith(suffix)]`

---

### MinLength

**Description:** Validates that a string has at least a specified minimum length.

**Methods:**
- `CheckMinLength` → `bool`
- `ValidateMinLength` → `ValidationResult`
- `EnsureMinLength` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| minLength | int | The minimum length required |

**Matching Attribute:** `[ValidateMinLength(minLength)]`

---

### MaxLength

**Description:** Validates that a string does not exceed a specified maximum length.

**Methods:**
- `CheckMaxLength` → `bool`
- `ValidateMaxLength` → `ValidationResult`
- `EnsureMaxLength` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| value | string? | The string to validate |
| maxLength | int | The maximum length allowed |

**Matching Attribute:** `[ValidateMaxLength(maxLength)]`
