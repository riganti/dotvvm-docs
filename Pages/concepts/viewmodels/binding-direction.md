# Binding direction

Sometimes, you may need to customize which properties will be transferred from the server to client or from the client to the server when using [commands](~/pages/concepts/respond-to-user-actions/commands). In many cases, there is no need to transfer the entire viewmodel. 

You can use the `[Bind(...)]` attribute to define which properties are transferred in which direction. 

```CSHARP
[Bind(Direction.ServerToClient)]
public string FirstName { get; set; }
```

## Directions

The `Direction` is an enumeration with the following options:

* `Both` is the default setting. The property value is transferred in both ways. Use this setting for values the user enters in the form controls, like [TextBox](~/controls/builtin/TextBox) or others.

* `None` the property value is **not** transferred either from the server to the client nor from the client to the server.

* `ServerToClient` transfers the value only from the server to the client. 

* `ServerToClientFirstRequest` transfers the value from the server to the client on the initial page load, not during postbacks. This is useful e. g. for representing a list of items in the [ComboBox](~/controls/builtin/ComboBox) when the collection is static and cannot be changed. 

* `ClientToServer` transfers the value only from the client to the server. You can use this for the values of the form fields which can only be read on the server (e.g. value in the search box). 

* `IfInPostBackPath` is a special setting that allows to transfer only part of the viewmodel from the server to the client. Consider a page with a [GridView](~/controls/builtin/GridView) control with 20 rows representing the users. The viewmodel contains a collection of 20 objects - one for each user. If you have a button in each row that can e.g. enable or disable the user, you can use this setting to transfer only the single object from the collection.

```DOTHTML
<dot:GridView DataSource="{value: Users}">
    <dot:GridViewTextColumn ValueBinding="{value: UserName}" HeaderText="User Name" />
    <dot:GridViewCheckBoxColumn ValueBinding="{value: IsEnabled}" HeaderText="Is Enabled?" />
    <dot:GridViewTemplateColumn>
        <dot:LinkButton Text="Enable / Disable" Click="{command: _parent.EnableDisableUser(_this)}" />
    </dot:GridViewTemplateColumn>
</dot:GridView>
```

If you use the `[Bind(Direction.IfInPostBackPath)]` on the `Users` property in the viewmodel, only the user from the row which was accessed, will be transferred. The viewmodel that will be posted to the server will look like this:

```
{
    "Users": [
        ...
        null,
        { 
            "UserName": "the user being edited",
            "IsEnabled": true
        },
        null,
        ...
    ]
}
```

> Please be careful when using non-default binding directions. Some properties of the viewmodel might not be initialized and may contain null values!

> Sending the entire viewmodel to the server and back may be inefficient in many real-world scenarios. See the [optimize command performance](~/pages/concepts/respond-to-user-actions/optimize-command-performance) chapter for more details.

## See also

* [Viewmodels overview](overview)
* [Viewmodel protection](viewmodel-protection)
* [Viewmodel best practices](work-with-data/best-practices)
* [Commands](~/pages/concepts/respond-to-user-actions/commands)
* [Optimize command performance](~/pages/concepts/respond-to-user-actions/optimize-command-performance)