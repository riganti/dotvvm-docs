# Best practices

The design and structure of viewmodels is crucial in DotVVM application. We recommend to focus on making the viewmodels easy to read, well-structured, and concise. This can prevent many problems, and help the application to be maintainable in the long-term.

## Separate properties with different semantics

The properties in viewmodel and all child objects should have plain `{ get; set; }`. There should be no logic in getters, setters, or in the constructors of the classes. The getters and setters are invoked by the serializer, and you never know in which order they will be invoked.

We recommend to distinguish between three types of information, and keep them separated. 

Imagine you are creating a page where you can edit a customer. Some users can actually edit the values, others can only view the customer details. When you edit the shipping address of the customer, you should be able to select a country from a list (which is maintained on a different page).

Here is how the viewmodel for such page will look like:

```CSHARP
public class CustomerDetailViewModel : DotvvmViewModelBase
{

    public CustomerDetailDto Customer { get; set; }
    
    [Protect(ProtectMode.SignData)]
    public IsEditable { get; set; }

    [Bind(Direction.ServerToClientFirstRequest)]
    public List<CountryDto> Countries => ...;

    ...
}

public class CustomerDetailDto 
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    ...
    public int CountryId { get; set; }
    ...
}
```

### Main data

The `Customer` property represents an object with the main entity that is subject to editing. The main data can be changed, and are typically stored in a database or in another persistent storage.

Notice that the `Id`, `FirstName` or `CountryId` properties are not declared directly in the viewmodel, but they reside in an object called `CustomerDetailDto`. The `Dto` suffix stands for [Data Transfer Objects](https://en.wikipedia.org/wiki/Data_transfer_object) - a plain object which just holds the data. Notice that it is not an entity class from Entity Framework (which we may be using in the application, but it will probably look different and contain more properties than we need in this page).

The point of extracting these properties in a separate DTO class, is that it is so simple to pass such object to other places where the business logic lives. It can be even in a different project:

```CSHARP
public class CustomerDetailViewModel : DotvvmViewModelBase
{
    ...

    public void Save() 
    {
        // we can pass the main data object to a service which handles the business logic
        customerService.Save(Customer);

        Context.RedirectToRoute("CustomerList");
    }
```

We recommend to separate the main data of the page to a separate DTO, because it is much easier to pass the data to other places of the application.

### State of the page

We have the `IsEditable` property in the viewmodel. It indicates whether the user is allowed to edit the customer details, or if they are just read-only. 

The property is not included in the `CustomerDetailDto` class, because it is not part of the customer information. The intent of this property is only to  control whether some controls are visible. It is not stored anywhere, and it will go away when the user leaves the page.

Notice that the property is [protected](../viewmodel-protection) so it cannot be changed by the user. This is not a replacement of a permission check that should be done in the business layer, but it helps to guarantee that an attacker cannot tamper with this value.

### Environment data

The page also needs a list of countries so the user can select a country when editing an address. We may want to edit countries on some other page in the application, but not here.

Therefore, the list of countries should not be a part of the `CustomerDetailDto` class, because it is not a part of the information about the particular customer. It just provides some ambient information which is read-only on this page.

We've made a separate property for the list of countries, and set the [bind direction](../binding-direction) to `ServerToClientFirstRequest`. The list of countries is not changed frequently, and we don't need to transfer it on postbacks - we just need to send it to the client on the first HTTP GET request so the `ComboBox` can be populated.

The `CustomerDetailDto` contains only the `CountryId` property which indicates the ID of the selected country. 

## Avoid business logic in the viewmodel

Remember that the viewmodels are part of the presentation layer. They shouldn't communicate with the database, send e-mails, or launch rockets to the Universe directly. 

In general, the viewmodel methods should only gather data from the viewmodel, and call some method from the business layer to do the real job. After the business layer performs the action, it should update the viewmodel with the results. 

A common example of placing the business logic in the presentation layer is injecting the `DbContext` in the viewmodel. Manipulation with the database should be definitely in another layer of the application. 

On the other hand, the viewmodels can contain presentation logic - e. g. formatting values, or transforming data to be displayed to the user in a nicer way. For example, if you build a page with a large calendar, the business layer may give you only a flat list of appointments, and it is the UI concern to present the appointments in a two-dimensional table. Still, we don't recommend to place this complex logic directly in the viewmodel - but it should be somewhere in the presentation layer.

## Avoid using Entity Framework entities in the viewmodel

Don't use Entity Framework (or other ORM) entities directly in the viewmodels. Remember that the viewmodel is serialized in JSON and sent to the client, so anyone will see all values in the entities. Many of them can be sensitive (for example, the `PasswordHash` column of the `AspNetUser` entity), or it's just not wise to expose them if you don't have to.

Even if the data is not sensitive, you often just need to display several fields, and there is no reason to transfer the entire entity to the client - it makes the application slower.

Also, you may end up with errors because of the lazy loading, which might "expand" the entities and cause big data transfers, or fail on cyclic references which are not supported in JSON.

If you are using Entity Framework (which we recommend), create [Data Transfer Objects](https://en.wikipedia.org/wiki/Data_transfer_object), and use them in your viewmodel instead. You can use libraries like [AutoMapper](http://automapper.org/) to make the mapping between entities and DTOs really easy.

```CSHARP
public class EmployeeViewModel
{
    // The Employee class has ~40 properties with all information about the employee.
    // Instead, we are using EmployeeListDto which contains only the properties which are used in the page.
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

## Use nested viewmodels

Viewmodels can contain child viewmodels, which is a concept we strongly recommend.

If your page has two logical parts (e. g. sign in and sign up form on the same page), we recommend to create two viewmodels, and use them as properties in the page viewmodel. Separating the viewmodels will make them easier to read and maintain, and if you separate the two pages eventually, it will be very easy.

```CSHARP
public class PageViewModel : DotvvmViewModelBase
{

    public SignInViewModel SignIn { get; set; }

    public SignUpViewModel SignUp { get; set; }

    public PageViewModel(AccountService accountService)
    {
        SignIn = new SignInViewModel(accountService);
        SignUp = new SignUpViewModel(accountService);
    }

}
```

Similarly, if your page displays a modal dialog, it is a good idea to create a separate viewmodel for the dialog. The dialog can also send signals to the containing page, e. g. using events:

```CSHARP
public class OrderListViewModel : DotvvmViewModelBase
{
    public OrderDialogViewModel OrderDialog { get; set;}

    public OrderListViewModel() 
    {
        OrderDialog = new OrderDialogViewModel();
        OrderDialog.OrderCreated += RefreshList();
    }

    public override Task PreRender() 
    {
        if (!Context.IsPostBack)
        {
            RefreshList();
        }
        base.PreRender();
    }

    public void RefreshList()
    {
        ...
    }
}

public class OrderDialogViewModel : DotvvmViewModelBase
{
    public event Action OrderCreated;
    ...
    public void Submit() 
    {
        ...
        OrderCreated?.Invoke();
    }
}
```

## See also

* [Viewmodels overview](../overview)
* [GridView data sets](gridview-data-sets)
* [Dependency injection](~/pages/concepts/configuration/dependency-injection/overview)
* [Binding direction](../binding-direction)
* [Viewmodel protection](../viewmodel-protection)
* [Video: Thinking in the MVVM way](https://www.youtube.com/watch?v=2iolJ_tgP4g&ab_channel=DotVVM)