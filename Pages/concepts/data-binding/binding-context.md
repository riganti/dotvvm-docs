# Binding Context

In **DotVVM**, each HTML element or DotVVM control has the `DataContext` property. Using this property, you can change the context in which data bindings are evaluated.

Let's have the following view and viewmodel:

<div>
<ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active">
        <a href="#view" role="tab" data-toggle="tab">page.dothtml</a>
    </li>        
    <li role="presentation">
        <a href="#viewmodel" role="tab" data-toggle="tab">AddressViewModel.cs</a>
    </li>
</ul>
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="view">

```DOTHTML
	@viewModel AddressViewModel, DotvvmDemo

	<div DataContext="{value: Customer}">
		{{value: Name}}
	</div>
```

</div>
<div role="tabpanel" class="tab-pane" id="viewmodel">

```CSHARP
public class AddressViewModel {
	public Customer Customer { get; set; }
}

public class Customer {
	public string Name { get; set; }
}
```

</div>
</div>
</div>

The `DataContext` property will make all bindings on the `<div>` element and all bindings inside this element to be evaluated in the context of the
`Customer` property of the viewmodel.

If the `DataContext` binding is not present, the binding inside the `<div>` would have to be `{{value: Customer.Name}}`.

## Null DataContext

There is also one helpful feature. If the `DataContext` of any element is `null`, the element is removed from the DOM and the bindings inside this element
are not evaluated at all.

When you set some value in the `DataContext` property, the element re-appears in the DOM.

If you don't want the `<div>` element to hide, the `DataContext` property must point to some property which is not `null`.

<br>

## Scope Context Variables

You can use the following binding context variables to navigate the `DataContext` hierarchy in elements that change the `DataContext` property.

* `_root` accesses the top-level viewmodel (the viewmodel of the page).
* `_parent` accesses the parent binding context.
* `_parent2` accesses the parent's parent binding context and so on...
* `_this` references the current context. It is useful when you need to pass the current `DataContext` e.g. as an argument to a method.

For example, the following binding calls the `DeleteAddress` method in the page viewmodel and passes the current binding context as an argument.

```DOTHTML
<div DataContext="{value: Company}">
	<dot:Button Click="{command: _root.DeleteAddress(_this)}" />
</div>
```

## Control Context Variable

The `_control` binding context variable can be used in user control files (`*.dotcontrol`) to access properties of the user control. See the [Markup Controls](/docs/tutorials/control-development-markup-controls) chapter for more info.

## Collection Context Variable

Additionally, you can use the `_collection` binding context variable to access the current item index and other properties in the `ItemTemplate` of a `Repeater`, `GridView` or similar data-bound control. The variable defines the following properties:

* `_collection.Index`
* `_collection.IsFirst`
* `_collection.IsOdd`
* `_collection.IsEven`

```DOTHTML
<dot:Repeater DataSource="{value: Items}">
  <p>Item {{value: _collection.Index}}</p>
</dot:Repeater>
```