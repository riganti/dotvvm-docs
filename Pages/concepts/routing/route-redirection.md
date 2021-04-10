# Route redirection

Over time, websites change and it might be necessary to alter some URLs as well. However, in many cases it is important to maintain compatibility with the old system. This can be achieved using redirections.

## Redirect to another route

During configuration, you can define redirections using the following code-snippets:

```CSHARP
config.RouteTable.AddRouteRedirection(
    name: "Redirection1", 
    urlPattern: "my/url/page/{Id}", 
    targetRouteNameProvider: context => "TargetPage",
    defaultValues: null,
    permanent: false,
    parametersProvider: null,
    urlSuffixProvider: null);
```

In the example above, we can see an addition of a new redirection record to the route table. This record is named *Redirection1* and specifies that any access to a page that matches the *my/page/url{Id}* pattern will be redirected to the route named *TargetPage*. This redirection is temporary (i.e. non-permanent).

+ The first argument specifies a **name of the redirection**. This name is not displayed anywhere, it is only a string which identifies the redirection within the framework.

+ The second argument is the **route URL pattern** that we are redirecting from. More information on routes and route parameters can accessed on [routing overview](~/pages/concepts/routing/overview).

+ The third argument is either a **target route name**, or a **target route name provider** (there are two overloads). The first overload is quite straight forward, as it always redirects to the route determined by the given `string`. The second overload expects a `Func<IDotvvmRequestContext, string>`, as can be seen in the code snippet above. This can be used to obtain context-based redirection targets.

+ The fourth argument (optional) specifies **default values for [route parameters](parameters)**.

+ The fifth argument (optional) specifies whether this redirection is **permanent**. The default value is `false`, which means non-permanent.

+ The sixth argument (optional) is a **parameters provider** of type `Func<IDotVVMRequestContex, Dictionary<string, object?>>`. This provider can be used to perform context-based transformations on parameters. An example that copies obtained parameters and alters the *Id* parameter by setting it with value *123* can be seen in the snippet below.

```CSHARP
    parametersProvider: context => {
        var newDict = new Dictionary<string, object>(context.Parameters);
        newDict["Id"] = 123;
        return newDict;
    });
```

+ The last argument (optional) is a **provider for URL suffix** (which consists of query string and fragment). The default value is `null` which means that the query string will be kept as is. If you want to transform it, specify your own function.

## Redirect to another URL

In case your website is split into multiple applications, or you need to redirect to a custom URL, you might find URL redirections quite useful. These redirections can be defined in the following way:

```CSHARP
config.RouteTable.AddUrlRedirection(
    name: "Redirect2", 
    urlPattern: "my/page/url{Id}", 
    targetUrlProvider: context => "https://www.dotvvm.com"
    defaultValues: null,
    permanent: true);
```

In the example above, we can see a new redirection addition to the route table. This record is named *Redirection2* and specifies that any access to a page that matches the *my/page/url{Id}* pattern will be redirected to *https://dotvvm.com*. This redirection is permanent.

+ The first argument specifies a **name of the redirection**. This name is not displayed anywhere, it is only a string which identifies the redirection within the route table.

+ The second argument is the **route URL pattern** that we are redirecting from. More information on routes and route parameters can accessed on [routing overview](~/pages/concepts/routing/overview).

+ The third argument is either a **target URL**, or a **target URL provider** (there are two overloads). The first overload is quite straight forward, as it always redirects to the URL determined by the given `string`. The second overload expects a `Func<IDotvvmRequestContext, string>`, as can be seen in the code snippet above. This can be used to obtain context-based redirection targets.

+ The fourth argument (optional) specifies **default values for [route parameters](parameters)**.

+ The fifth argument (optional) specifies whether this redirection is **permanent**. The default value is `false`, which means non-permanent.

## See also

* [Parameters](~/pages/concepts/routing/parameters)
* [Auto-discover routes](~/pages/concepts/routing/auto-discover-routes)
* [Custom presenter](~/pages/concepts/routing/custom-presenters)