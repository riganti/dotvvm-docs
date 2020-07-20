# REST API Bindings: Building own API

> This feature is new in **DotVVM 2.0**. See [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/{branch}) for more details about configuration.

If you decide to build the REST API using **ASP.NET Web API** or **ASP.NET MVC Core**, there are NuGet packages you can use to allow sharing objects between DotVVM application and the REST API.

These NuGet packages work with **Swashbuckle**, a popular library that exposes Swagger JSON metadata. 

## Installing Swashbuckle extensions for DotVVM (OWIN)

First, make sure you have Swashbuckle installed and configured in your project. 

* [Swashbuckle - ASP.NET Core](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

Then, install the following NuGet package to the REST API project:

```
Install-Package DotVVM.Api.Swashbuckle.Owin
```

## Installing Swashbuckle extensions for DotVVM (ASP.NET Core)

First, make sure you have Swashbuckle installed and configured in your project.

* [Swashbuckle - classic ASP.NET](https://github.com/domaindrivendev/Swashbuckle) 

Then install the following NuGet package(s) to the REST API project:

```
Install-Package DotVVM.Api.Swashbuckle.AspNetCore

# The following package is necessary for DotVVM 2.5+ projects that target ASP.NET Core < 3.0
Install-Package Swashbuckle.AspNetCore.Newtonsoft
```

## Configure Swashbuckle extensions for DotVVM (OWIN)

To enable DotVVM integration, call the `EnableDotvvmIntegration` extension method in the Swashbuckle configuration.

In OWIN, this is typically done in `SwaggerConfig.cs` file.

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

## Configure Swashbuckle extensions for DotVVM (ASP.NET Core)

To enable DotVVM integration, call the `EnableDotvvmIntegration` extension method in the Swashbuckle configuration.

In ASP.NET Core, this is configured in `Startup.cs` file.

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

Additionally from DotVVM 2.5, it is also necessary to override the default schema-naming strategy using the call to `CustomSchemaIds`.

```CSHARP
services.Configure<DotvvmApiOptions>(opt => 
{
    // TODO: configure DotVVM Swashbuckle options
});

string GetCustomSchemaId(Type modelType)
{
    if (!modelType.IsConstructedGenericType) return modelType.Name.Replace("[]", "Array");

    var generics = modelType.GetGenericArguments()
        .Select(genericArg => CustomSchemaId(genericArg))
        .Aggregate((previous, current) => previous + current);

    return $"{modelType.Name.Split('`').First()}[{generics}]";
}

services.AddSwaggerGen(options => {
    ...
    options.CustomSchemaIds(type => GetCustomSchemaId(type))
    options.EnableDotvvmIntegration();
});
```

Furthermore, if you plan to use DotVVM 2.5+ with ASP.NET Core < 3.0, you additionally need to call `AddSwaggerGenNewtonsoftSupport`.

```CSHARP
services.AddSwaggerGenNewtonsoftSupport();
```

### Registering known types

By default, [DotVVM Command Line](/docs/tutorials/advanced-dotvvm-command-line/{branch}) generates classes for all types used in the REST API (in both C# and TypeScript clients). For example, if the API returns a list of orders, there will be the `Order` class in the generated client.

If the API is hosted in the same project as the DotVVM application, or if the API project can share these types with the DotVVM application using a class library, you can register these types as **known types**. In this case, they won't be included in the generated client and you need to make sure both DotVVM and API can see these types with the same name and namespace.

To register known types, configure the DotVVM integration like this:

```CSHARP
// OWIN
c.EnableDotvvmIntegration(opt => 
{
    // add a single type
    opt.AddKnownType(typeof(Order));

    // add all types from the assembly
    opt.AddKnownAssembly(typeof(Order).Assembly);

    // add all types from the namespace
    opt.AddKnownNamespace(typeof(Order).Namespace);
});

// ASP.NET Core
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

