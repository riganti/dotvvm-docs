## Globalization And Cultures

When DotVVM serializes the viewmodel, it includes an information about the current thread culture which was used to process the request.

If you use any control which works with numeric or date values (e.g. [Literal](/docs/controls/builtin/Literal/{branch}) with its `FormatString` property), 
the page needs to know which culture should be used in order to apply the correct format.

### Default Culture

In the [DotVVM configuration](/docs/tutorials/basics-configuration/{branch}), you can specify the default culture which is used for all requests. The best way 
is to set this value in the `DotvvmStartup.cs` file using the following code:

```CSHARP
config.DefaultCulture = "en-US";
```

### Switching Cultures

> The way how cultures are switched was changed in [DotVVM 2.0](/docs/tutorials/basics-globalization/2-0) to handle asynchronous methods correctly. 

If your website supports multiple languages and cultures, you need to store the language the user has selected somewhere. 
Whichever method you use (cookies, URL, database...), you need to tell DotVVM at the beginning of the request processing, which culture is used for the particular
HTTP request.

This can be done by calling the `Context.ChangeCurrentCulture` method. In simple scenarios, you can call it in the `Init` method in the viewmodel. 
In larger applications you'll probably create an [action filter](/docs/tutorials/advanced-action-filters/{branch}) for that.

```CSHARP
public override Task Init()
{
    var lang = Context.Query["lang"];
    if (lang == "cs")
    {
        Context.ChangeCurrentCulture("cs-CZ");
    }
    else
    {
        Context.ChangeCurrentCulture("en-US");
    }
    return base.Init();
}
```

> The `Context.ChangeCurrentCulture` method is removed in [DotVVM 2.0](/docs/tutorials/basics-globalization/2-0) and replaced by a localization presenter mechanism.