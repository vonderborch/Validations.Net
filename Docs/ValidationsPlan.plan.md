---
name: Validations Plan
overview: "Expand Validations.Net with general and specialized validators (generic over INumber/IFloatingPoint where applicable), ValidationAttributes and type-level IsValid/IsNotValid (all failures), collection/span/stream support, and fluent ValidationSets. Single source of truth for scope and design."
todos:
  - id: type-discovery-runner
    content: "Type discovery + runner + ValidationAttribute.Validate contract"
    status: pending
  - id: attributes-isvalid
    content: "Concrete ValidationAttributes (IsNull, IsNotNull, AgainstPredicate) + IsValid/IsNotValid"
    status: pending
  - id: validation-set
    content: "ValidationSet and fluent builder (all failures aggregate)"
    status: pending
  - id: general-validators
    content: "Additional general validators in batches (generic numeric per §1.0.1, containment, etc.)"
    status: pending
  - id: specialized-validators
    content: "Specialized subdirectories (Age, DataFormat, DateTime, FileSystem, Streams, etc.)"
    status: pending
  - id: validation-set-from-type
    content: "Optional: build ValidationSet from TypeValidationInfo"
    status: pending
isProject: true
---

# Validations.Net: Validators, ValidationAttributes, and ValidationSets

Single plan for expanding Validations.Net with general and specialized validators, ValidationAttributes (with IsValid/IsNotValid for type-level validation), and fluent ValidationSets. Type-level and composite validation return **all failures** (no first-failure-only).

---

## Current state

- **Validators:** Three exist in [source/Validations.Net/Validators/](source/Validations.Net/Validators/): `IsNull`, `IsNotNull`, `AgainstPredicate`. Pattern: static class, extension methods `Check*`, `Validate*`, `Ensure*`; constants `ValidatorName`, `DefaultValidationFailureMessage`; use `ValidationResult` / `ValidationException` and optional `IBlackboard` / `[CallerArgumentExpression] parameterName`.
- **ValidationAttribute:** Abstract base in [source/Validations.Net/ValidationAttribute.cs](source/Validations.Net/ValidationAttribute.cs) with constructor `(string name)`. [PropertyValidationInfo](source/Validations.Net/Predicates/PropertyValidationInfo.cs) and [FieldValidationInfo](source/Validations.Net/Predicates/FieldValidationInfo.cs) reference `List<ValidationAttribute>`, but no code discovers or runs these attributes today.
- **Docs:** [Docs/Validators/IsValid.md](Docs/Validators/IsValid.md) and [IsNotValid.md](Docs/Validators/IsNotValid.md) describe type-level validation via attributes; IsValid/IsNotValid are not implemented in source. [AgainstPredicate.md](Docs/Validators/AgainstPredicate.md) documents `ValidateAgainstPredicateAttribute`.
- **ValidationAttributes folder:** Present in csproj as empty placeholder; no concrete attributes exist.

---

## 1. Validator expansion

**Pattern to follow:** Same as `IsNull`/`IsNotNull`: static class, `Check*`, `Validate*`, `Ensure*`, `ValidatorName`, `DefaultValidationFailureMessage`, `ValidationResult.CreateFromValidationFailure(ValidatorName, ...)` / `CreateFromValidationSuccess()`, `[MethodImpl(AggressiveInlining)]`, optional blackboard and `parameterName`.

**Layout:**

- **General validators** in `source/Validations.Net/Validators/` (flat). Includes **IsValid** and **IsNotValid**, which run all ValidationAttributes on a class/object (see section 2.4).
- **Specialized validators** in subdirectories under `Validators/` (Age, DataFormat, DateTime, FileSystem, Streams/IO, Identifiers, Network, Finance, Version, Geography, Security; optionally Strings for business-style only). Numeric and general string validators live in the general list.

### 1.0 Supported collection/sequence types and performance

Validators that operate on collections, sequences, or "containers" (empty, length, containment) must support multiple type shapes without boxing and with minimal code duplication.

**Types supported by general validators (collection/containment/length/empty):**

- **Interface-based:** `IEnumerable<T>`, `ICollection<T>`, `IReadOnlyCollection<T>`, `IList<T>`, `IReadOnlyList<T>` (covers `List<T>`, arrays `T[]`, etc.).
- **String:** `string` (as character sequence or substring container).
- **Ref-struct spans (no boxing):** `Span<T>`, `ReadOnlySpan<T>` — provide **explicit overloads** so call sites can pass spans without conversion to IEnumerable; keeps high performance and avoids allocations.
- **Memory (non-ref-struct):** `Memory<T>`, `ReadOnlyMemory<T>` — explicit overloads where useful (e.g. async or stored buffers); share logic with span overloads where possible.
- **Arrays:** `T[]` is covered via `IList<T>`/`ICollection<T>`; explicit array overloads only if needed for perf (e.g. to avoid interface calls).

**Streams and I/O:** `Stream`, `PipeReader`, and similar are **not** treated as generic collections in general validators (they are sequential read abstractions). Handle them in **specialized** validators (e.g. `Validators/Streams/` or `Validators/IO/`) so general validators stay generic and allocation-friendly. Specialized validators can validate stream properties (e.g. CanRead, Length), content read into buffers (reusing general validators on the buffer), or stream-specific rules.

**Genericity and performance (design rules):**

- **Prefer generic implementations:** Where a validator applies to many types (numeric, comparable, enumerable, etc.), implement it once using generic type parameters and interface constraints (e.g. `INumber<T>`, `IComparable<T>`, `IEnumerable<T>`) instead of separate overloads per concrete type. Use overloads only when necessary: for ref structs (Span, ReadOnlySpan), for performance-sensitive paths, or when a type does not implement a shared interface. This reduces duplication and keeps behavior consistent across types.
- **Shared core logic:** Implement the validation algorithm once (e.g. "contains item", "contains any", "length") in internal helpers or private methods; public API overloads (Span, IEnumerable, string, etc.) delegate to that logic to reduce duplication.
- **Overloads, not boxing:** Use separate overloads for `Span<T>`, `ReadOnlySpan<T>`, `IEnumerable<T>`, `string`, etc., so value-type and ref-struct arguments are not boxed.
- **Ref-struct overloads:** Provide extension methods or static methods that accept `Span<T>`/`ReadOnlySpan<T>` where the validator is meaningful (containment, length, empty) so stack-only types remain stack-only.
- **Specialized for special cases:** Streams, pipes, and other I/O or domain-specific sequence types get their own validators in specialized subdirectories; they can call general validators on buffers or in-memory views where appropriate.

### 1.0.1 Numeric and floating-point types (generic)

Numeric and floating-point validators should be implemented as generically as possible using **System.Numeric** interfaces (.NET 7+), so one implementation covers many types without per-type overloads or boxing.

- **Numeric validators** (IsDivisibleBy, IsEven, IsOdd, IsPositive, IsNegative, IsZero, IsNonZero; and comparison/range where the value is numeric) should be **generic methods** constrained by:
  - **INumber&lt;T&gt;** for general number operations (comparison with zero, sign, etc.) — applies to int, long, decimal, float, double, etc.
  - **IBinaryInteger&lt;T&gt;** where integer-only behavior is needed (e.g. IsEven, IsOdd, IsDivisibleBy) — applies to int, long, byte, etc., not float/double.
  - Use **one generic implementation** per validator (e.g. `CheckIsPositive&lt;T&gt;(this T value) where T : INumber&lt;T&gt;`) instead of overloads for each concrete type; avoid boxing by keeping parameters and returns as `T`.
- **Floating-point-specific validators** (IsFinite, IsNaN, and optionally IsInfinity) should be generic over **IFloatingPoint&lt;T&gt;** so one implementation works for `float` and `double` (and any future floating type that implements the interface).
- **IsCreditCard** and similar format-oriented numeric checks may work over `INumber<T>` or a string view depending on design; prefer a generic numeric signature where the check is purely numeric.
- **Range and comparison** validators (IsInRange, IsGreaterThan, etc.) already use `IComparable<T>`; for numeric types this aligns with INumber. Ensure numeric validators themselves are generic as above.
- **Note:** System.Numeric (INumber, IBinaryInteger, IFloatingPoint) is available from .NET 7 onward; the project target (e.g. net9.0) already supports it.

### 1.1 General validators (with types tested)

All in `source/Validations.Net/Validators/` (flat). Implement in phases as needed.

| Validator(s) | Types tested | Notes |
|--------------|--------------|--------|
| IsNull, IsNotNull | `T?` (any reference or nullable value type) | Already present |
| AgainstPredicate | `T?` (any) | Predicate `Func<T?, bool>` or named predicate |
| IsValid, IsNotValid | `class`, `struct` (any type with ValidationAttributes) | Runs all ValidationAttributes on instance |
| IsNullOrEmpty, IsNotNullOrEmpty | `string`, `IEnumerable<T>`, `Span<T>`, `ReadOnlySpan<T>`, `Memory<T>`, `ReadOnlyMemory<T>` (see §1.0) | Null or empty; overloads to avoid boxing |
| IsEmpty, IsNotEmpty | Same as above (see §1.0) | Empty vs non-empty |
| IsEquals, IsNotEquals | `T` (IEquatable or EqualityComparer) | Value equality |
| IsGreaterThan, IsLessThan, IsGreaterThanOrEquals, IsLessThanOrEquals | `T` (IComparable) | Comparison |
| IsInRange, IsNotInRange | `T` (IComparable) | Min/max bounds; **inclusive/exclusive flags** for min and max (e.g. `minInclusive`, `maxInclusive` or options struct so caller can choose `[min,max]`, `(min,max)`, `[min,max)`, `(min,max]`) |
| IsLength, IsNotLength | `string`, `ICollection`/`IEnumerable`, `Span<T>`, `ReadOnlySpan<T>`, arrays (see §1.0) | Exact or min/max length; overloads for spans/arrays |
| IsOneOf, IsNotOneOf | `T` (any; set of allowed values) | Membership in set |
| IsNullOrWhiteSpace, IsNotNullOrWhiteSpace, IsWhiteSpace, IsNotWhiteSpace | `string` | Whitespace semantics |
| IsAssignableTo, IsInstanceOf | `object` (type checks) | Type compatibility / runtime type |
| IsDefault, IsNotDefault | `T` (struct or nullable) | Value equals default(T); useful for structs and flags |
| IsSameAs, IsNotSameAs | `object` / `T` | Reference equality (ReferenceEquals), not value equality |
| IsDivisibleBy, IsEven, IsOdd, IsPositive, IsNegative, IsZero, IsNonZero, IsCreditCard | `T where T : INumber<T>` (and `IBinaryInteger<T>` for integer-only: IsEven, IsOdd, IsDivisibleBy); single generic implementation for int, long, decimal, float, double, etc. No boxing. See §1.0.1. | General numeric; generic over System.Numeric |
| IsFinite, IsNaN (optionally IsInfinity) | `T where T : IFloatingPoint<T>` (float, double) | Non-finite checks; one generic implementation. See §1.0.1. |
| IsMatch (regex), IsNotMatch, IsValidUri, MaxLength, MinLength | `string` | General string (not business-style; IsValidEmail is specialized) |
| IsSorted, IsNotSorted | `IEnumerable<T>` where T : IComparable (see §1.0) | Sequence in ascending/descending order; order flag |
| IsDistinct, IsNotDistinct | `IEnumerable<T>` (see §1.0) | All elements unique (no duplicates) |
| IsSingle, IsNotSingle | `IEnumerable<T>`, `Span<T>`, etc. (see §1.0) | Exactly one element (count == 1) |
| IsCount, IsNotCount | `IEnumerable<T>`, `Span<T>`, etc. (see §1.0) | Count/length equals (or not) a given value |
| IsElementOf, IsNotElementOf | `T` + `IEnumerable<T>` (item in collection); `string` + `string` (substring) | Item in container |
| DoesContain, DoesNotContain | `IEnumerable<T>` + `T` (element); `string` + `string` (substring) | Container contains item |
| DoesContainAny, DoesNotContainAny | Container and candidate set as `IEnumerable<T>` or `string` | Any of set in container |
| DoesContainAll, DoesNotContainAll | Container and required set as `IEnumerable<T>` or `string` | All of set in container |

**Range validators (IsInRange / IsNotInRange):** Support configurable bounds. API must allow the caller to specify for each bound whether it is **inclusive** or **exclusive** (e.g. `minInclusive: true`, `maxInclusive: false` for `[min, max)`). Default when omitted: both inclusive `[min, max]`. Overloads or an options type can avoid long parameter lists.

### 1.2 Specialized validators (with types tested)

Each in its own subdirectory under `Validators/`. Business-style or domain-specific only. Each gets a matching ValidationAttribute when used on types/fields/properties.

| Subdirectory | Validator(s) | Types tested | Notes |
|--------------|--------------|--------------|--------|
| Validators/Age/ | IsValidAge, IsAdult, IsMinor, IsWithinAgeRange | `DateTime`, `DateTimeOffset`, or `int` (age in years) | Age from date or numeric age |
| Validators/DataFormat/ | IsValidJson, IsNotValidJson | `string` | JSON format |
| Validators/DataFormat/ | IsValidXml, IsNotValidXml | `string` | XML format |
| Validators/DataFormat/ | IsValidBase64, IsNotValidBase64 | `string` | Base64 encoding |
| Validators/DataFormat/ | IsValidEmail | `string` | Business-style; could live under Strings/ instead |
| Validators/DateTime/ | IsInFuture, IsInPast | `DateTime`, `DateTimeOffset` | Relative to now or given reference |
| Validators/DateTime/ | IsBusinessDay, IsWeekend | `DateTime`, `DateTimeOffset` | Day-of-week |
| Validators/DateTime/ | IsWithinDateRange | `DateTime`, `DateTimeOffset` | Range of dates |
| Validators/DateTime/ | IsUtc, IsLocal, IsDateOnly, IsTimeOnly | `DateTime`, `DateTimeOffset` | Kind and date vs time |
| Validators/DateTime/ | IsLeapYear | `DateTime` or `int` (year) | Leap year |
| Validators/FileSystem/ | DoesFileExist, DoesFileNotExist | `string` (path) | File existence |
| Validators/FileSystem/ | DoesDirectoryExist, DoesDirectoryNotExist | `string` (path) | Directory existence |
| Validators/Streams/ (or IO/) | Stream/IO validators as needed | `Stream`, `PipeReader`, etc. | CanRead, Length in range, content via buffers; reuse general validators on buffers |
| Validators/Identifiers/ | IsValidGuid, IsNotValidGuid | `string` | GUID format |
| Validators/Identifiers/ | IsValidId (configurable format) | `string` | Generic id format (e.g. alphanumeric, length) |
| Validators/Network/ | IsValidIpAddress (v4/v6), IsValidHostname | `string` | IP and hostname format |
| Validators/Finance/ | IsValidIban, IsValidBicSwift | `string` | Banking identifiers |
| Validators/Version/ | IsValidSemanticVersion, IsCompatibleVersion | `string` or `Version` | SemVer parseable; version compatibility |
| Validators/Geography/ | IsValidLatitude, IsValidLongitude | `double` | Lat/long in valid ranges |
| Validators/Security/ | IsSecurePassword (complexity rules) | `string` | Password strength (length, character sets) |

Add or remove from these lists as you scope v1.

**Deliverables per validator:** one static class file, matching unit tests in `test/automated/Validations.Net.Test/Validators/` (and subdirs for specialized), and optional doc under `Docs/Validators/`.

---

## 2. ValidationAttributes and type-level validation

**Goal:** Attributes that can be applied to **class**, **field**, and **property**, with a single execution path. The **IsValid** and **IsNotValid** validators are the dedicated entry points that check all ValidationAttributes on a class/object (see 2.4).

### 2.1 Base and contract

- Keep [ValidationAttribute](source/Validations.Net/ValidationAttribute.cs) as the base. Extend it so attribute instances can participate in validation:
  - Add an abstract (or virtual) method that returns `ValidationResult` given the member value and context, e.g. `ValidationResult Validate(object? value, string? memberName, IBlackboard? blackboard)`.
  - Store `name` (and any validator-specific args) in the attribute for error messages and for consistency with existing `ValidationException`/`ValidationResult` usage.
- Ensure concrete attributes can target `AttributeTargets.Class | Field | Property` where appropriate.

### 2.2 Concrete attributes

- Add concrete attribute types under `source/Validations.Net/ValidationAttributes/` (replace the empty folder with real types), one (or two) per validator pair, e.g.:
  - `ValidateIsNullAttribute`, `ValidateIsNotNullAttribute`
  - `ValidateAgainstPredicateAttribute` (already documented; predicate name/group)
  - Then, as validators are added: `ValidateIsEqualsAttribute`, `ValidateIsInRangeAttribute`, `ValidateIsLengthAttribute`, etc.
- Each attribute’s `Validate(...)` implementation should call the corresponding static validator so logic lives in one place. Handle type mismatches by returning a failure `ValidationResult` with a clear message rather than throwing.

### 2.3 Type discovery

- Introduce a **type validation info** component that, for a given `Type`, discovers:
  - All **properties** and **fields** (e.g. public and non-public, instance) that have at least one `ValidationAttribute`.
  - Optionally, **class-level** attributes that apply to the instance as a whole.
- Output can be a small struct/class (e.g. `TypeValidationInfo`) holding a list of member descriptors, each with `MemberInfo`, member name, and `List<ValidationAttribute>`. Consider caching per-type (e.g. `ConcurrentDictionary<Type, TypeValidationInfo>`).

### 2.4 IsValid and IsNotValid validators (attribute checking on class/object)

- **IsValid** and **IsNotValid** are validators that check all ValidationAttributes on a class or object instance.
- **Runner (internal):** Given an object instance and `TypeValidationInfo` (or type), iterate members (and optionally class-level attributes), get member value, run each attribute’s `Validate(value, memberName, blackboard)` in turn. **Collect and return all failures** (do not stop at first failure). The result type for type-level validation must expose all failures—e.g. an aggregate result type (e.g. `AggregateValidationResult`) or a result that carries `IReadOnlyList<ValidationResult>` for the failures.
- **IsValid:** Extension methods on class/struct instance: `CheckIsValid`, `ValidateIsValid`, `EnsureIsValid` (and optional `GetValidationResultForIsValid`). Parameters: optional `variableName`, `blackboard`. **Succeeds** when all ValidationAttributes on the type (members and optionally class) pass.
- **IsNotValid:** `CheckIsNotValid`, `ValidateIsNotValid`, `EnsureIsNotValid`. **Succeeds** when at least one ValidationAttribute fails (logical inverse of IsValid).
- Both use the same type discovery and runner; they differ only in how they interpret the combined results.

### 2.5 Class-level attributes

- If “validate class” means “run class-level attributes with the instance as value,” the same runner can support a list of class-level attributes and run them with `value = instance`. Clarify in implementation whether class-level and member-level are both supported and document.

---

## 3. ValidationSets (fluent composition and generic execution)

**Goal:** Define reusable validation “sets” in fluent style using the validators (and optionally attribute-derived rules), and execute them generically (single value in, aggregated result out).

### 3.1 Core abstraction

- **ValidationSet&lt;T&gt;** (or similar name): represents a list of validation steps that can be run against a value of type `T`.
- Each step is something that, given `T value` and optional context, produces a `ValidationResult`. So the step can be represented as `Func<T, IBlackboard?, ValidationResult>` or a small interface, e.g. `IValidationStep<T>` with a single method returning `ValidationResult`.
- **Execution:** `ValidationSet<T>.Execute(T value, IBlackboard? blackboard = null)` runs all steps in sequence and returns an **aggregated result with all failures** (same as type-level validation; do not stop at first failure). Use the same aggregate result shape (e.g. `AggregateValidationResult` or list of failures).

### 3.2 Fluent API

- **Builder:** `ValidationSet.For<T>()` or `new ValidationSetBuilder<T>()` that exposes:
  - `.Add(step)` where `step` is a `Func<T, IBlackboard?, ValidationResult>` or a method that adds a predefined step (e.g. `.AddIsNotNull()`, `.AddIsInRange(min, max)`).
  - Optional: `.Add(attribute)` to add the same logic as an attribute.
- **Building:** `.Build()` or similar returns an immutable `ValidationSet<T>` (or the builder is the set) that can be stored and reused.
- **Execution:** `.Execute(value, blackboard)` and, if desired, `.Check(value)`, `.Ensure(value)` that delegate to the same steps and either return bool or throw.

### 3.3 Integration

- **With validators:** Each “add” method (e.g. `AddIsNotNull()`) wraps the corresponding extension validator so the set is just a composition of existing validators.
- **With attributes (optional):** Build a `ValidationSet<T>` from a type’s `TypeValidationInfo`, or allow adding a step that runs all attribute validations for type T.
- **Generic execution:** Call sites can hold `ValidationSet<T>` and execute via `set.Execute(value)` with a known `T`.

### 3.4 Location and surface

- New types in something like `Validations.Net/ValidationSets/` (or `Validations.Net/Fluent/`): e.g. `ValidationSetBuilder<T>`, `ValidationSet<T>`, and optionally `IValidationStep<T>`. Keep the API in the same namespace or a sub-namespace (e.g. `Validations.Net.ValidationSets`).

---

## 4. Implementation order (suggested)

1. **Type discovery + runner + ValidationAttribute.Validate contract** – Enables attribute-based validation and IsValid/IsNotValid.
2. **Concrete ValidationAttributes** for existing validators (IsNull, IsNotNull, AgainstPredicate) and **IsValid/IsNotValid** implementation – Closes the loop for type-level validation as in the docs.
3. **ValidationSet and fluent builder** – Composable, generic execution without depending on reflection at execution time.
4. **Additional general validators** (e.g. IsNullOrEmpty/IsNotNullOrEmpty, IsEquals/IsNotEquals, IsInRange/IsNotInRange, IsLength/IsNotLength, containment, numeric/floating generic per §1.0.1, string, IsDefault/IsSameAs, IsSorted/IsDistinct/IsSingle/IsCount) – Add in small batches with tests and matching attributes.
5. **Specialized subdirectories** (Age, DataFormat, DateTime, FileSystem, Streams, Identifiers, Network, Finance, Version, Geography, Security) – Add domain validators and corresponding attributes as needed.
6. **Optional: build ValidationSet from type** – Populate a set from `TypeValidationInfo` so fluent and attribute-based flows share the same execution path.

---

## 5. Design choices (decided or to confirm)

- **ValidationResult aggregation:** **Decided.** Type-level validation and composite (ValidationSet) validation both **return all failures**; no "first failure only" mode.
- **Class-level attributes:** Should class-level attributes (applied to the type) run with the instance as the value, and be part of the same discovery/runner?
- **Validator scope for v1:** Which general pairs do you want in the first phase, and which specialized subdirectories (if any) in scope from the start?

Once these are decided, the plan can be turned into concrete tasks (files to add, method signatures, and tests).
