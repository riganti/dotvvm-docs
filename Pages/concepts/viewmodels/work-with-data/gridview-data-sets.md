# GridView data sets

If you use [GridView](~/controls/builtin/GridView) or [Repeater](~/controls/builtin/Repeater), one of the common requirements is to allow users to sort the collection or split large tables into smaller pages.

DotVVM offers `GridViewDataSet<T>` and `GenericGridViewDataSet<T, ...>` classes which can help with filtering, sorting, paging, row insertion, and row editing.

## Use the default data set

To get started, use the `GridViewDataSet<T>`. It contains:

* `Items` - the `List<T>` of items that are currently visible to the user
* `SortingOptions` - sorting columns name and direction
* `PagingOptions` - the page-index based options used by [DataPager](~/controls/builtin/DataPager)
* `RowEditOptions` - the ID of currently edited row
* `IsRefreshRequired` - a "dirty flag" indicating that the data should be loaded again (i.e. after page change)

```CSHARP
public class CustomerListViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerListDto> Customers { get; set; } = new()
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

In the page, you can bind the data set to the control's `DataSource` property. Also, you can add the `DataPager` control, and bind it to the same object.

```DOTHTML
<dot:GridView DataSource="{value: Customers}">
    <Columns>
        <dot:GridViewTextColumn HeaderText="Last name"
                                ValueBinding={value: LastName}
                                AllowSorting />
    </Columns>
</dot:GridView>

<dot:DataPager DataSet="{value: Customers}" />
```

## Load data from IQueryable

Most developers use `IQueryable`-based ORMs such as [Entity Framework](https://docs.microsoft.com/en-us/ef/) or [Marten](https://martendb.io/).

DotVVM provides `LoadFromQueryable` and `LoadFromQueryableAsync` functions which loads data from `IQueryable` after applying the data set filtering, sorting, and paging options.

```CSHARP
public class CustomerListViewModel : DotvvmViewModelBase
{
    public GridViewDataSet<CustomerListDto> Customers = ...;

    public override async Task PreRender()
    {
        if (Customers.IsRefreshRequired)
        {
            IQueryable<CustomerListDto> queryable = _customerService.GetCustomerList();
            await Customers.LoadFromQueryableAsync(queryable, Context.RequestAborted);
        }
        await base.PreRender();
    }
}
```

Notice that the `GetCustomerList` method doesn't get any information about the column we use for sorting, or how many records we want. The `LoadFromQueryable` method will append `OrderBy`, `Skip` and `Take` calls on the `IQueryable` before it calls `ToList`, so only the rows that are actually needed will be fetched from the database.

> Please note that some `IQueryable` providers cannot perform paging when the sort expression is not set. For example, if you try to use `PagingOptions` without `SortingOptions` with Entity Framework or Entity Framework Core, you'll get `System.NotSupportedException: The method 'Skip' is only supported for sorted input in LINQ to Entities. The method 'OrderBy' must be called before the method 'Skip'.` In such case, you need to set the `SortingOptions.SortExpression` property to a name of the property that shall be used for sorting. 

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
```

## Use custom option types

DotVVM 5.0 splits data set behavior into composable option types.
Use the `GenericGridViewDataSet<T, ...>` when the defaults don't fit your needs.

DotVVM comes with few additional option types:

* `MultiCriteriaSortingOptions`
* `NextTokenPagingOptions`
* `NextTokenHistoryPagingOptions`

Additionally, all options have a no-op version, i.e. `NoPagingOptions` will disable paging support on the dataset.

For example, APIs that page using a continuation token can use token paging options:

```CSHARP
public GenericGridViewDataSet<IssueDto,
    NoFilteringOptions,
    SortingOptions,
    NextTokenHistoryPagingOptions,
    NoRowInsertOptions,
    RowEditOptions> Issues { get; set; } = new(new(), new(), new(), new(), new());
```

If you have other needs, you can define your own dataset options.
For instance, we can implement enum-based sorting options, allowing users to sort only by price, popularity or ID:

```CSHARP
public class RestrictedSortingOptions : ISortingOptions, ISortingStateCapability, IApplyToQueryable
{
    public bool Descending { get; set; }
    public SortProperty Property { get; set; }

    public enum SortProperty { Id, Price, Popularity }

    // ISortingStateCapability enables sort display in GridView
    public IEnumerable<SortCriterion> Criteria => [
        new SortCriterion { SortDescending = Descending, SortExpression = Property.ToString() }
    ];

    public bool IsColumnSortedAscending(string? sortExpression) => !Descending && sortExpression == Property.ToString();
    public bool IsColumnSortedDescending(string? sortExpression) => Descending && sortExpression == Property.ToString();


    // IApplyToQueryable enables LoadFromQueryable(Async)
    public IQueryable<T> ApplyToQueryable<T>(IQueryable<T> queryable)
    {
        if (!typeof(MyItemBase).IsAssignableFrom(typeof(T)))
            throw new Exception("Can be only used with MyItemBase");

        var q = (IQueryable<MyItemBase>)queryable;
        q = (Property, Descending) switch {
            (SortProperty.Id, false) => q.OrderBy(i => i.Id),
            (SortProperty.Id, true) => q.OrderByDescending(i => i.Id),
            (SortProperty.Popularity, false) => q.OrderBy(i => i.Popularity),
            (SortProperty.Popularity, true) => q.OrderByDescending(i => i.Popularity),
            (SortProperty.Price, false) => q.OrderBy(i => i.Price),
            (SortProperty.Price, true) => q.OrderByDescending(i => i.Price),
        };
        return (IQueryable<T>)q;
    }
}
```

DotVVM.Framework does not currently provide implementation of FilteringOptions.
A general implementation is available in BusinessPack, and filters for specific DTOs can be easily implemented.

## Load data with static commands

`GridView`, `DataPager`, and `AppendableDataPager` can load data using a static command.
This avoids sending the entire page viewmodel to the server just to change the current page or sort order.

The static command accepts `GridViewDataSetOptions<TFilteringOptions, TSortingOptions, TPagingOptions>` and returns `GridViewDataSetResult<TItem, TFilteringOptions, TSortingOptions, TPagingOptions>`.

```CSHARP
[AllowStaticCommand]
public static async Task<GridViewDataSetResult<CustomerListDto, NoFilteringOptions, SortingOptions, PagingOptions>>
    LoadCustomers(GridViewDataSetOptions options)
{
    var dataSet = new GridViewDataSet<CustomerListDto>();
    dataSet.ApplyOptions(options);

    await dataSet.LoadFromQueryableAsync(CustomerRepository.GetCustomerList());

    return new(dataSet.Items.ToList(), dataSet.GetOptions());
}
```

We can bind the same loader to both GridView and the DataPager:

```DOTHTML
<dot:GridView DataSource={value: Customers}
              LoadData={staticCommand: RootViewModel.LoadCustomers}>
    <dot:GridViewTextColumn HeaderText="Name"
                            ValueBinding={value: Name}
                            AllowSorting />
</dot:GridView>

<dot:DataPager DataSet={value: Customers}
               LoadData={staticCommand: RootViewModel.LoadCustomers} />
```

Alternatively to `DataPager` use the `ApendableDataPager` for “load more” or infinite-scroll patterns.

```DOTHTML
<dot:AppendableDataPager DataSet={value: Customers}
                         LoadData={staticCommand: RootViewModel.LoadCustomers}>
    <LoadTemplate>
        <dot:Button Text="Load more" Click={staticCommand: _dataPager.Load()} />
    </LoadTemplate>
</dot:AppendableDataPager>
```

## Refresh the data set

The data sets are commonly loaded in the `PreRender` [viewmodel lifecycle](../overview) method because it runs after methods invoked from [commands](~/pages/concepts/respond-to-user-actions/commands).

In many cases, the data in the data set stay unchanged, so it doesn't need to be loaded on every postback. That's why we check the `IsRefreshRequired` property - it is be `true` on the first request, but once the data is loaded, it is be reset to `false` (`LoadFromQueryable` resets this property automatically).

If the user changes the sort order in the `GridView` by clicking on a column header, or selects a different page in the `DataPager`, the `IsRefreshRequired` will be set to `true`. You should also call `RequestRefresh` method on the data set if your code changes any of the dataset options.

```DOTHTML
<div class="filters">
    <dot:TextBox Text={value: SearchText} placeholder="Search customers" />
    <dot:Button Text="Search" Click={command: Customers.RequestRefresh()} />
</div>
```

## See also

* [Viewmodels overview](../overview)
* [Viewmodel best practices](best-practices)
* [GridView](~/controls/builtin/GridView)
* [Repeater](~/controls/builtin/Repeater)
* [DataPager](~/controls/builtin/DataPager)
* [Static commands](~/pages/concepts/respond-to-user-actions/static-commands)
