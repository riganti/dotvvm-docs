## Sample 1: Basic Usage

The tabs can be defined inside the `<bp:TabControl>` individually. Each tab can have a different content in this case.

Alternatively, the tabs can be generated using the `DataSource` property bound to a collection in the viewmodel. In this case, the `TabHeaderTemplate` and `TabContentTemplate` properties are used to specify the header and content of the tabs.

If the tab header contains only a text value, you can use the `HeaderText` property instead of the `HeaderTemplate`.