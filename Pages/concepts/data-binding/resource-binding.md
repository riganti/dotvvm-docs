# Resource binding

The **resource binding** is commonly used to access resources and constants, or to evaluate any expression on the server. 

Consider the following markup:

```DOTHTML
<dot:Button Text="{resource: Constant}" />
``` 

The binding expression will be evaluated when the button is rendered into HTML. When the page is displayed, it will behave the same way as if the text was hard-coded in the markup. 

```DOTHTML
<!-- The value of the resource binding will be "baked" in the rendered HTML -->
<input type="button" value="My constant value" />
```

This is different than the [value binding](~/pages/concepts/data-binding/value-binding) where the expression is translated to a Knockout JS expression and responds to the changes of the viewmodel property.

In case of the resource binding, the value is "baked" in the HTML of the page so it is not possible to change it while the page is loaded.

The `value` binding will produce Knockout JS expression:

```DOTHTML
<!-- ko text: Constant --><!-- /ko -->
```

The `resource` binding renders just the pure value like it is hard-coded in the rendered HTML:

```
My constant value
```

Resource binding may be helpful in combination with [server-side rendering and SEO](~/pages/concepts/server-side-rendering).

## Access the RESX file entries

The primary scenario for this binding is to access .NET resource files (RESX). The default syntax is `{resource: FullNamespace.ResourceClass.ResourceKey}`. 
This will find the appropriate RESX file and use the value with the specified key.

For example, if you have a project named `MyWebApp` and you have a `Resources\Web\Strings.resx` file in the project, the resource class will 
be `MyWebApp.Resources.Web.Strings` (provided you haven't change the default namespace in the project properties). 

To retrieve the resource, you need to use the following expression:
```DOTHTML
<dot:Button Text="{resource: MyWebApp.Resources.Web.Strings.SomeResourceKey}" />
```

In order to be able to access the RESX file entries, they must be generated as public members. Make sure that you have set the __Access Modifier__ field to **Public**.

!(Enable public members code generation in RESX files)[resource-binding-img1.png]

## The @import directive

The syntax with the full namespace is quite long, so you can use the `@import` directive to import namespaces.

For example, in a project with the `Resources\Web\Strings1.resx` and `Resources\Web\Strings2.`esx* files, the markup can look like this:

```DOTHTML
@import MyWebApp.Resources.Web

{{resource: Strings1.SomeResource}}
{{resource: Strings2.SomeResource}}
```

## Call static methods or evaluate expressions on the server

The resource binding can be used to call methods, access constants or evaluate any expressions on the server.

### Access the `Context` properties

In many cases, you may want to print out some values from the [request context](~/pages/concepts/viewmodels/request-context). Since the `Context` property of the viewmodel is not serialized and sent to the client, you cannot use the value binding to retrieve the values. 

However, it is possible with the resource binding:

```DOTHTML
<nav>
  <!-- Highlight the current page in the menu based on the current route name -->
  <ul>
    <li class-active="{resource: Context.Route.RouteName == "Home"}">
      Home
    </li>
    <li class-active="{resource: Context.Route.RouteName == "Profile"}">
      Profile
    </li>
  </ul>

  <div class="user">
    <!-- Print out the current user name -->
    Welcome, {{resource: Context.HttpContext.User.Identity.Name}}!
  </div>
</nav>
```

### Call any method

Since the resource bindings are evaluated on the server, there are no restrictions in what methods you can call. 

## Binding route names in `RouteLink`

If you want to bind the `RouteName` property of the [RouteLink](~/controls/builtin/RouteLink) control, you cannot use the value binding. Value binding expressions are evaluated on the client which means that the entire route table would have to be sent to the client (which may impose some security risks). 

When you try to use a value binding for the `RouteName` property, you will get an error. But you can use the resource binding - it will work the same way as if the route name would be hard-coded in the markup.

```DOTHTML
<dot:Repeater DataSource="{value: MenuItems}" RenderSettings.Mode="Server" WrapperTagName="ul">
    <li>
        <dot:RouteLink RouteName="{resource: RouteName}" Text="{resource: Title}" />
    </li>
</dot:Repeater>
```

Notice that the [Repeater](~/controls/builtin/Repeater) needs to be switched to the server-side rendering mode, which will make it render all items directly in the page. By default, the Repeater would only render the inner template and generate the concrete items on the client-side.

If you need the menu to be fully dynamic (to support adding and removing items, each using different routes), you will need to build the URL for each item on the server and include them in the viewmodel. Then you can bind these URLs to plain hyperlinks:

```DOTHTML
<dot:Repeater DataSource="{value: MenuItems}" WrapperTagName="ul">
    <li>
        <a href="{value: Url}">{{value: Title}}</a>
    </li>
</dot:Repeater>
```

## Culture

The resource bindings are always evaluated on the server. When evaluating, the `CurrentUICulture` of the thread that handles the HTTP request will be used. 
 
See the [Globalization](~/pages/concepts/localization-and-cultures/multi-language-applications) section for more information about request cultures.

## See also

* [Data-binding overview](~/pages/concepts/data-binding/overview)
* [Value binding](~/pages/concepts/data-binding/value-binding)
* [Binding context](~/pages/concepts/data-binding/binding-context)
* [Respond to user actions](~/pages/concepts/respond-to-user-actions/overview)