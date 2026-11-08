## Sample 2: Filtering

To enable filtering, use the `FilteredGridViewDataSet<T>` class in the viewmodel, and set `AllowFiltering="true"` on the Tailwind columns which the user can filter by. The filter operators are chosen based on the type of the column value (text, number, date, boolean, enum, or a collection of enums).

Columns with `Searchable="true"` are included in the full-text search. Use `FilteringOptions.AdditionalFilters` in the viewmodel to add filters for properties which are not displayed in any column.

The filters are applied when the data set is loaded using `LoadFromQueryable`, together with sorting and paging. If you use the `LoadData` static command to load the data, configure the filter fields (`AdditionalFilters`) on the returned data set.

> Filtering is new in DotVVM 5.1.
