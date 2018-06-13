# Binding Syntax

One of the most useful things in **DotVVM** is the **data binding**. The magic of writing "rich client-side apps" without using JavaScript
is the ability of DotVVM to translate binding expressions to JavaScript. 

All data bindings are **strongly typed** and compiled right before the page is accessed for the first time. Dynamic types are not supported yet.

Therefore, each master page has to specify the `@viewModel` directive so the binding compiler can verify the correctness of the expression and compile it.

There are several types of data binding in **DotVVM**. 

* `{value: Property}` - accesses the value of a specified property from the viewmodel.
* `{command: Function()}` - executes the specified method in the viewmodel. 
* `{staticCommand: Function()}` - executes a static method in the viewmodel.
* `{resource: ResourceFile.ResourceKey}` - gets the resource entry value from the specified RESX file.

There are also the `controlCommand` and `controlProperty` bindings. They are used when writing your own user controls.
You can find more about them in the [Markup Controls](/docs/tutorials/control-development-markup-only-controls/{branch}) section.

> The commercial version of [DotVVM for Visual Studio](/landing/dotvvm-for-visual-studio-extension) shows the IntelliSense for expressions in all types 
of bindings. 

## Usage

We can use a data binding expression almost everywhere in DOTHTML. 

The binding expression is enclosed in curly braces and has two parts:

* Binding type - e.g. `value`, `command` etc.
* Binding expression - a C#-like expression (with some restrictions and some enhancements).

For example, to bind some value from the viewmodel to a HTML attribute, you can use this syntax:
 
```DOTHTML
<a href="{value: Url}">...</a>
```

If you want to render the value as a text, you have to use double curly braces. That is because of the `<script>` and `<style>` elements.
The curly braces have special meaning inside these elements.

```DOTHTML
<p>Hello {{value: YourName}}!</p>
```

It is not possible to use the binding expression in the HTML attribute value in combination with another content:

```DOTHTML
<!-- This does not work - the whole attribute value has to be a data binding! -->
<a class="tab {value: AdditionalLinkClass}">...</a>
```

However, you can use expressions inside the binding, so you can combine the values easily:

```DOTHTML
<!-- This works - the whole attribute is a data binding! -->
<a class="{value: "tab " + AdditionalLinkClass}">...</a>
```

> DotVVM offers a special feature that allows to bind multiple CSS classes based on various expressions. See [Class Attribute](/docs/tutorials/basics-class-attribute/{branch}) page for more information.