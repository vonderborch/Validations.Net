# File System

Validators for file and directory existence at a given path.

## Validators

### DoesFileExist

**Description:** Validates that a path refers to an existing file.

**Methods:**
- `CheckDoesFileExist` → `bool`
- `ValidateDoesFileExist` → `ValidationResult`
- `EnsureDoesFileExist` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| path | string? | The file path to validate |

**Matching Attribute:** `[ValidateDoesFileExist]`

---

### DoesFileNotExist

**Description:** Validates that a path does not refer to an existing file.

**Methods:**
- `CheckDoesFileNotExist` → `bool`
- `ValidateDoesFileNotExist` → `ValidationResult`
- `EnsureDoesFileNotExist` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| path | string? | The file path to validate |

**Matching Attribute:** `[ValidateDoesFileNotExist]`

---

### DoesDirectoryExist

**Description:** Validates that a path refers to an existing directory.

**Methods:**
- `CheckDoesDirectoryExist` → `bool`
- `ValidateDoesDirectoryExist` → `ValidationResult`
- `EnsureDoesDirectoryExist` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| path | string? | The directory path to validate |

**Matching Attribute:** `[ValidateDoesDirectoryExist]`

---

### DoesDirectoryNotExist

**Description:** Validates that a path does not refer to an existing directory.

**Methods:**
- `CheckDoesDirectoryNotExist` → `bool`
- `ValidateDoesDirectoryNotExist` → `ValidationResult`
- `EnsureDoesDirectoryNotExist` → `string?` (throws on failure)

**Parameters:**

| Parameter | Type | Description |
|-----------|------|-------------|
| path | string? | The directory path to validate |

**Matching Attribute:** `[ValidateDoesDirectoryNotExist]`
