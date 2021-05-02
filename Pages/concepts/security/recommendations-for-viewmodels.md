# Recommendations for viewmodels

The most crucial thing in DotVVM is to understand the "public" nature of the viewmodels. There are two rules you should keep in mind:

1. **Never put anything sensitive in the viewmodel**
2. **Never trust anything users put in the viewmodel**

The viewmodels are serialized and sent to the client's browser. Anyone can open the Dev Tools console and look in the page source. Also, anyone can modify any property in the viewmodel, or even tamper with the viewmodel structure (adding or removing entries in the collections, assigning objects into properties with the `null` value, or vice-versa). 

In general, we found it helps to **think of the viewmodel as it is the input and output of a REST API**. You need to be careful about what you send out, and what you receive.

## Create DTOs with only the properties the page needs

It is tempting to take entity classes from the Entity Framework, and just use them in the viewmodel. However, in most cases you will be exposing more information than the page really needs, and it may lead to security risk. 

For example, if you want to display a list of the users in a [GridView](~/controls/builtin/GridView), don't use directly the `User` entities in the viewmodel.  Such entities will probably have fields with _hashed passwords and other highly sensitive information_ about the users. Even if these values are not displayed to the user in the `GridView`, they will be a part of the viewmodel and can be read by anyone.

**Always use [Data Transfer Objects (DTOs)](https://en.wikipedia.org/wiki/Data_transfer_object) in the viewmodels, and make sure they contain only the properties that the page needs.** 

It may look like a lot of extra work, but it pays off in the long-term. Don't worry if you need to create multiple DTOs for different purposes. It's better than reusing the same DTO for many different scenarios - it may also carry properties that are not needed by the page, which is unnecessary or even dangerous.

Libraries like [AutoMapper](http://automapper.org/) can help with mapping entities to DTOs. 

We also recommend to follow the [separation of data, state and environment](~/pages/concepts/viewmodels/work-with-data/best-practices#separate-properties-with-different-semantics) - this helps to keep the structure of the viewmodels and the DTOs clean, and easy to review.

## Don't trust anything in the viewmodel

The user may change anything in the viewmodel - not just the data that are bound to `TextBox` or other controls. Pay special attention to IDs of related entities, or properties indicating whether some part of the page is available (e. g. whether the user can edit something) - even these may be changed.

If you hide some button using the `Visible` binding, the user can still open the Dev Tools console and modify the viewmodel so the button is displayed. They can still make a postback using such button, and you need to verify __on the server__ that the user is permitted to make that action.

Similarly, if you calculate the price of an order and send it to the client in the viewmodel, once the user submits the order, you should never use the price provided by the user - you should always recalculate the price. 

### Re-calculate properties on postbacks

Some properties may be sent to the client, but they are calculated on the server, and can be recalculated on all requests (including postbacks). These are often properties which indicate whether the user is allowed to perform some action. These properties are used to display or enable some parts of the UI. 

The user may change the values in Dev Tools console and click on such buttons, so you should always check the permissions for the action in the business layer. 
However, it makes sense to recalculate these properties on all requests, ideally in the `Load` method of the viewmodel (so it takes place before commands triggered by these buttons).

Theoretically, the user will be able to send a postback with modified value of this property, but thanks to its recalculation in the `Load` method, the user-submitted value won't be used.

```CSHARP
public override Task Load()
{
    CanChangeShippingAddress = orderService.CanChangeShippingAddress(OrderId);

    base.Load();
}
```

### Use bind direction

Another way of "ignoring" client-side changes to the property is to set [bind direction](~/pages/concepts/viewmodels/bind-direction) to `ServerToClient` or `ServerToClientFirstRequest`. You can achieve the same behavior by delaring the setter as private.

```CSHARP
[Bind(Direction.ServerToClientFirstRequest)]
public UserBasicInfoDto UserInfo { get; }

// private set behaves same as [Bind(Direction.ServerToClient)]
public bool CanChangeShippingAddress { get; private set; }
```

See the [bind direction](~/pages/concepts/viewmodels/bind-direction) chapter for more info.

A special case are properties which are fetched from the route or query string parameters using `FromRoute` or `FromQuery` parameters. They are set to `Bind(Direction.ServerToClient)` automatically. We recommend to use `private set` for such properties to express that the value cannot be provided by the user.

### Use viewmodel protection

If you need to send some value to the client, and work with it later (on postbacks), you can turn on [viewmodel protection](~/pages/concepts/viewmodels/viewmodel-protection) on such property. It is possible to __sign__ the value (the user can change it, but the server will notice it and throw an exception), or __encrypt__ it (the user won't be able to read it). 

```CSHARP
[Protect(ProtectMode.SignData)]
public int CustomerId { get; set; }
```

See the [viewmodel protection](~/pages/concepts/viewmodels/viewmodel-protection) chapter for more info.

## Check permissions in the business layer

DotVVM offers a set of tools for [validation of data](~/pages/concepts/validation/overview), but it may not cover all the cases. The validation can make sure that the ID of the order is in a correct format, but it doesn't check that the user is allowed to display or modify such order. 

This needs to be handled in the business layer of the application. A good idea is to throw exceptions if the user is not authorized to perform such operation, and handling such exceptions using [exception filters](~/pages/concepts/viewmodels/filters/exception-filters).

## Example

This example demonstrates and explains various approaches, how the data can be protected:

* Setting the bind direction to `ServerToClient` - ignoring values possibly sent by the client
* Using the viewmodel protection
* Getting the information from the business layer on each request (including postbacks)

```CSHARP
public class OrderDetailViewModel : DotvvmViewModelBase
{

    private readonly OrderService orderService;

    // the user won't be able to change the OrderId in the viewmodel, DotVVM always reads it from the route
    // marking the setter as private is optional, but we like it as it helps to express that the value cannot be set from the client-side
    // however, the user can change the ID in the address bar of the browser
    // we'll have to make sure the business layer throw an exception if the user cannot display the order
    [FromRoute("Id")]
    public int OrderId { get; private set; }

    // we are using signing protection, so the user won't be able to change the value
    // otherwise, we'd have to load the associated CustomerId on every request
    [Protect(ProtectMode.SignData)]
    public int CustomerId { get; set; }

    // this object contains ONLY the properties that are actually needed by the UI
    // the bind direction tells that this data is sent to the client on the first request, and cannot be changed next time
    [Bind(Direction.ServerToClientFirstRequest)]
    public UserBasicInfoDto UserInfo { get; }

    // this object contains ONLY the properties that are actually needed by the UI
    public OrderDetailsDto OrderDetails { get; set; }

    // private set sets the bind direction to ServerToClient
    // the user will be able to change the value on the client, so the "Change Shipping Address" button may appear
    public bool CanChangeShippingAddress { get; private set; }

    public OrderDetailViewModel(OrderService orderService)
    {
        this.orderService = orderService;
    }

    public override Task Load()
    {
        // we get this info on all requests, including postbacks
        CanChangeShippingAddress = orderService.CanChangeShippingAddress(OrderId);

        base.Load();
    }

    public override Task PreRender()
    {
        if (!Context.IsPostBack)
        {
            // the OrderService should verify the user has permissions to get the order details
            // it can throw e.g. UnauthorizedAccessException which will be caught by exception filter
            OrderDetails = orderService.GetOrderDetails(OrderId);

            // this property is not used on postbacks, it's used only on the first request
            // DotVVM will ignore it on postbacks, so it doesn't need to be protected
            UserInfo = orderService.GetUserInfo(OrderId);

            // this property is needed on postbacks, so we are signing it
            // the user won't be able to change it
            // alternatively, we'd have to load the CustomerId on every request (in the Load phase)
            CustomerId = orderService.GetOrderCustomer(OrderId).CustomerId;
        }

        base.PreRender();
    }

    // not all properties of OrderDetails can be changed (e. g. order creation date)
    // the viewmodel and the UI is designed to perform only "possible and meaningful" actions, instead of just saving the entire OrderDetailsDto

    public void ChangeShippingAddress()
    {
        // we have created a special DTO that represents address change request
        var newAddress = new AddressChangeDto() 
        {
            Street = OrderDetails.ShippingAddress.Street,
            ...
        };

        // the OrderService must check the permissions
        // * can the user modify that order?
        // * can the address be changed in this state of the order?
        orderService.ChangeShippingAddress(OrderId, newAddress)
    }

    ...

}
```

## See also

* [HTML encoding](html-encoding)
* [Authentication & authorization](authentication-and-authorization/overview)
* [Best practices for viewmodels](~/pages/concepts/viewmodels/work-with-data/best-practices)