# Resource Binding

> In DotVVM 1.1, the resource bindings have changed. The `@resourceType` and `@resourceNamespace` directives are no longer supported. The `@import` directive is used instead.

The **resource binding** is used to access the values from .NET resource files (RESX). The default syntax is `{resource: FullNamespace.ResourceClass.ResourceKey}`. 
This will find the appropriate RESX file and use the value with the specified key.

For example, if you have a project named *MyWebApp* and you have a *Resources\Web\Strings.resx* file in the project, the resource class will 
be *MyWebApp.Resources.Web.Strings* (provided you haven't change the default namespace in the project properties). To retrieve the resource, you need
to use `{resource: MyWebApp.Resources.Web.Strings.SomeResourceKey}`.

## The @import Directive

The syntax with full namespace is long, so you can use the `@import` directive to import namespaces.

For example, in a project with the *Resources\Web\Strings1.resx* and *Resources\Web\Strings2.resx* files, the markup can look like this:

```DOTHTML
@import MyWebApp.Resources.Web

{{resource: Strings1.SomeResource}}
{{resource: Strings2.SomeResource}}
```

<br />

 The resource bindings are always evaluated on the server. When evaluating, the `CurrentUICulture` of the thread that handles the HTTP request will be used. 
 
 See the [Globalization](/docs/tutorials/basics-globalization/{branch}) section for more information about working with the request cultures.

