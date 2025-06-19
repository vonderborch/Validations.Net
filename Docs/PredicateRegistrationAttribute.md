# PredicateRegistrationAttribute

Attached to a method, field, or property, this attribute will allow the predicate to be used for predicate-based
validation attributes.

## Valid Targets

- `method`
- `field`
- `property`

## Parameters

| Parameter Name | Type        | IsRequired | Description                                      |
|----------------|-------------|------------|--------------------------------------------------|
| predicateName  | string      | Yes        | The name of the predicate                        |
| predicateGroup | string?     | No         | The optional name of the group for the predicate |

## Notes

- Public or private methods, fields, or properties can be registered. Private items must be registered on the
    class/struct they are being used to validate.
- Static or non-static methods, fields, or properties can be registered. Non-static items must be registered on the
    class/struct they are being used to validate.
