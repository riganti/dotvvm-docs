# DotVVM Dynamic Data

> From **DotVVM 4.1**, the **DotVVM Dynamic Data** package is considered obsolete. It will be published along with DotVVM, but it will not get new features and enhancements. **Please migrate to [DotVVM Auto UI](~/pages/concepts/auto-ui/overview) which provides more features and better performance**. 

**DotVVM Dynamic Data** is an extension of DotVVM which can auto-generate forms and [GridView](~/controls/builtin/GridView) controls based on the metadata and data annotation attributes in model classes.

For example, consider the following class:

```CSHARP
public class EmployeeDTO
{
    [Display(AutoGenerateField = false)]
    public int Id { get; set; }
	
    [Required]
    [EmailAddress]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }  
    ...
}
```

Based on the properties in this class, and the data annotation attributes provided, **DotVVM Dynamic Data** can auto-generate a form for editing the employee. If you want to display a list of employees, it can also auto-generate the columns in `GridView`. 

This library is useful in simple applications where the user interface closely reflects the schema of the model. In such case, the user interface is practically empty as the forms and grids are generated automatically. 

## Install DotVVM Dynamic Data

First, you need to install the `DotVVM.DynamicData` NuGet package in your project:

```
Install-Package DotVVM.DynamicData
```

To use Dynamic Data, add the following line to the `DotvvmStartup.cs` file.

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    options.AddDynamicData(dynamicDataConfig);
}
```

In case you did not provide the `dynamicDataConfig` (or it was `null`), you should call the `AddDynamicDataConfiguration` yourself. This will create a default configuration and register dynamic data controls for you. You can achieve this by adding the folowing line to the `DotvvmStartup.cs` file.

```CSHARP
private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
{
    config.AddDynamicDataConfiguration();
}
```

## Add data annotations to model classes

This will allow to provide UI metadata using the standard .NET Data Annotations attributes.

```CSHARP
public class EmployeeDTO
{
    [Display(AutoGenerateField = false)]        // this field will be hidden
    public int Id { get; set; }


    // first group of fields
	
    [Required]
    [EmailAddress]
    [Display(Name = "User Name", Order = 1, GroupName = "Basic Info")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "First Name", Order = 2, GroupName = "Basic Info")]
    public string FirstName { get; set; }  

    [Required]
    [Display(Name = "Last Name", Order = 3, GroupName = "Basic Info")]
    public string LastName { get; set; }
    
    [DisplayFormat(DataFormatString = "d")]
    [Display(Name = "Birth Date", Order = 4, GroupName = "Basic Info")]
    public DateTime BirthDate { get; set; }
    
	
    // second group of fields
	
    [Display(Name = "E-mail", Order = 11, GroupName = "Contact Info")]
    public string PersonalEmail { get; set; }

    [Display(Name = "Phone", Order = 12, GroupName = "Contact Info")]
    public string PersonalPhone { get; set; }
    
}
```

## Generate GridView columns

Now, when you have your DTO class decorated with data annotation attributes, you can auto-generate [GridView](~/controls/builtin/GridView) columns.

DotVVM Dynamic Data contains the `DynamicDataGridViewDecorator` control. Use this decorator around `GridView` to initialize its `Columns` collection.

```DOTHTML
<dd:DynamicDataGridViewDecorator>
    <dot:GridView DataSource="{value: Employees}" />
</dd:DynamicDataGridViewDecorator>
```

If you want to add your own columns (e.g. the edit button) to the auto-generated ones, you can use the `ColumnPlacement` to specify whether
the generated columns should appear on the left side or the right side from your own columns.

```DOTHTML
<dd:DynamicDataGridViewDecorator ColumnPlacement="Left">
    <dot:GridView Type="Bordered" DataSource="{value: Employees}">
        <Columns>
            <!-- The auto-generated columns will appear here because ColumnPlacement is Left. -->
            
            <dot:GridViewTemplateColumn>   <!-- your own column -->
                <dot:LinkButton Click="{command: _parent.Edit(Id)}" Text="Edit" />
            </dot:GridViewTemplateColumn>
        </Columns>
    </dot:GridView>
</dd:DynamicDataGridViewDecorator>
```

## Generating Forms

DotVVM Dynamic Data also contain the `DynamicEntity` control that can be used to generate forms. 

```DOTHTML
<dd:DynamicEntity DataContext="{value: EditedEmployee}" />
``` 

The control takes its `DataContext` and generates form fields for all properties of the object using the metadata from data annotation attributes. 

If you want the form to have a custom layout, you need to use the group names in the `Display` attribute, and render each group separately. If you specify the `GroupName` property, the `DynamicEntity` will render only fields from this group. 

```DOTHTML
<!-- This will render two columns. -->
<div class="row">
    <div class="col-md-6">
        <dd:DynamicEntity DataContext="{value: EditedEmployee}" GroupName="Basic Info" />
    </div>
    <div class="col-md-6">
        <dd:DynamicEntity DataContext="{value: EditedEmployee}" GroupName="Contact Info" />
    </div>
</div>
``` 

By default, the form is rendered using the [TableDynamicFormBuilder](https://github.com/riganti/dotvvm-dynamic-data/blob/master/src/DotVVM.Framework.Controls.DynamicData/Builders/TableDynamicFormBuilder.cs) class. This class renders a plain HTML table with rows for each of the form fields. 

You can write your own form builder and register it in the `DotvvmStartup.cs` class. The builder must implement the `IFormBuilder` interface.
 
```CSHARP
config.Services.AddSingleton<IFormBuilder, YourOwnFormBuilder>();
```

## Loading Metadata from Resource Files

Decorating every field with the `[Display(Name = "Whatever")]` is not very effective when it comes to localization - you need to specify the resource file type and resource key. Also, if you have multiple entities with the `FirstName` property, you'll probably want to use the same field name for all of them. 

That's why DotVVM Dynamic Data comes with the resource-based metadata providers. They can be registered in the `DotvvmStartup.cs` like this:

```CSHARP
config.RegisterResourceMetadataProvider(typeof(Resources.ErrorMessages), typeof(Resources.PropertyDisplayNames));
```

The `ErrorMessages` and `PropertyDisplayNames` are RESX files in the `Resources` folder and they contain the default error messages and display names of the properties.

### Localizing Error Messages

If you use the `[Required]` attribute and you don't specify the `ErrorMessage` or `ErrorMessageResourceName` on it, the resource provider will look in the `ErrorMessages.resx` file
and if it finds the `Required` key there, it'll use this resource item to provide the error message.

Your `ErrorMessages.resx` file may look like this:

```
Resource Key            Value
-------------------------------------------------------------------------------------
Required                {0} is required!    
EmailAddress            {0} is not a valid e-mail address!
...
```

### Localizing Property Display Names

The second resource file `PropertyDisplayNames.resx` contains the display names. If the property doesn't have the `[Display(Name = "Something")]` attribute, the provider will look in the 
resource file for the following values (in this order). If it finds an item with that key, it'll use the value as a display name of the field

* `TypeName_PropertyName`
* `PropertyName`

So if you want to use the text "Given Name" for the `FirstName` property in all classes, with the exception of the `ManagerDTO` class where you need to use the "First Name" text, your resource file
should look like this:

```
Resource Key            Value
-------------------------------------------------------------------------------------
FirstName               Given Name
ManagerDTO_FirstName    First Name
...
```

## Extensibility

### Custom metadata providers

You can provide your own implementation of metadata providers. This may be useful if you want to read the metadata from other place than .NET attributes, for example from a database.

* `IPropertyDisplayMetadataProvider` provides basic information about properties - the display name, format string, order, group name (you can split the fields into multiple groups and render each group separately).

* `IViewModelValidationMetadataProvider` allows to retrieve all validation attributes for each property.

### Custom editors

Currently, the framework supports `TextBox` and `CheckBox` editors, which can edit string, numeric, date-time and boolean values. If you want to support any other data type, you can implement your own editor and grid column. 

You need to derive from the [FormEditorProviderBase](https://github.com/riganti/dotvvm-dynamic-data/blob/master/src/DotVVM.Framework.Controls.DynamicData/PropertyHandlers/FormEditors/FormEditorProviderBase.cs) to implement a custom editor in the form, and to derive from the [GridColumnProviderBase](https://github.com/riganti/dotvvm-dynamic-data/blob/master/src/DotVVM.Framework.Controls.DynamicData/PropertyHandlers/GridColumns/GridColumnProviderBase.cs) to implement custom GridView column.

Then, you have to register the editor in the `DotvvmStartup.cs` file. Please note that the order of editor providers and grid columns matters. The Dynamic Data will use the first provider which returns `CanHandleProperty = true` for the property.

```CSHARP
dynamicDataConfig.FormEditorProviders.Add(new YourEditorProvider());
dynamicDataConfig.GridColumnProviders.Add(new YourGridColumnProvider());
```

## See also

* [DotVVM Dynamic Data GitHub repository](https://github.com/riganti/dotvvm-dynamic-data)
* [Northwind Store Sample](https://github.com/riganti/dotvvm-samples-northwind)


