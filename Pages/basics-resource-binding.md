# Resource Binding

The **resource binding** is used to access resources and constants, or to evaluate any expression on the server. 

Consider the following markup:

```CSHARP
<dot:Button Text="{resource: Constant}" />
``` 

The binding expression will be evaluated when the button is rendered into HTML, and it will behave the same way as if the text was hard-coded in the markup. In comparison to the [Value Binding](/docs/tutorials/basics-value-binding/{branch}), the expression is not translated to Knockout JS expression.

## Accessing the resources

The primary scenario for this binding is to access .NET resource files (RESX). The default syntax is `{resource: FullNamespace.ResourceClass.ResourceKey}`. 
This will find the appropriate RESX file and use the value with the specified key.

For example, if you have a project named *MyWebApp* and you have a *Resources\Web\Strings.resx* file in the project, the resource class will 
be *MyWebApp.Resources.Web.Strings* (provided you haven't change the default namespace in the project properties). To retrieve the resource, you need
to use `{resource: MyWebApp.Resources.Web.Strings.SomeResourceKey}`.

In order to be able to access the RESX file entries, their C# representation must exist. Make sure that you have set the __Access Modifier__ field to **Public**: 
![Enable code generation for RESX files](basics-resource-binding-img1.png)

## The @import Directive

The syntax with full namespace is long, so you can use the `@import` directive to import namespaces.

For example, in a project with the *Resources\Web\Strings1.resx* and *Resources\Web\Strings2.resx* files, the markup can look like this:

```DOTHTML
@import MyWebApp.Resources.Web

{{resource: Strings1.SomeResource}}
{{resource: Strings2.SomeResource}}
```

## Other Uses

The resource binding can be used to call methods, access constants or properties on the server.

This may help with [Server-side Rendering](/docs/tutorials/basics-server-side-html-generation/{branch}) and SEO (search engine optimization).

The difference between `{{value: FirstName}}` and `{{resource: FirstName}}` is the output HTML.

The `value` binding will produce Knockout JS expression:

```DOTHTML
<!-- ko text: FirstName --><!-- /ko -->
```

The `resource` binding renders just the pure value like it is hard-coded in the markup:

```
John
```

It also helps with rendering menus or other lists of links:

```DOTHTML
<dot:Repeater DataSource="{value: MenuItems}" RenderSettings.Mode="Server" WrapperTagName="ul">
    <li>
        <dot:RouteLink RouteName="{resource: RouteName}" Text="{resource: Title}" />
    </li>
</dot:Repeater>
```

Notice that the [Repeater](/docs/controls/builtin/Repeater/{branch}) is switched to the server-side rendering mode which will make it render all items directly in the page. By default, the Repeater only renders one item and generate the items on the client-side.

The [RouteLink](/docs/controls/builtin/RouteLink/{branch}) control uses `resource` bindings so they are evaluated when the control is rendered and they behave like a hard-coded values on the client-side. It is the only way how to use `RouteLink` if the `RouteName` is parameterized. The `RouteName` property cannot use `value` binding - it would require to have a complete route table distributed on the client which would be a security issue. 

## Notes

 The resource bindings are always evaluated on the server. When evaluating, the `CurrentUICulture` of the thread that handles the HTTP request will be used. 
 
 See the [Globalization](/docs/tutorials/basics-globalization/{branch}) section for more information about working with the request cultures.

