# GridView data sets

If you use [GridView](~/controls/builtin/GridView) or [Repeater](~/controls/builtin/Repeater), one of the common requirements is to allow the users to sort the collection, or to split large tables into smaller pages.

DotVVM offers a class called `GridViewDataSet<T>` which can help with sorting, paging, and single-row editing. This class contains the following properties:

* `Items` is a `List<T>` of items that are currently visible to the user
* `SortingOptions` property contains the metadata of how the data is sorted (column name, and direction)
* `PagingOptions` property provides information for the [DataPager](~/controls/builtin/DataPager) component, like page size, current page index, or the total number of rows in the table

## Use the data set

The data set object can be defined like this:

```CSHARP
public class CustomerListViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerListDto> Customers = new GridViewDataSet<CustomerListDto>()
    {
        PagingOptions = 
        {
            PageSize = 20
        },
        SortingOptions = 
        {
            SortExpression = nameof(CustomerListDto.LastName)
        }
    };
    ...
}
```

In the page, you can just bind the data set to the control's `DataSource` property. Also, you can add the `DataPager` control, and bind it to the same object.

```DOTHTML
<dot:GridView DataSource="{value: Customers}">
    <Columns>
    ...
    </Columns>
</dot:GridView>

<dot:DataPager DataSet="{value: Customers}" />
```

## Load data from IQueryable

Most developers prefer using ORMs, such as [Entity Framework](https://docs.microsoft.com/en-us/ef/) which work with the `IQueryable` interface. 

DotVVM data sets make it very easy to load data from `IQueryable` providers:

```CSHARP
public class CustomerListViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerListDto> Customers = ...;

    public override PreRender()
    {
        if (Customers.IsRefreshRequired) 
        {
            IQueryable<CustomerListDto> queryable = _customerService.GetCustomerList();
            Customers.LoadFromQueryable(queryable);
        }
        base.PreRender();
    }
}
```

Notice that the `GetCustomerList` method doesn't get any information about the column we use for sorting, or how many records we want. The `LoadFromQueryable` method will append `OrderBy`, `Skip` and `Take` calls on the `IQueryable` before it calls `ToList`, so only the rows that are actually needed will be fetched from the database.

## Load data manually

If you don't use `IQueryable`, you can load data on your own by assigning the `Items` and `PagingOptions.TotalItemsCount` properties on the data set.

You can read all information about the current page and sort preferences from the `PagingOptions` and `SortingOptions` properties.

```CSHARP
public class CustomerListViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerListDto> Customers = ...;

    public override PreRender()
    {
        if (Customers.IsRefreshRequired) 
        {
            int totalRowCount;
            Customers.Items = _customerService.GetCustomerList(
                pageIndex: Customers.PagingOptions.PageIndex,
                pageSize: Customers.PagingOptions.PageSize,
                sortExpression: Customers.SortingOptions.SortExpression,
                sortDescending: Customers.SortingOptions.SortDescending,
                out totalRowCount
            );
            Customers.PagingOptions.TotalItemsCount = totalRowCount;
            Customers.IsRefreshRequired = false;
        }
        base.PreRender();
    }
}
```

## Refresh the data set

The data sets are commonly loaded in the `PreRender` [viewmodel lifecycle](../overview) method, because it happens after methods invoked from [commands](~/pages/concepts/respond-to-user-actions/commands) are executed.

In many cases, the data in the data set stay unchanged, so it doesn't need to be loaded on every postback. That's why we check the `IsRefreshRequired` property - it is be `true` on the first request, but once the data is loaded, it is be reset to `false` (`LoadFromQueryable` resets this property automatically).

If the user changes the sort order in the `GridView` by clicking on a column header, or selects a different page in the `DataPager`, the `IsRefreshRequired` will be set to `true`. You can also call `RequestRefresh` method on the data set - it is quite useful if you want to create e. g. a refresh button.

```DOTHTML
<div class="filters">
    <dot:TextBox Text="{value: SearchText}" placeholder="Search customers" />
    <dot:Button Text="Search" Click="{value: Customers.RequestRefresh()}" />
</div>
```

## See also

* [Viewmodels overview](../overview)
* [Viewmodel best practices](best-practices)
* [GridView](~/controls/builtin/GridView)
* [Repeater](~/controls/builtin/Repeater)
* [DataPager](~/controls/builtin/DataPager)