Used in cooperation with a [GridView](~/controls/builtin/GridView), [Repeater](~/controls/builtin/Repeater) or a similar control, the `DataPager` can render the buttons which allow the user to browse a large collection of records using pages.

The `DataPager` requires a `GridViewDataSet` object in the viewmodel. This object keeps track of the page size, current page number, and also holds
the data of the current page. It can also be used as a `DataSource` in `GridView`, `Repeater` or similar controls.