# Create pages and controls

The [DotVVM Command Line tool](install) can create new [pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), or [markup controls](~/pages/concepts/control-development/markup-controls) in ASP.NET Core projects.

> Creating pages, master pages, and controls is supported only in ASP.NET Core projects using the new project system. For .NET Framework projects, we recommend [using Visual Studio](~/pages/quick-starts/build/first-page).

## Supported commands

<table class="table table-bordered">
    <tr>
        <th>Task</th>
        <th>Short Syntax</th>
        <th>Long Syntax</th>
        <th>Options</th>
    </tr>
    <tr>
        <td>Create Page</td>
        <td><code>dotnet dotvvm ap</code></td>
        <td><code>dotnet dotvvm add page</code></td>
        <td>
            <ul>
                <li><code>{PageName}</code> - name of the page or path of the `.dothtml` file</li>
                <li><code>-m {MasterPage}</code> - (optional) name or path of the master page</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>Create Master Page</td>
        <td><code>dotnet dotvvm am</code></td>
        <td><code>dotnet dotvvm add master</code></td>
        <td>
            <ul>
                <li><code>{PageName}</code> - name of the page or path of the `.dotmaster` file</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>Create Markup Control</td>
        <td><code>dotnet dotvvm ac</code></td>
        <td><code>dotnet dotvvm add control</code></td>
        <td>
            <ul>
                <li><code>{ControlName}</code> - name of the control or path of the `.dotcontrol` file</li>
                <li><code>-c</code> - (optional) create a code-behind file for the control</li>
            </ul>
        </td>
    </tr>
</table>

## Example

1. Create the `Views/Site.dotmaster` master page:

```
dotnet dotvvm add master Site
```

2. Create the `Views/Page1.dothtml` page and embed it in the `Views/Site.dotmaster`:

```
dotnet dotvvm add page Page1 -m Site
```

3. Create the `Controls/MyControl.dotcontrol` user control with the code behind file:

```
dotnet dotvvm add control Controls/MyControl.dotcontrol -c
```

## See also

* [Install DotVVM CLI](install)
* [Generate REST API clients](generate-rest-api-clients)
