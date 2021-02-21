# Overview

DotVVM allows interacting with REST APIs directly from the DOTHTML views. You can fill [GridView](/docs/controls/builtin/GridView/{branch}), [Repeater](/docs/controls/builtin/Repeater/{branch}) and other controls with data from the REST API, and use [Button](/docs/controls/builtin/Button/{branch}) or other controls to call REST API methods as a response to user action. It is also possible to refresh data that have already been loaded (in the `GridView` for example) based on a change of a particular viewmodel property, or explicitly.

## Building the REST API

It is possible to consume an external REST API, but in most scenarios, you will be consuming your own API. This API can be build in any technology, the most common scenario for .NET developers will be **ASP.NET Web API** or **ASP.NET MVC Core**. For these two technologies, DotVVM includes special NuGet packages that allow to reuse objects between the API controllers and DotVVM viewmodels.

> See [Building own REST API for REST API Bindings](/docs/tutorials/basics-rest-api-bindings-building-own-api/{branch}) for more information.

The API needs to expose Swagger JSON metadata so DotVVM can generate client classes. 

## Registering the REST API and generating the clients

To start using the API, you need to register it using [DotVVM Command Line](/docs/tutorials/advanced-dotvvm-command-line/{branch}) and generate C# and TypeScript client classes. The C# client class can be used in the viewmodels to access the API. The TypeScript class is used on the client side by DotVVM and binding expressions which use the API are using this class.

To register the API, run the following command in the project directory:

```
dotnet dotvvm api create http://localhost:43852/swagger/v1/swagger.json MyProject.Api Api/ApiClient.cs wwwroot/Scripts/ApiClient.ts
```

* The first argument is the URL of the Swagger JSON metadata. 
* The second argument is the namespace in which the client classes will be declared.
* The third argument is the path to the file in which the C# client classes will be generated.
* The fourth argument is the path to the file in which the TypeScript classes will be generated.

If you haven't been using TypeScript in the project, add the [tsconfig.json](https://www.typescriptlang.org/docs/handbook/tsconfig-json.html) file in the root folder of the project and install the TypeScript command-line compiler (you will need [Node.js](https://nodejs.org/en/) for this):

```
npm install typescript -g
```

Finally, add the following registration in `DotvvmStartup.cs` file:

```CSHARP
config.RegisterApiClient(typeof(Api.Client), "http://localhost:43852/", "/Scripts/ApiClient.js", "_myApi");
```

* The first argument is the type of the generated C# client.
* The second argument is the base URL of the API.
* The third argument is relative URL to the JavaScript file of the TypeScript client class.
* The fourth argument is the name of variable which will be used in DotVVM views.

## Updating the generated clients

Whenever the REST API changes, you should generate the client classes so they exactly match the interface of the REST API. To do that, run the following command in the project directory:

```
dotnet dotvvm api regen
```

This will refresh all registered APIs. If you want to update only a specific API, add the URL of Swagger JSON metadata at the end of the command above.

## Using the API in binding expressions

The REST APIs can be used in [Value Binding](/docs/tutorials/basics-value-binding/{branch}) and in [Static Command Binding](/docs/tutorials/basics-static-command-binding/{branch}). 

You must not change the default `Newtonsoft.Json` settings for serialization/deserialization. This is due to the fact that the same settings are currently used within the generated API bindings. If you require changes to the settings, you can achieve this by using a different overload of `JsonConvert.SerializeObject` or `JsonConvert.DeserializeObject` and pass your settings as an additional parameter.

## Loading data from API

You can use the variable registered in `DotvvmStartup.cs` in value bindings to load data into [GridView](/docs/controls/builtin/GridView/{branch}), [Repeater](/docs/controls/builtin/Repeater/{branch}), [ComboBox](/docs/controls/builtin/ComboBox/{branch}) or other controls. The collection doesn't need to be declared in the viewmodel and it is not transferred on postbacks, which significantly reduces the amount of data being sent to the server.

```DOTHTML
<dot:ComboBox DataSource="{value: _myApi.GetCountries()}" SelectedValue="{value: CountryId}" ItemTextBinding="{value: name}" ItemValueBinding="{value: id}" />
```

<!-- 
### Using GridViewDataSet

REST API bindings support the `GridViewDataSet<T>` object which can be used to perform sorting and paging. It must be supported on the REST API side - see [Building own REST API for REST API Bindings](/docs/tutorials/basics-rest-api-bindings-building-own-api/{branch}) for more information.

You can use [GridView](/docs/controls/builtin/GridView/{branch}) with paging and sorting like this:

```DOTHTML
<dot:GridView DataSource="{value: DataSet = _myApi.GetCompanies(DataSet.SortingOptions, DataSet.PagingOptions)}">
    ...
</dot:GridView>
<dot:DataPager DataSet="{value: DataSet}" />
```

The `DataSet` property must be declared in the viewmodel and its [Binding Direction](/docs/tutorials/basics-binding-direction/{branch}) can be set to `ServerToClientFirstRequest`:

```CSHARP
[Bind(Direction.ServerToClientFirstRequest)]
public GridViewDataSet<Company> DataSet { get; set; } = new GridViewDataSet<Company>() 
{
    SortingOptions =
    {
        SortExpression = nameof(Company.Id)
    },
    PagingOptions =
    {
        PageSize = 10
    }
};
```

The API controller method can look like this:

```CSHARP
[HttpGet]
public GridViewDataSet<Company> GetCompanies([FromQuery, AsObject(typeof(ISortingOptions))]SortingOptions sortingOptions, [FromQuery, AsObject(typeof(IPagingOptions))]PagingOptions pagingOptions)
{
    var dataSet = new GridViewDataSet<Company>()
    {
        PagingOptions = pagingOptions,
        SortingOptions = sortingOptions
    };
    dataSet.LoadFromQueryable(companiesService.GetAllCompaniesQueryable());
    return dataSet;
}
``` -->

## Invoking actions on REST API

DotVVM can also invoke any methods on REST API using [Button](/docs/controls/builtin/Button/{branch}) or other controls which are capable of invoking commands.

You can use [Static Command Binding](/docs/tutorials/basics-static-command-binding/{branch}) to call any REST API method. 

```DOTHTML
<dot:Button Text="Save" Click="{staticCommand: CompanyId = _myApi.SaveCompany(Company)}" />
```

## Refreshing data loaded from REST API

Imagine a page displaying a grid of companies and allowing the users to edit these records. Both loading and saving changes uses REST API bindings. When the company is modified, the grid needs to be refreshed. 

DotVVM provides both automatic and manual ways to refresh data. 

### Automatic refresh based on URL

DotVVM refreshes all binding expressions which use `HTTP GET` methods whenever another HTTP method (such as `POST`, `PUT` or `DELETE`) was posted to the same URL. 

If you load the `GridView` using `GET /api/companies` and the save button calls `POST /api/companies`, the `GridView` will be refreshed automatically. 

### Manual refresh based on viewmodel property

To make the REST API call refreshed when a viewmodel property changes, you can wrap the call on API binding expression variable into `_api.RefreshOnChange(apiCall, Property)`.

For example, you can refresh contents of a [GridView](/docs/controls/builtin/GridView/{branch}) based on country selected in a [ComboBox](/docs/controls/builtin/ComboBox/{branch}):

```DOTHTML
<dot:ComboBox DataSource="{value: _myApi.GetCountries()}" SelectedValue="{value: CountryId}" ItemTextBinding="{value: name}" ItemValueBinding="{value: id}" />

<dot:GridView DataSource="{value: _api.RefreshOnChange(_myApi.GetSales(CountryId), CountryId)}">
    ...
</dot:GridView>
```

If you need to refresh data based on multiple properties, you can pass an expression that uses these properties as a second argument:

```DOTHTML
<dot:GridView DataSource="{value: _api.RefreshOnChange(_myApi.GetSales(CountryId, TypeName), CountryId + '/' + TypeName)}">
    ...
</dot:GridView>
```

Whenever the value of the second argument changes, the API call will be refreshed.

### Manual refresh on explicit events

If you need to refresh the data explicitly on some event (a button click for example), you can wrap the API call in `_api.RefreshOnEvent(apiCall, YourEventName)`. You can trigger the event by calling `_api.PushEvent(YourEventName)`. 

```DOTHTML
<dot:Button Text="Refresh" Click="{staticCommand: _api.PushEvent('LoadCompanies')}" />

<dot:GridView DataSource="{value: _api.RefreshOnEvent(_myApi.GetCompanies(), 'LoadCompanies')}">
    ...
</dot:GridView>
```

You can use `;` operator to combine multiple statements in the commands, so it is possible for example to save and refresh:

```DOTHTML
<dot:Button Text="Save" Click="{staticCommand: _myApi.SaveCompany(Company); _api.PushEvent('LoadCompanies')}" />
```
