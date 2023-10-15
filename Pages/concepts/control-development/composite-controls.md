# Composite controls

Composite controls are a new way of declaring controls added in version 4.0. The main use case for composite controls is to offer an **easy method of combining existing controls into larger blocks**, and being able to parameterize these blocks.

## Declare the composite control

The composite control must satisfy the following two requirements:

* Inherit from the `CompositeControl` class.
* Contain the static or instance method called `GetContents`. This method can have any parameters (they will be interpreted as control properties), and must return either `DotvvmControl` or `IEnumerable<DotvvmControl>`. 

The declaration can look like this:

```CSHARP
public class ImageButton : CompositeControl
{
    public static DotvvmControl GetContents(
        [DefaultValue("Button")] ValueOrBinding<string> text,
        ICommandBinding click,
        string imageUrl = "/icons/default-image.png"
    )
    {
        return new LinkButton()
            .SetProperty(b => b.Click, click)
            .AppendChildren(
                new HtmlGenericControl("img")
                    .SetAttribute("src", imageUrl),
                new Literal(text)
            );
    }
}
```

## Composite control properties

The code snippet above declared the `ImageButton` control with 3 properties:

* `Text` - the property is of type `ValueOrBinding<string>`. You can assign both [value binding](~/pages/concepts/data-binding/value-binding) or a hard-coded value to the control. Also, the property has a default value - if it is not specified, the text "Button" will be used.

* `Click` - the property specifies `ICommandBinding` as a type, so only the [command](~/pages/concepts/respond-to-user-actions/commands) or staticCommand binding can be assigned to the property. It doesn't have a default value, therefore it is required. 

* `ImageUrl` - the property is declared as `string`, so it will support only hard-coded values in markup (or a resource binding). It has a default value.

You can also use the following types of parameters:

* `ITemplate` for passing templates - you can then use the [TemplateHost](~/controls/builtin/TemplateHost) to instantiate the template in the generated controls.

* `DotvvmControl` or `IEnumerable<DotvvmControl>` for passing child elements - you can use for example the `AppendChildren` method to place the controls as children in the generated controls.

* `IValueBinding<T>` for properties that allow only value binding expressions.

You can use nullable types - in general, they tell DotVVM that the property is optional. You can specify the default value either as a default value of the parameter, or via the `[DefaultValue]` attribute. If the property is not nullable and doesn't provide a default value, it will be treated as a required property.

You can use the [MarkupOptions](control-properties#specify-markup-otpions) attributes or the DataContextChange attributes on the properties, same as on the properties in markup or code-only controls.

If the property is named Content or ContentTemplate, it will be the default content property - any controls inside of the CompositeControl will be placed into this property.

See the [Control properties](control-properties) for more information about declaring control properties.

> If a parameter of type `IDotvvmRequestContext` is added to GetContents, it will not create a new property and instead simply pass the current request context into the method. 

## Fluent API for building control hierarchies

In order to simplify building control hierarchy, DotVVM adds several extension methods. The most important are:

* `SetProperty` - can set a property to the control.
    * It supports specifying the property via a lambda expression or by passing the `DotvvmProperty` object
    * It supports specifying the property as its name, which allows setting properties of other composite controls or markup controls using the [`@property` directive](./markup-controls.md#declare-the-property-using-the-property-directive).
    * It supports the `ValueOrBinding<T>` types, it will internally call either `SetValue` or `SetBinding`.

* `SetAttribute` - sets a HTML attribute. It is commonly used for `HtmlGenericControl`.

* `SetCapability` - sets a [control capability](control-capabilities) property. 

* `AddCssClass` - adds a CSS class. It can be called multiple times.
    - `AddCssClass(className, condition)` overload can be used to add a conditional CSS class. The condition may be a value binding.

* `AddCssStyle` - add an inline CSS style into the `style` attribute. The style value may be a binding.

* `AppendChildren` - appends children to the control's children collection.

### Using Markup controls in composite controls

If you plan to instantiate a markup control as a child inside a composite control, you should use the `MarkupControlContainer` class. 
See the [Markup controls](code-only-controls#using-markup-controls-in-code-only-controls) page for more info.

### PostBack.Handlers

[`PostBack.Handlers`](../respond-to-user-actions/postback-handlers.md) is a collection of postback handler which can be added onto any control.
The postback handlers are automatically applied to postback expressions the control creates using the `KnockoutHelper.GenerateClientPostBackExpression` method.
However, composite controls don't generate any postback expressions and delegate this work to child components, so the Postback.Handlers collection must be copied to the same child components where command bindings are passed.

As of version 4.2, `PostBack.Handlers` set on the composite control are copied into the child components automatically.
The cloning procedure takes place after the GetContents method is invoked, so any controls created in the method should have the correct postback handlers applied.

Explicit action might be needed if additional controls are generated outside the GetContents, for example inside a `DelegateTemplate`.
In such case, apply the `this.CopyPostBackHandlersRecursive(control)` on the control which might need a postback handler from the parent composite.

```CSHARP
public DotvvmControl GetContents(
    IValueBinding<IEnumerable<string>> dataSource,
    ICommandBinding itemClick = null,
    ICommandBinding headerClick = null
)
{
    var repeater = new Repeater() {
        WrapperTagName = "div",
        ItemTemplate = new DelegateTemplate(() =>
            // postback handlers must be explicitly cloned
            this.CopyPostBackHandlersRecursive(new Button("Item", itemClick))
        )
    }
    .SetProperty(Repeater.DataSourceProperty, dataSource);
    return new HtmlGenericControl("div")
        .AppendChildren(
            new HtmlGenericControl("h3") { InnerText = "List of buttons" }
                // no action needed, this command will be found automatically
                .SetBinding(Events.Click, headerClick),
            repeater
        );
}
```

In the example above, we create a list of buttons with one clickable title above.
The buttons are created inside `DelegateTemplate`, so we must copy the postback handler explicitly.

Note that in this simple case, it would be easier to use `new CloneTemplate(new Button("Item", itemClick))` instead.
In that case, DotVVM is able to set the postback handlers automatically.

> The postback handlers are only applied onto controls which have **the same command** as the composite control.
> No postback handlers will be automatically applied if a new command binding is created (even when derived from an existing command).
> You can always override this behavior by explicitly copying the matching `Postback.Handlers` collection.


## Precompilation

By default, the GetContents runs for each HTTP request, potentially many times if the control is in a Repeater.
Instead, it is possible to evaluate the control only once, during the dothtml compilation using the `[ControlMarkupOptions(Precompile = ControlPrecompilationMode.Always)]` attribute on the component.
The available precompilation modes are:

* **Never** - Never attempt precompilation (the default).
* **IfPossibleAndIgnoreExceptions** - Attempt precompilation whenever it's possible. If exception is thrown by the control, it is ignored and precompilation is skipped.
* **IfPossible** - Attempt precompilation whenever it's possible. If exception is thrown by the control, compilation fails.
* **Always** - Always try to precompile the control and fail compilation when it's not possible.
* **InServerSideStyles** - Always precompile this controls and do that while styles are being processed. This will allow other styles to process the generated controls AutoUI.

The precompilation is deemed impossible if a resource binding is used in a property which does not support `ValueOrBinding`.
The control may also throw the `SkipPrecompilationException` to suppress precompilation in certain cases.

Precompilation is especially valuable if the composite control is expensive to create - if it needs to compile new bindings or scan objects using reflection, for example.
Instead of caching the individual bindings, the entire generated control tree can be cached.

In the following example, we create a simplistic form control which simply creates a textbox for each property of the current data context.


```CSHARP
// "Always" Precompile - the CreateBinding is slow and we want to avoid performance surprises.
// In case the precompilation isn't possible, we will get a compilation error
[ControlMarkupOptions(Precompile = ControlPrecompilationMode.Always)]
public class ObjectEditor
{
    public ObjectEditor(BindingCompilationService bindingService)
    {
        this.bindingService = bindingService;
    }
    public IEnumerable<DotvvmControl> GetContents()
    {
        foreach (var property of this.GetDataContextType().DataContextType.GetProperties())
        {
            var binding = CreateBinding(property);

            yield return new HtmlGenericControl("div")
                .AddCssClass("form-row")
                .AppendChildren(
                    new Literal($"{property.Name}: "),
                    new TextBox().SetProperty(t => t.Text, binding)
                );
        }
    }

    IValueBinding CreateBinding(PropertyInfo property)
    {
        DataContextStack dataContext = this.GetDataContextType();
        // DotVVM bindings are built using System.Linq.Expressions
        // _this is represented as a parameter expression with BindingParameterAnnotation of the current data context
        Expression _this =
            Expression.Parameter(dataContext.DataContextType, "_this")
                      .AddParameterAnnotation(new BindingParameterAnnotation(dataContext));
        Expression expression = Expression.Property(_this, property);

        // To initialize the binding, we have to specify the current data context and the expression.
        // The constructor automatically creates the server-side compiled delegate and the Knockout.js binding.
        // This takes rather long time to compile, which is why we enforce 
        return new ValueBindingExpression(this.bindingService, new object[] {
            dataContext,
            new ParsedExpressionBindingProperty(expression),
        });
    }
}
```

There is number of limitations on the precompiled controls:

* Resource bindings can only be used in properties of type `ValueOrBinding<T>`
* IDotvvmRequestContext isn't available
* `this.Parent` isn't available
* All child controls are of type `ResolvedControlHelper.LazyRuntimeControl`. In you need access to the the real control, call the the `GetLogicalChildren()` method on it.
* `this.GetDotvvmUniqueId` and `this.CreateClientId` methods currently don't work
* `OnInit`, `OnLoad`, `OnPreRender` and the Render methods are ignored
* At runtime, the control is replaced by `PrecompiledControlPlaceholder` which only contains its content.

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Control properties](control-properties)
* [Control capabilities](control-capabilities)
