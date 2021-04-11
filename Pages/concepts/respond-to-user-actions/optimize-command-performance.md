# Optimize command performance

As mentioned in the [Respond to user actions](overview) chapter, the commands come with some overhead which can make some pages responding slowly. If you run into performance issues with commands, here are some tips on how to make the page faster. 

## Remove unnecessary data from the viewmodel

When working with our customers, we have seen many viewmodels containing data that weren't actually necessary for the view. The typical cause of this issue is reusing objects already present in the application, instead of creating separate ones which would contain only the necessary information.

For example, if you need to display a table of employees where only few columns are needed, create a special class that represents these rows, and use it in the viewmodel instead of using the original class with all properties of the employee. These special classes are sometimes called [Data Transfer Objects](https://en.wikipedia.org/wiki/Data_transfer_object). 

```CSHARP
public class EmployeeViewModel
{
    // The Employee class has ~40 properties with all information about the employee.
    // Instead, we are using EmployeeSimpleDto which contains only the properties which are used in the page.
    public List<EmployeeListDto> Employees { get; set; }

    public override Task PreRender() 
    {
        Employees = _employeeService.GetEmployeeList();
        base.PreRender();
    }
}

public class EmployeeService 
{
    public List<EmployeeListDto> GetEmployeeList() 
    {
        return _dbContext.Employees
            .Select(e => new EmployeeListDto() 
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.Department.Name,
                CreatedDate = e.CreatedDate
            })
            .ToList();

            // alternatively, you can use the AutoMapper library to generate projections instead of writing Select statements
            // return _dbContext.Employees
            //     .ProjectTo<EmployeeListDto>()
            //     .ToList();
    }
}
```

## Use static commands

Sometimes, you just need to call a method on the server to update a single property in the viewmodel. In this case, you can use a [static command](static-commands) which doesn't transfer the viewmodel at all. It just sends the method arguments to the server, and can assign the return value to some property in the viewmodel. 

You can either call static methods, or methods defined in [static command services](static-command-services) that are imported using the `@service` directive.

```DOTHTML
<!-- calling a static method -->
<dot:Button Text="Refresh Grid" Click="{staticCommand: Items = PageViewModel.LoadItems()}" />
```
 
```DOTHTML
@service _itemService = MyApp.Services.ItemsService

<dot:Button Text="Refresh Grid" Click="{staticCommand: Items = _itemService.LoadItems()}" />
```

Static commands can be make local changes in the viewmodel without talking to the server:

```DOTHTML
<dot:Button Text="Toggle State" Click="{staticCommand: State = !State}" />
```

You can use the `;` operator to have multiple statements in static commands. You can even declare variables or call LINQ methods. See [supported expressions in bindings](~/pages/concepts/data-binding/supported-expressions) for more details on what can be used in static commands.

## Enable server-side viewmode cache

DotVVM 2.4 added a new experimental feature called [server-side viewmodel cache](~/pages/concepts/viewmodels/server-side-viewmodel-cache). This feature caches the viewmodels on the server, so the client can send only the differences between the local viewmodel and the original copy stored on the server.

This can save a lot of data transferred in exchange for a bit of server memory. Many operations don't change the viewmodel at all (e. g. removing a row from a grid), so the savings made thanks to this feature can be more than 90%. 

See the [server-side viewmodel cache](~/pages/concepts/viewmodels/server-side-viewmodel-cache) chapter for more info.

## Use Bind attribute 

Some properties in the viewmodel never change, or can be changed only on the server. Specifying the [Binding direction](~/pages/concepts/viewmodels/binding-direction) for these properties can help to significantly reduce the amount of data transferred. 

```CSHARP
public class CurrencyExchangeViewModel : DotvvmViewModelBase
{

    // This property is used as a DataSource for several ComboBox controls. 
    // However, the list of currencies never changes so there is no need to transfer them in subsequent requests.
    [Bind(Direction.ServerToClientFirstRequest)]
    public List<CurrencyDTO> Currencies => currenciesService.GetAll();

    // This property cannot be changed by the user, it is only set on the server. There is no need send it to the server on postbacks.
    [Bind(Direction.ServerToClient)]
    public string ErrorMessage { get; set; }

}
```

## Use REST API bindings

Another way of simplifying the viewmodels is to use [REST API bindings](rest-api-bindings/overview). This features allows to register a REST API as a variable in binding expressions (e.g. `_ordersApi`), and then invoke REST methods on it. The results of the API calls can be used as values for many controls ([GridView](~/controls/builtin/GridView) for example), and don't need to be present in the viewmodel at all.

Using this approach, you can make a page with a grid of data which will be loaded using REST API. The viewmodel can then contain only the state of the page (current page index, current order and so on), not the data displayed in the page. This can make a significant change in the size of the viewmodel.

Also, if you use HTTP GET requests for obtaining the data, you can take advantage of the caching on the HTTP level. For example, loading the lists of countries or currencies into the [ComboBox](~/controls/builtin/ComboBox) can be done by a REST API calls which can be safely cached as they don't change frequently.

## See also

* [Commands](commands)
* [Static commands](static-commands)
* [Server-side viewmodel cache](~/pages/concepts/viewmodels/server-side-viewmodel-cache)
* [REST API bindings](rest-api-bindings/overview)