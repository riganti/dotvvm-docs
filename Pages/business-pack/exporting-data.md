# Exporting data

**DotVVM Business Pack** supports exporting data from [GridView](~/controls/businesspack/GridView) to CSV, Excel and PDF files. There are plenty of configuration options that allow you to customize the resulting file before returning it to the user.

> The Excel exports are using the [ClosedXML](https://github.com/ClosedXML/ClosedXML) library. When you add the NuGet package to your project, the library and all its dependencies will be referenced automatically.

> The PDF exports are being re-implemented as they depend on an library which not supported anymore. The functionality will be changed in one of the future releases.

## Install NuGet package

The export functionality is distributed in separate NuGet packages. 

In order to **export to an Excel file**, install the following package in your project:

```
Install-Package DotVVM.BusinessPack.Export.Excel
```

For CSV files, use the `DotVVM.BusinessPack.Export.Csv` package instead.

## Implement a basic Excel export

The export functionality need to obtain an instance of the `GridView` control. In order to do that, you should mark the control with an `ID` attribute:

```DOTHTML
<bp:GridView ... ID="MyGrid">
```

Now, you can add a button which will perform the export:

```DOTHTML
<bp:Button Text="Export XLSX" Click="{command: Export()}" />
```

The `Export` method should look like this:

```CSHARP
// using DotVVM.BusinessPack.Controls;

public async Task Export()
{
    // get grid view
    var gridView = (GridView)Context.View.FindControlByClientId("MyGrid");
    
    // create the data set for export
    var dataSet = new BusinessPackDataSet<T>();
    dataSet.SortingOptions = Entries.SortingOptions;
    dataSet.FilteringOptions = Entries.FilteringOptions;
    
    // TODO: load all data in the data set (paging is not set so all records should be loaded)
    dataSet.LoadFromQueryable(...);

    // export the data
    var settings = GridViewExportExcelSettings<T>.Default;
    
    // TODO: configure export settings if needed

    var export = new GridViewExportExcel<T>(settings);
    using var file = export.Export(gridView, dataSet);

    // return the file to the user
    await Context.ReturnFileAsync(file, "export.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
}
```

The exported file should contain all rows (without respect to paging) with the same columns that are specified in the `GridView` control.

## Configure Excel exports

The `GridViewExportExcelSettings<T>.Default` returns the default configuration: 

* the columns will be auto-sized so all content will fit in the cells
* the exported table will have headers that support sorting and filtering 

If you don't want these settings, you can use `GridViewExportExcelSettings<T>.Empty` and build your configuration from scratch.

### Worksheet-level rules

You can call several extension methods on the settings object to configure additional aspects of the export:

* `.WithAdjustToContents()` method adjusts the column widths so all content will fit in the cells. This rule is enabled by default.
* `.WithStartAddress(row, column)` tells the exporter on which position the table should start. By default, it starts on the first row and the first column, however sometimes it is useful to leave several blank rows above the table.
* `.WithWorksheetName("My worksheet")` allows to customize the name of the sheet.
* `.WithTableHeader()` tells the exporter to configure the exported range as a table with support for sorting and filtering.
* `.WithRule(onWorksheetExporting, onDataExporting, onDataExported)` methods allows you to hook into specific phases of the export. You have access to the worksheet objects and you can perform any customization to the exported file.

### Column-level rules

To configure behavior of specific columns, you can call the `.ForColumn(c => c.PropertyName, ...)` or `.ForColumn("ColumnName", ...)`.

The first method should be preferred as it is strongly-typed. It will match all columns that are bound to the specified property path.

```DOTHTML
<bp:GridViewTextColumn Value="{value: FirstName}" />
```

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn(c => c.FirstName, options => ...)   // configure the column bound to FirstName property
```

The second method can be used to configure columns which don't use the `Value` property (for example the `<bp:GridViewTemplateColumn>`). Such columns must be marked with `ColumnName` property:

```DOTHTML
<bp:GridViewTemplateColumn ColumnName="MyTemplateColumn">
   ...
</bp:GridViewTemplateColumn>
```

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn("MyTemplateColumn", options => ...)   // configure the column bound to FirstName property
```

The columns can specify the following options:

* `.WithHeaderText("My column")` method sets the specified text in the header cell.
* `.WithAdjustToContents()` method adjusts the column width so all content will fit in the cells.
* `.WithNumberFormat(format)` method specifies the number format for the values. Use the [Excel value format strings](https://support.microsoft.com/en-us/office/number-format-codes-5026bbd6-04bc-48cd-bf33-80f18b4eae68).
* `.WithDataType(dataType)` method specifies the data type of the column. The possible values are `Text`, `Number`, `Boolean`, `DateTime` and `TimeSpan`. 
* `.WithIgnore()` method excludes the column from the export.
* `.WithValueTransform<TValue>(v => ...)` allows to pre-process the value in the column before it is exported. This allows you to perform any custom conversions.
* `.WithRule(onDataTransforming, onColumnExporting, onColumnExported, onCellExporting, onCellExported)` methods allows you to hook into specific phases of the export. You have access to the worksheet objects and you can perform any customization to the exported file.

### Column value providers

Some columns (for example the `GridViewTemplateColumn`) don't have the `Value` property, so the exporter cannot figure out which property will be displayed in the column. 
In order to export such columns, you have to register a custom value provider:

```DOTHTML
<bp:GridViewTemplateColumn HeaderText="Hours" ColumnName="Hours">
    <bp:Text>{{value: $"{Hours:n1} h"}}</bp:Text>
</bp:GridViewTemplateColumn>
```

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .WithColumnValueProvider("Hours", (control, column, entry) => Math.Round(entry.Hours, 1));
```

Without setting the value provider, the column in the exported file will always be empty.

### Common use-cases

This section demonstrates how to perform commonly requested tasks.

#### Exclude column from export

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn(c => c.FirstName, options => options.WithIgnore());
```

#### Change column header

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn("Date", options => options.WithHeaderText("Date of the entry"));
```

#### Change number format

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn(c => c.Price, options => options.WithNumberFormat("#.000"));
```

#### Export only date without time

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .ForColumn(c => c.CreatedDate, options => options.WithValueTransform<DateTime>(v => v.Date));
```

#### Place title on top of the table

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .WithStartAddress(3, 1)
    .WithRule(onDataExported: (worksheet, _) =>
    {
        var cell = worksheet.Cell(1, 1);
        cell.SetValue("Time Tracking report");
        cell.Style.Font.SetFontSize(20);
    });
```

#### Add a subtotals row

```CSHARP
var settings = GridViewExportExcelSettings<T>.Default
    .WithRule(onDataExported: (worksheet, _) =>
    {
        var table = worksheet.Tables.Single();
        table.SetShowTotalsRow(true);
        table.Field(0).TotalsRowLabel = "TOTAL HOURS";
        
        // calculate SUM in the fourth column
        table.Field(3).TotalsRowFunction = XLTotalsRowFunction.Sum;
    });
```

## See also

* [DotVVM Business Pack overview](./getting-started)
* [Themes](./themes/overview)
* [Business Pack controls](~/controls/businesspack/Alert)
* [Release notes](./release-notes)