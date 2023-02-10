# Selectors

**Auto UI** supports fields in which the user can pick the value from some list of options. Selection of a single item as well as a selection of multiple items are supported.

## Pure selection

### Define selection type

First, you need to define a type of a selection. This type works as a "marker" type for the collection of options from which it is selected, and also specifies the type of the selected value (most commonly `int`, `Guid`, `string` or enum).

To do that, define a record which inherits from `Selection<TValue>` where `TValue` is the type of the selected value. 

You can define the selection like this:

```CSHARP
public record CountrySelection : Selection<Guid>;
```

The `Selection<TValue>` base class has the following properties:
* `TValue Value` - the selected value
* `string DisplayName` - the text representation of the option

### Apply the Selector attribute

Mark the property in the model object with the `[Selector(typeof(TSelectionType))]` attribute, or set the selector by the [convention](./metadata#convention-based-approach).

If the property is of a primitive type (`string`, numbers, date & time values, enums, `Guid`) or any of its nullable variants, the user will be able to select a single value in a [ComboBox](~/controls/builtin/ComboBox) control.

If the property is a collection of primitive types (for example `List<int>`, ...), Auto UI will generate a list of [CheckBox](~/controls/builtin/CheckBox) controls. 

> If there is a lot of items, the default controls may not be working well. You may consider implementing your own controls or finding third-party controls with search capabilites.

### Implement a service to provide the options

For each selection type, you have to implement `ISelectionProvider<TSelectionType>` which will provide the options whenever Auto UI will need them.

```CSHARP
public class CountrySelectionProvider : ISelectionProvider<CountrySelection>
{
    private readonly AppDbContext dbContext;

    public CountrySelectionProvider(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<List<CountrySelection>> GetSelectorItems()
    {
        return dbContext.Countries
            .OrderBy(c => c.Name)
            .Select(c => new CountrySelection()
            {
                Value = c.Id,
                DisplayName = c.Name
            })
            .ToListAsync();
    }
}
```

This class needs to be registered in the service collection:

```CSHARP
services.AddScoped<ISelectionProvider<CountrySelection>, CountrySelectionProvider>();
```

### Add the child viewmodel on the target page

The last step is to add a property of type `SelectionViewModel<TSelectionType>` in the viewmodel of the page where the property with `Selection` attribute is used. It can be in the root page viewmodel or anywhere the binding context hierarchy - it just has to be a public get & set property and it must be initialized. How the property is named is not important - Auto UI will be looking at the type of the property.

```CSHARP
public SelectionViewModel<CountrySelection> Countries { get; set; } = new();
```

Now, DotVVM will be able to obtain the list of options, and it will store it automatically in this child viewmodel.

## Selections with parameters

Sometimes, the list of options depend on other values. This often appears if a cascading drop-down list scenarios or other more complex situations. 

There is a second version of the interface `ISelectionProvider<TSelectionType, TParameter>` with an additional type parameter representing the parameters which are needed to provide the options (it can be either a primitive type or an object).

In such case, you need to perform the following steps:

* Define the selection type as specified in the [Define selection type](#define-selection-type) chapter.

```CSHARP
public record LocationByCountrySelection : Selection<int>;
```

* Implement the `ISelectionProvider<TSelectionType, TParameter>` interface:

```
public class LocationByCountrySelectionProvider : ISelectionProvider<LocationByCountrySelection, int?>
{
    private readonly AppDbContext dbContext;

    public LocationByCountrySelectionProvider(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<List<LocationByCountrySelection>> GetSelectorItems(int? countryId)
    {
        if (countryId == null)
        {
            return Task.FromResult(new List<LocationByCountrySelection>());
        }

        return dbContext.Locations
            .Where(l => l.CountryId == countryId)
            .OrderBy(c => c.Name)
            .Select(l => new LocationByCountrySelection()
            {
                Value = l.Id,
                DisplayName = l.Name
            })
            .ToListAsync();
    }
}
```

* Register the service in the service collection:

```CSHARP
services.AddScoped<ISelectionProvider<LocationByCountrySelection, int?>, LocationByCountrySelectionProvider>();
```

* Add the property in the viewmodel and initialize it in the constructor. Pass a lambda function which will provide the value of the parameter:

```CSHARP
public SelectionViewModel<LocationByCountrySelection, int?> Locations { get; set; }

// constructor
public MyViewModel() 
{
    Locations = new SelectionViewModel<LocationByCountrySelection, int?>(() => this.CountryId);
    // ...
}
```

* If the parameter value (in our case it's the `CountryId` property) changes, you can easily request the list of options to be refreshed by calling the `RequestRefresh()` method:

```DOTHTML
<auto:Form DataContext="{value: Meetup}" 
           Validation.Enabled="false"
           Changed-CountryId="{command: _root.Locations.RequestRefresh()}" />
```

## See also

* [Auto UI Overview](./overview)
* [Metadata](./metadata)
* [Extensibility](./extensibility)
* [Form control](~/controls/builtin-autoui/Form)
