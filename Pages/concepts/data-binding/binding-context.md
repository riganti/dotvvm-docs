# Binding context

In **DotVVM**, each HTML element or DotVVM control has the `DataContext` property. Using this property, you can change the context in which data bindings are evaluated.

Let's have the following view and viewmodel:

# [Address.dothtml](#tab/example1-view)

```DOTHTML
	@viewModel AddressViewModel, DotvvmDemo

	<div DataContext="{value: Customer}">
		{{value: Name}}
	</div>
```

# [AddressViewModel.cs](#tab/example1-viewmodel)

```CSHARP
public class AddressViewModel 
{
	public Customer Customer { get; set; }
}

public class Customer 
{
	public string Name { get; set; }
}
```

***

The `DataContext` property will make all bindings on the `<div>` element and its children to be evaluated in the context of the `Customer` property of the viewmodel.

If the `DataContext` binding is not present, the binding inside the `<div>` would have to be `{{value: Customer.Name}}`.

This mechanism allows you to simplify binding expressions - to use `{value: Name}` instead of `{value: CustomerName}`.

## Scope variables

You can use the following binding context variables to navigate in the `DataContext` hierarchy:

* `_root` accesses the top-level viewmodel (the viewmodel of the page).
* `_parent` accesses the parent binding context.
* `_parent2` accesses the parent's parent binding context, and so on...
* `_this` references the current context. It is useful when you need to pass the entire `DataContext` e.g. as an argument to a method.

For example, the following binding expression calls the `DeleteAddress` method in the page viewmodel and passes the current binding context as an argument:

# [EditCompany.dothtml](#tab/example2-view)

```DOTHTML
<div DataContext="{value: Company}">
	<dot:Button Click="{command: _root.DeleteAddress(_this)}" />
</div>
```

# [EditCompanyViewModel.cs](#tab/example2-viewmodel)

```CSHARP
public class EditCompanyViewModel 
{
	public Company Company { get; set; }

    public void DeleteAddress(Company company) 
    {
        // ...
    }
}

public class Company 
{
    public int Id { get; set; }
	public string Name { get; set; }
}
```

***

## Collection context variable

If the binding context is a collection, you can use the `_collection` binding context variable to access the current item index and other properties. This behavior is most commonly used in the `ItemTemplate` of a `Repeater`, `GridView`, or similar data-bound controls. 

The `_collection` variable has the following properties:

* `_collection.Index`
* `_collection.IsFirst`
* `_collection.IsOdd`
* `_collection.IsEven`

For example, you can print out the indexes of collection items:

```DOTHTML
<dot:Repeater DataSource="{value: Items}">
  <p>Item {{value: _collection.Index}}</p>
</dot:Repeater>
```

## Null binding context behavior

There is one useful caveat to the `DataContext` property. If you set the `DataContext` of any element to `null`, the element will be removed from the DOM, and the bindings inside this element won't be evaluated at all.

After a non-`null` value is set to the `DataContext` property, the element will be returned back in the page DOM.

This can be used to create optional sections in the page which are available only when corresponding objects are loaded.

## Access markup control properties

The `_control` binding context variable can be used in user control files (`*.dotcontrol`) to access properties of the user control. 

See the [Markup controls](~/pages/concepts/control-development/markup-controls) chapter for more info.

## See also

* [Value binding](~/pages/concepts/data-binding/value-binding)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)