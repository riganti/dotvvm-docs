## Sample 13: Filtering - Operators

You can specify what filtering operators should be supported for specific types.

These operators can be limited for the whole `GridView` or for a specific column. Both `GridView` and all column types have properties `BooleanOperators`, `NumberOperators`, `StringOperators`, `EnumOperators` and `CollectionOperators`, each of these will change filtering operators based on the type of the property.

Business Pack comes with a predefined set of operators for each of the operator types. They can be specified in markup with the `op` prefix. You can also specify `DisplayName` for each operator in order provide better description from the business perspective.

Possible operators for each type are:
    - `StringOperators` - `EqualOperator`, `NotEqualOperator`, `StartsWithOperator`, `EndsWithOperator`, `ContainsOperator`
    - `NumberOperators` - `EqualOperator`, `NotEqualOperator`, `GreaterThanOperator`, `GreaterThanOrEqualOperator`, `LessThanOperator`, `LessThanOrEqualOperator`
    - `BooleanOperators` - `TrueOperator`, `FalseOperator`
    - `EnumOperators` - `EqualOperator`, `NotEqualOperator`
    - `CollectionOperators`- `ContainsOperator`

If your type supports null values you can also use `NullOperator` and `NotNullOperator`.

Note that whenever having two or more `GridView`s with an identical data source, it is your responsibility to correctly define operators. Shared data source means that `FilteringOptions` are shared as well. Therefore, if you activate a filter on one of the grids, the same filter should be applied to all of them. This is generally not possible if the set of supported filters is not same on all `GridView`s and you may run into problems when a filter could not be fully applied on all `GridView`s.