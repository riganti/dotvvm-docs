# Data-binding overview

One of the key concepts in **DotVVM** is the **data-binding**. The magic of building "rich client-side apps" without writing JavaScript is here thanks to the ability of DotVVM to translate binding expressions to JavaScript. 

All binding expressions in DotVVM are **strongly-typed** and compiled, which helps to avoid many runtime errors caused by typos in identifiers and similar. 

Therefore, each DotVVM page has to specify the `@viewModel` directive so the binding compiler can verify the correctness of all expressions, and pre-compile them.

## Types of binding expressions

There are several types of binding expressions in **DotVVM**:

* `{value: Property}` - accesses the value of a specified property in the viewmodel.
* `{resource: ResourceFile.ResourceKey}` - evaluates an expression when the page is rendered.
* `{command: Function()}` - posts the viewmodel to the server and invokes the specified method. 
* `{staticCommand: Function()}` - invokes either a static method, or a method in a static command service.

The [value binding](~/pages/concepts/data-binding/value-binding) and [resource binding](~/pages/concepts/data-binding/resource-binding) are used to access the viewmodel properties. 

The [command](~/pages/concepts/respond-to-user-actions/commands) and [static commands](~/pages/concepts/respond-to-user-actions/static-commands) are used to [respond to user actions](~/pages/concepts/respond-to-user-actions/overview) in buttons and other components.

There are also the `controlCommand` and `controlProperty` bindings, which are just a special cases of `command` and `value` bindings used in [creating markup controls](~/pages/concepts/control-development/markup-controls). 

> The commercial version of [DotVVM for Visual Studio](https://www.dotvvm.com/products/visual-studio-extensions) offers IntelliSense for expressions in all types of bindings. 

## Usage

You can use a data binding expression almost everywhere in DotHTML. The binding expression is enclosed in curly braces and has two parts:

* Binding type - e.g. `value`, `command` etc.
* Binding expression - a C#-like expression (with some restrictions and some enhancements).

For example, to bind some value from the viewmodel to a HTML attribute, you can use this syntax:
 
```DOTHTML
<a href="{value: Url}">...</a>
```

If you want to render the value as a text, you need to use double curly braces. That is because of the `<script>` and `<style>` elements - single curly braces have special meaning inside these elements.

```DOTHTML
<p>Hello {{value: YourName}}!</p>
```

It is not possible to use the binding expression in the HTML attribute value in combination with another content:

```DOTHTML
<!-- This does not work - the whole attribute value has to be a data binding! -->
<a class="this-does-not-work {value: DoNotUseThis}">...</a>
```

However, you can use [expressions](supported-expressions) inside the binding, so you can combine the values easily:

```DOTHTML
<!-- This works - the whole attribute is a binding expression -->
<a class="{value: "tab " + AdditionalLinkClass}">...</a>
```

> If you want to build complex `class` attributes, DotVVM has a special feature that allows to [combine multiple CSS classes](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles).

## Null handling

You don't have to worry about `null` values in binding expressions. If some part of the expression evaluates to `null`, the whole expression will return `null`. 

Internally, DotVVM treats every `.` as `.?` in C# 6.

> In previous versions of DotVVM, when you tried to call a method in a value binding and any of its arguments evaluated to `null`, the method wasn't invoked and the result of the expression was `null`. From DotVVM 3.0, this behavior was changed - the method will be invoked and `null` value will be passed as an argument.

## Double and single quotes

Because the bindings in HTML attributes are often wrapped in double quotes, DotVVM allows to use single quotes (apostrophes) for strings as well. 

This is different from the C# syntax where double quotes are used for `string` values while single quotes are used for `char` values.
In DotVVM, the single and double quotes can be used interchangeably.

```DOTHTML
<a class="{value: Active ? 'active' : 'not-active' }"></a>
```

## The @import directive

You can use the `@import` directive in a similar way how you use `using` statement in C# to import namespaces.

For example, in a project with the `Resources\Web\Strings1.resx` and `Resources\Web\Strings2.`resx* files, the markup can look like this:

```DOTHTML
@import MyWebApp.Resources.Web

{{resource: Strings1.SomeResource}}
{{resource: Strings2.SomeResource}}
```

## Enums

If you have a property of an enum type in your viewmodel, you may need to work with that value in the binding. 

```CSHARP
public class MyViewModel {
    ...
    public MyApp.Enums.ButtonColor Color { get; set; }    // ButtonColor is enum
    ...
}
```

You can use the `@import` directive to import the namespace in which the enum is declared. Then, you can use the `ButtonColor.Red` to reference the enum member.

```DOTHTML
@viewModel ...
@import MyApp.Enums

<div class-red="{value: Color == ButtonColor.Red}"></div>
```

On the client-side, the enum values are converted to strings on the client side, so you can compare the value with strings. The following expression will also work - this is different from C# where enums cannot be compared with `string` values directly.

```DOTHTML
<a class="{value: Color == 'Red' ? 'button-red' : 'button-normal'}">button</a>
```

You can use `[EnumMember(Value = "abc")]` to provide a different string representation of the enum value:

```CSHARP
public enum ButtonColor
{
    [EnumMember(Value = "button-red")]
    Red,
    [EnumMember(Value = "button-green")]
    Green,
    [EnumMember(Value = "button-blue")]
    Blue
}
```


## See also

* [Value binding](~/pages/concepts/data-binding/value-binding)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)