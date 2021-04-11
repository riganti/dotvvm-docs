# Provide API metadata

In order to [call REST API](overview) from DotVVM, the API needs to provide [Open API](https://www.openapis.org/) metadata.

## ASP.NET Core or ASP.NET Web API

If you decide to build the REST API using [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0) or [ASP.NET Web API](https://docs.microsoft.com/en-us/aspnet/web-api/), there are NuGet packages you can use to allow sharing data models between DotVVM application and the REST API.

These NuGet packages work with [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), a popular library that generates Open API metadata. 

# [Install Swashbuckle ASP.NET Core](#tab/aspnetcore)

First, make sure you have `Swashbuckle` installed and configured in your project.

* [Swashbuckle - ASP.NET Core](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [Swashbuckle - ASP.NET Core - Newtonsoft](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Newtonsoft/) (_Optional: This package is necessary only for DotVVM 2.5+ projects that target ASP.NET Core 2.x, or projects using `Newtonsoft.Json` serializer with ASP.NET Core 3.x_)

Then install the following NuGet package to the REST API project:

```
Install-Package DotVVM.Api.Swashbuckle.AspNetCore

# optional - install only if you are using Newtonsoft.Json
Install-Package Swashbuckle.AspNetCore.Newtonsoft
```

To enable DotVVM integration, call the `EnableDotvvmIntegration` extension method in the Swashbuckle configuration in `Startup.cs` file.

```CSHARP
services.Configure<DotvvmApiOptions>(opt => 
{
    // TODO: configure DotVVM Swashbuckle options
});

services.AddSwaggerGen(options => {
    ...
    options.EnableDotvvmIntegration();
});
```

**Optional**: In case of DotVVM 2.5 or newer with ASP.NET Core 2.x, or ASP.NET Core 3.x with `Newtonsoft.Json` serializer, you also need to call `AddSwaggerGenNewtonsoftSupport`.

```CSHARP
services.AddSwaggerGenNewtonsoftSupport();
```

# [Install Swashbuckle for OWIN](#tab/owin)

First, make sure you have `Swashbuckle` installed and configured in your project. 

* [Swashbuckle - classic ASP.NET](https://github.com/domaindrivendev/Swashbuckle) 

Then, install the following NuGet package to the REST API project:

```
Install-Package DotVVM.Api.Swashbuckle.Owin
```

To enable DotVVM integration, call the `EnableDotvvmIntegration` extension method in the Swashbuckle configuration in `SwaggerConfig.cs` file.

```CSHARP
config.EnableSwagger(c =>
    {
        ...
        c.EnableDotvvmIntegration(opt => 
        {
            // TODO: configure DotVVM Swashbuckle options    
        });
    })
    .EnableSwaggerUi(c => { ... });
```

***

### Share models between API and DotVVM app

By default, [DotVVM CLI](~/pages/concepts/cli/install) generates classes for all types used in the REST API (in both C# and TypeScript clients). For example, if the API returns a list of orders, there will be the `Order` class in the generated client.

If the API is hosted in the same project as the DotVVM application, or if the API project can share these types with the DotVVM application using a class library, you can register these types as **known types**. In such case, they won't be included in the generated clients, and you just need to make sure both DotVVM and API can see these types with the same name and namespace.

To register known types, configure the DotVVM integration like this:

# [Register known types in ASP.NET Core](#tab/aspnetcore)

```CSHARP
services.Configure<DotvvmApiOptions>(opt => 
{
    // add a single type
    opt.AddKnownType(typeof(Order));

    // add all types from the assembly
    opt.AddKnownAssembly(typeof(Order).Assembly);

    // add all types from the namespace
    opt.AddKnownNamespace(typeof(Order).Namespace);
});
```

# [Register known types in OWIN](#tab/owin)

```CSHARP
c.EnableDotvvmIntegration(opt => 
{
    // add a single type
    opt.AddKnownType(typeof(Order));

    // add all types from the assembly
    opt.AddKnownAssembly(typeof(Order).Assembly);

    // add all types from the namespace
    opt.AddKnownNamespace(typeof(Order).Namespace);
});
```

***

<!-- Some DotVVM types, such as `GridViewDataSet`, `SortingOptions` or `PagingOptions` are registered as known types by default. This makes building APIs with paging and sorting easier.

## Working with GridViewDataSet

You can declare API controler methods which return `GridViewDataSet` and accepts `SortingOptions` and `PagingOptions`:

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
```

Because we are using HTTP GET, the `[FromQuery]` attribute is used to place all parameters in the URL - the URL will look like this:

```
/api/companies?sortExpression=Name&sortDescending=false&pageIndex=0&pageSize=20
```

However, the generated C# method signature looks like this:

```
public GridViewDataSet<Company> GetCompanies(string sortExpression, bool sortDescending, int pageIndex, int pageSize);
```

Because this would be uncomfortable to consume from the page, there is the `[AsObject]` attribute - it tells the generator to keep these object together. The signature with this attribute looks like this:

```
public GridViewDataSet<Company> GetCompanies(ISortingOptions sortingOptions, IPagingOptions pagingOptions);
```
 -->

## Azure Functions

If you want to consume [Azure Functions](https://azure.microsoft.com/en-us/services/functions/), you can refer to the [Open API definition tutorial](https://docs.microsoft.com/en-us/azure/azure-functions/functions-openapi-definition) in the official docs.

## Other technologies

If the API is built with different technology, you'll need to obtain the Open API specification of the API. Most frameworks should be able to generate the document for you. 

Alternatively, you can build it yourself in [Swagger Editor](https://editor.swagger.io).

## See also

* [REST API bindings](overview)
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
