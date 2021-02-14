# Upgrading to DotVVM 2.0

DotVVM 2.0 brings several new features including:

* [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/2.0)
* [Static Command Services](/docs/tutorials/basics-static-command-services/2.0)
* [PostBack Concurrency Modes](/docs/tutorials/basics-postback-concurrency-mode/2.0)
* [FormControls.Enabled Property](/docs/tutorials/basics-control-properties-and-attributes/2.0)
* [_collection Context Variable](/docs/tutorials/basics-binding-context/2.0)

<iframe width="560" height="315" src="https://www.youtube.com/embed/LiTajcb6tug" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>


There have been several breaking changes between **DotVVM 1.1** and **DotVVM 2.0**. Perform the following steps to do the upgrade:

## 1. Upgrade DotVVM NuGet packages

Every DotVVM application uses either `DotVVM.AspNetCore` or `DotVVM.Owin` NuGet package. These packages depend on `DotVVM` and `DotVVM.Core` packages.
All of these packages must be upgraded to their `2.x` versions. 

Run the following command in the __Package Manager Console__ window, or click __Manage NuGet Packages__ item in the context menu of the project and perform the upgrade in UI.

```
# for ASP.NET Core
Update-Package DotVVM.AspNetCore

# for OWIN
Update-Package DotVVM.Owin
```



## 2. Move registration of DotVVM-related services to DotvvmStartup

Because of changes in __DotVVM Compiler__ (a tool which provides metadata for Visual Studio IntelliSense), we had to move registration of DotVVM-related services into `DotvvmStartup` (or other class that implements `IDotvvmServiceConfigurator` interface).

In `Startup.cs` file, remove the lambda method in `UseDotVVM` call:

```CSHARP
// OWIN
// ==========================================================

// DotVVM 1.1
var config = app.UseDotVVM<DotvvmStartup>(ApplicationPhysicalPath, options => 
{
    // copy the body of the lambda and remove it
    options.AddDefaultTempStorages("Temp");
});

// DotVVM 2.0
var config = app.UseDotVVM<DotvvmStartup>(ApplicationPhysicalPath);
```

```CSHARP
// ASP.NET Core
// ==========================================================

// DotVVM 1.1
var config = app.UseDotVVM<DotvvmStartup>(options => 
{
    // copy the body of the lambda and remove it
    options.AddDefaultTempStorages("Temp");
});

// DotVVM 2.0
var config = app.UseDotVVM<DotvvmStartup>();
```

Add `ConfigureServices` method in `DotvvmStartup.cs` and place contents of the lambda inside:

```CSHARP
public class DotvvmStartup : IDotvvmStartup
{
    ...

    public void ConfigureServices(IDotvvmServiceCollection options)
    {
        // paste the body of the lambda here
        options.AddDefaultTempStorages("Temp");
    }

}
```

And last, make the `DotvvmStartup.cs` class implement `IDotvvmServiceConfigurator`:

```CSHARP
public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
{
    ...
}
```

> This `ConfigureServices` should register only services that are related to DotVVM - uploaded file storage, custom viewmodel loaders, or commercial controls like [DotVVM Business Pack](/docs/tutorials/commercial-business-pack-install/{branch}). All other services should be configured in `Startup.cs` like it was before.



## 3. Add jQuery resource if you need it

**DotVVM 1.1** was including jQuery in the page in Debug mode, because it was required by `dotvvm.debug.js` helper library. The need for jQuery in this helper was removed with **DotVVM 2.0**, so `jQuery` resource is not registered in the DotVVM configuration.

If you application uses jQuery and if it is not included with another library (like [Bootstrap for DotVVM](/docs/tutorials/commercial-bootstrap-for-dotvvm/{branch}) or [DotVVM Business Pack](/docs/tutorials/commercial-business-pack-install/{branch})), add the following code into `ConfigureResources` method in `DotvvmStartup.cs`:

```CSHARP
config.Resources.Register("jquery", new ScriptResource()
{
    // use relative URL if you ship jQuery with your application
    Location = new UrlResourceLocation("https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js")
});
```



## 4. Route Registration with Custom Presenters

We have changed the signature of `RouteTable.Add` method for registering [custom presenters](/docs/tutorials/advanced-custom-presenters/{branch}). Now you can specify only a type of the presenter - in this case, the presenter instance will be retrieved from `IServiceProvider`. 

An unnecessary parameter specifying the virtual path has been removed the overloads with presenters:

```CSHARP
// DotVVM 1.1
config.RouteTable.Add("Rss", "export/rss", null, () => new RssPresenter());

// DotVVM 2.0
config.RouteTable.Add("Rss", "export/rss", typeof(RssPresenter));
```



<a name="action-filters"></a>

## 5. Action Filters

If you have implemented custom action or exception filters and used `OnPageLoadingAsync` or `OnPageLoadedAsync` methods, they were removed and the behavior was changed.

These methods were originally called for DotVVM pages and also custom presenters, and because of a bug in the pipeline, they were called twice for DotVVM pages. 

We have split these methods to **presenter-level** and **page-level** events:

+ **Presenter-level Events** (applicable to DotVVM pages and custom presenters)
    - `OnPresenterExecutingAsync` is executed immediately after the URL is mapped to a specific route and presenter is resolved. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.
    - `OnPresenterExecutedAsync` is executed immediately after the presenter completes processing of the request. This method is called also for DotVVM pages as they are handled by `DotvvmPresenter` class.

+ **Page-level Events** (applicable to DotVVM pages)
    - `OnPageInitializedAsync` is executed after the page control tree is built and viewmodel instance is initialized.
    - `OnPageRenderedAsync` is executed after the response is rendered completely and before the viewmodel instance is disposed.

If you need to handle these events for both DotVVM pages and custom presenters, override the **presenter-level methods**.

If you only need to handle these events for DotVVM pages, override the **page-level methods**.

### 5.1 Exception Filters

There is a new method `OnPresenterExceptionAsync` which is called when an unhandled exception is thrown from a custom presenter or a DotVVM page (which is processed using `DotvvmPresenter`).

If you have a custom exception filter and need to handle exceptions from presenters, you may want to override this exception to log the errors. 



<a name="postback-handlers"></a>

## 6. Custom PostBack Handlers 

We have rearchitected the way how [custom postback handlers](/docs/tutorials/control-development-creating-custom-postback-handlers/{branch}) are implemented. 

If you implemented your own postback handlers, you will need to make the following change in the C# part of the handler:

```CSHARP
...
protected override Dictionary<string, object> GetHandlerOptions()
{
    return new Dictionary<string, object>()
    {
        ["message"] = GetValueRaw(MessageProperty)      // TranslateValueOrBinding was removed - use GetValueRaw
    };
}
...
```

Then, update the JavaScript implementation of the postback handler so the `execute` method returns `Promise`:

```JAVASCRIPT
dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["confirm"] = function ConfirmPostBackHandler(options) {

        var message = options.message; // you'll get the parameters passed to the handler in the options object
        
        return {
                execute: function(callback) {
                    return new Promise(function (resolve, reject) {
                        // do whatever you need and if you need to do the postback, invoke the 'callback()' function
                        if (confirm(message)) {
                            // call next postback handler
                            callback().then(resolve, reject);
                        } else {
                            // signalize that the postback was cancelled
                            reject({type: "handler", handler: this, message: "The postback was aborted by user."});
                        }
                    });
                },

                // optional settings
                after: ["xxx"],        // you can specify that this handler should be launched after some other handler
                before: ["xxx"]        // you can specify that this handler should be launched before some other handler
            };
        };
    });
```

Also note that `dotvvm.postBackHandlers` collection was renamed to `dotvvm.postbackHandlers`. 



<a name="gridview"></a>

## 6. GridView Control 

We have changed the way collection data are loaded into GridViewDataSet. In DotVVM 1.1, you could provide delegate to `GridViewDataSet` which would be used to load data. We have removed the delegate because loading the data could lead to deadlock.

```CSHARP
public GridViewDataSet<Customer> Customers { get; set; }

public override Task Init()
{
    Customers = GridViewDataSet.Create(gridViewDataSetLoadDelegate: GetData, pageSize: 4);
    return base.Init();
}
```

In DotVVM 2.0, you have to manually load collection to `GridViewDataSet` when it's appropriate:

```CSHARP
public GridViewDataSet<Customer> Customers { get; set; } 
        = new GridViewDataSet<Customer>() { PagingOptions = { PageSize = 4 } };

public override Task PreRender()
{
    if (Customers.IsRefreshRequired)
    {
        var queryable = GetData();
        Customers.LoadFromQueryable(queryable);
    }
    return base.PreRender();
}
```



# Obsolete Constructs

There are several things in **DotVVM 2.0** which were marked as obsolete. Although the features still work, we recommend to fix them soon.

## 1. ValueType property on TextBox, Literal and GridViewTextColumn

The `ValueType` property was needed whenever you worked with date or numeric values in `TextBox`, `Literal` or `GridViewTextColumn` controls. In **DotVVM 2.0**, this property was made obsolete and is not used by the framework - the type of the data-bound value is inferred automatically.



## 2. ComboBox now supports ItemTextBinding and ItemValueBinding

The `DisplayMember` property was replaced by `ItemTextBinding`, the `ValueMember` was replaced with `ItemValueBinding`.

```DOTHTML
<!-- DotVVM 1.1 -->
<dot:ComboBox DataSource="{value: People}" 
              DisplayMember="FullName" 
              ValueMember="Id" 
              SelectedValue="{value: SelectedPerson}" />

<!-- DotVVM 2.0 -->
<dot:ComboBox DataSource="{value: People}" 
              ItemTextBinding="{value: FullName}" 
              ItemValueBinding="{value: Id}" 
              SelectedValue="{value: SelectedPerson}" />
```

## 3. DotvvmConfiguration now exposes IServiceProvider

The `ServiceLocator` property was replaced by `ServiceProvider` in the `DotvvmConfiguration`.
