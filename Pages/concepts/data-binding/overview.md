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

However, you can use expressions inside the binding, so you can combine the values easily:

```DOTHTML
<!-- This works - the whole attribute is a binding expression -->
<a class="{value: "tab " + AdditionalLinkClass}">...</a>
```

> If you want to build complex `class` attributes, DotVVM has a special feature that allows to [combine multiple CSS classes](~/pages/concepts/dothtml-markup/combine-css-classes-and-styles).

## See also

* [Value binding](~/pages/concepts/data-binding/value-binding)
* [Resource binding](~/pages/concepts/data-binding/resource-binding)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)