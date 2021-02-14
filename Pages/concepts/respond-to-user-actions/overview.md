# Optimizing PostBacks

Most DotVVM applications have been using [Command Binding](/docs/tutorials/basics-command-binding/{branch}) for all actions the user can invoke from the page. This type of binding causes a postback and by default, the entire viewmodel is transferred from the client to the server so the command can be processed. All changes made to the viewmodel on the server are serialized and sent back to the client.

In reasonably-sized pages, it is not a big issue, but sometimes, a large amount of data needs to be transferred on every button click. For example, if the page contains a [GridView](/docs/controls/builtin/GridView/{branch}) with many of rows, the viewmodel can have tens or hundreds of kilobytes, which is a problem.

DotVVM offers many solutions to address this issue. We are also working on a diagnostics window for [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio-professional-extension) which will show, how much data is transferred on each postback and highlight requests with potential issues.

Here is a list of tips which can help you to optimize your DotVVM pages:

## 1. Remove unnecessary data from the viewmodel

While working with our customers, we have seen many viewmodels containing data that weren't necessary for the view. The typical cause of this issue is reusing objects already present in the application instead of creating a separate ones which would contains only the necessary information.

For example, if you need to display a table of employees where only few columns are needed, create a special class that represents these rows and use it in the viewmodel instead of the original class which may have more properties. These special classes are sometimes called [Data Transfer Objects](https://en.wikipedia.org/wiki/Data_transfer_object). 

## 2. Use Bind attribute 

Some properties in the viewmodel never change, or can be changed only on the server. Specifying the [Binding Direction](/docs/tutorials/basics-binding-direction/{branch}) for these properties can help to significantly reduce the amount of data being transferred. 

```CSHARP
public class CurrencyExchangeViewModel : DotvvmViewModelBase
{

    ...

    // This property is used as a DataSource for several ComboBox controls. The list of currencies never changes so there is no need to transfer them unles this is the first page load (HTTP GET).
    [Bind(Direction.ServerToClientFirstRequest)]
    public List<CurrencyDTO> Currencies => currenciesService.GetAll();

    // This property cannot be changed by the user, it is only set on the server side. There is no need to transfer it from the client to the server.
    [Bind(Direction.ServerToClient)]
    public string ErrorMessage { get; set; }

    ...
 
}
```

## 3. Static Command 

Sometimes, you just need to call a method on the server and update a single property in the viewmodel. In this case, you can use [Static Command Binding](/docs/tutorials/basics-static-command-binding/{branch}) which doesn't need to transfer the viewmodel at all. It just sends the method parameters to the server and optionally assigns the return value of the method to a property in the viewmodel. 

By default, the method must be static.

```DOTHTML
<dot:Button Text="Refresh Grid" Click="{staticCommand: Items = PageViewModel.LoadItems()}" />
```

**DotVVM 2.0** introduces a new feature called [Static Command Services](/docs/tutorials/basics-static-command-services/{branch}). It allows the static commands to can call even non-static methods, which can take advantage of dependency injection.
 
```DOTHTML
@service _itemService = MyApp.Services.ItemsService

<dot:Button Text="Refresh Grid" Click="{staticCommand: Items = _itemService.LoadItems()}" />
```

Static commands can be also used to perform local changes in the viewmodel without communicating with the server:

```DOTHTML
<dot:Button Text="Toggle State" Click="{staticCommand: State = !State}" />
```

You can perform multiple statements in the static command. Use `;` to separate these commands.

## 4. REST API Bindings

Another way of ligheting the viewmodels is to use [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/{branch}). This allows to register a REST API as a variable in binding expressions (e.g. `_ordersApi`) and call REST methods on it. The results of the API calls can be used as values for many controls ([GridView](/docs/controls/builtin/GridView/{branch}) for example) and doesn't need to be present in the viewmodel at all.

Using this approach, you can make a page with a grid of data which will be loaded using REST API. The viewmodel can then contain only the state of the page (current page index, current order and so on), not the data displayed in the page. This can make a significant change in the size of the viewmodel.

## Future Enhancements

There are a lot of other ways we plan to include in future versions of DotVVM. 

For example, we are working on a mechanism that will allow to translate some methods in the viewmodels (those which only make changes to the viewmodel and do not have any dependencies on server-side things like SQL database) in JavaScript. This will allow to call an instance method on a viewmodel without an actual communication with the server.
