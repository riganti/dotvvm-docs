# Overview

DotVVM allows interacting with REST APIs directly from the DotHTML views. 

For example, you can load data into [GridView](~/controls/builtin/GridView), [Repeater](~/controls/builtin/Repeater), and other controls directly from a REST API, or you can use [Button](~/controls/builtin/Button) or other controls to call REST API methods in response to user actions. 

It is also possible to refresh data that have already been loaded (in the `GridView` for example) based on a change of a particular viewmodel property, or explicitly.

## Build the REST API

It is possible to consume an external REST API from DotVVM, but in most scenarios you will probably be calling your own API. 

This API can be built in any technology, the most common scenario for .NET developers will be using [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0) or [ASP.NET Web API](https://docs.microsoft.com/en-us/aspnet/web-api/). For these two technologies, DotVVM offers special NuGet packages which allow to reuse data models between the API controllers and DotVVM viewmodels.

In order to consume the API from DotVVM, the API needs to expose [Open API](https://www.openapis.org/) metadata. 

> See [Provide API metadata](provide-api-metadata) chapter for more information.

## Register the REST API and generate the clients

To start using the API, you need to **generate C# and TypeScript clients**, and **register them** in the application.

The C# client class can be used in the viewmodels to access the API if you need to call it from the server. The TypeScript classes will be used on the client-side by DotVVM.

### Prerequisites

In order to generate the clients, you will need the following runtimes installed. 

* [.NET Core SDK](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/en/)

> These runtimes are not necessary for _running_ the app, but they are necessary to generate and compile the client classes - they need to be installed on the developer's machine.

If you have the runtimes, install the [DotVVM CLI](~/pages/concepts/cli/install) and TypeScript compiler (`tsc`):

```
dotnet tool install -g DotVVM.CommandLine
npm install -g typescript
```

### Configure TypeScript in the project

If you haven't used TypeScript in the project, you'll need to add support for it first.

**Option 1: Compile TypeScript via MSBuild**

Run `Install-Package Microsoft.TypeScript.MSBuild` in the Package Manager Console window to install TypeScript NuGet package. It should compile all `*.ts` files automatically as part of the project build.

**Option 2: Build with `tsc`**

If you prefer to compile the scripts manually, or if you are using webpack or other bundler, make sure you have `tsconfig.json` file present in the project directory. It can be created by running the following command:

```
cd <your-project-directory>
tsc init
```

### Generate the clients

To generate the clients, navigate to the project directory and run the following command:

```
cd <your-project-directory>
dotnet dotvvm api create http://localhost:43852/swagger/v1/swagger.json MyProject.Api Api/ApiClient.cs wwwroot/Scripts/ApiClient.ts
```

* The first argument is the URL of the Open API metadata. 
* The second argument is the namespace in which the client classes will be declared.
* The third argument is the path to the file in which the C# client classes will be generated.
* The fourth argument is the path to the file in which the TypeScript classes will be generated.

Finally, add the following registration in `DotvvmStartup.cs` file:

```CSHARP
config.RegisterApiClient(typeof(Api.Client), "http://localhost:43852/", "/Scripts/ApiClient.js", "_myApi");
```

* The first argument is the type of the generated C# client.
* The second argument is the base URL of the API.
* The third argument is relative URL to the JavaScript file of the TypeScript client class.
* The fourth argument is the name of variable which will be used in DotVVM views.

### Update the generated clients

Whenever the REST API changes, you should re-generate the client classes so they exactly match the interface of the REST API. To do that, run the following command in the project directory:

```
dotnet dotvvm api regen
```

This will refresh all registered APIs. If you have registered multiple APIs in the project and want to update only a specific API, add the URL of Swagger JSON metadata at the end of the command above.

## Use the API

The REST APIs can be used in [value bindings](~/pages/concepts/data-binding/value-binding) and in [static commands](../static-commands). 

You must not change the default `Newtonsoft.Json` settings for serialization and deserialization. This is due to the fact that the same settings are currently used within the generated API bindings. If you require changes to the settings, you can achieve this by using a different overload of `JsonConvert.SerializeObject` or `JsonConvert.DeserializeObject` and pass your settings as an additional parameter.

### Load data from API

You can use the variable registered in `DotvvmStartup.cs` in value bindings to load data into [GridView](~/controls/builtin/GridView), [Repeater](~/controls/builtin/Repeater), [ComboBox](~/controls/builtin/ComboBox), or other controls. 

The collection doesn't need to be declared in the viewmodel, which significantly reduces the amount of data sent to the server.

```DOTHTML
<dot:ComboBox DataSource="{value: _myApi.GetCountries()}" 
              SelectedValue="{value: CountryId}" 
              ItemTextBinding="{value: name}" 
              ItemValueBinding="{value: id}" />
```

It is possible to take advantage of the HTTP-level caching. If the REST API provides a `Cache-Control` header, DotVVM won't even make a request to the API - the data will be fetched from the browser cache. 

<!-- 
### Using GridViewDataSet

REST API bindings support the `GridViewDataSet<T>` object which can be used to perform sorting and paging. It must be supported on the REST API side - see [Building own REST API for REST API Bindings](/docs/tutorials/basics-rest-api-bindings-building-own-api) for more information.

You can use [GridView](~/controls/builtin/GridView) with paging and sorting like this:

```DOTHTML
<dot:GridView DataSource="{value: DataSet = _myApi.GetCompanies(DataSet.SortingOptions, DataSet.PagingOptions)}">
    ...
</dot:GridView>
<dot:DataPager DataSet="{value: DataSet}" />
```

The `DataSet` property must be declared in the viewmodel and its [Binding Direction](/docs/tutorials/basics-binding-direction) can be set to `ServerToClientFirstRequest`:

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

### Invoke actions on REST API

DotVVM can also invoke any methods on REST API using [Button](~/controls/builtin/Button) or other controls.

You can use [static command](../static-commands) to call any REST API method. 

```DOTHTML
<dot:Button Text="Save" Click="{staticCommand: CompanyId = _myApi.SaveCompany(Company)}" />
```

### Refresh data loaded from REST API

Imagine a page displaying a grid of companies, and allowing the users to edit these records. Both loading and saving changes uses REST API bindings. 

When the company is modified, the grid needs to be refreshed. DotVVM provides both automatic and manual ways to refresh data. 

#### Automatic refresh based on URL

DotVVM refreshes all binding expressions which use `HTTP GET` methods whenever another HTTP method (such as `POST`, `PUT` or `DELETE`) was posted to the same URL. 

If you load the `GridView` using `GET /api/companies`, and the save button calls `POST /api/companies`, the `GridView` will be refreshed automatically. 

#### Manual refresh based on viewmodel property

To make the REST API call refreshed when a viewmodel property changes, you can wrap the call on API binding expression variable into `_api.RefreshOnChange(apiCall, Property)`.

For example, you can refresh contents of a `GridView` based on country selected in a `ComboBox`:

```DOTHTML
<dot:ComboBox DataSource="{value: _myApi.GetCountries()}" 
              SelectedValue="{value: CountryId}" 
              ItemTextBinding="{value: name}" 
              ItemValueBinding="{value: id}" />

<dot:GridView DataSource="{value: _api.RefreshOnChange(_myApi.GetSales(CountryId), CountryId)}">
    ...
</dot:GridView>
```

If you need to refresh data based on multiple properties, you can pass an expression that uses these properties as a second argument:

```DOTHTML
<dot:GridView DataSource="{value: _api.RefreshOnChange(_myApi.GetSales(CountryId, TypeName), CountryId + '---' + TypeName)}">
    ...
</dot:GridView>
```

Whenever the value of the second argument changes, the API will be called again and the new value will be used.

#### Manual refresh on explicit events

If you need to refresh the data explicitly on some event (a button click for example), you can wrap the API call in `_api.RefreshOnEvent(apiCall, "YourEventName")`. You can trigger the event by calling `_api.PushEvent("YourEventName")`. 

```DOTHTML
<dot:Button Text="Refresh" Click="{staticCommand: _api.PushEvent("LoadCompanies")}" />

<dot:GridView DataSource="{value: _api.RefreshOnEvent(_myApi.GetCompanies(), "LoadCompanies")}">
    ...
</dot:GridView>
```

You can use `;` operator to combine multiple statements in the commands, so it is possible for example to save and refresh:

```DOTHTML
<dot:Button Text="Save" Click="{staticCommand: _myApi.SaveCompany(Company); _api.PushEvent("LoadCompanies")}" />
```

## See also

* [Provide API metadata](provide-api-metadata)
* [DotVVM CLI](~/pages/concepts/cli/install)
* [Value binding](~/pages/concepts/data-binding/value-binding)
* [Static commands](../static-commands)
