## DotVVM Command Line

When you create DotVVM project (ASP.NET Core) using [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension) or using [dotnet new](/docs/tutorials/how-to-start-command-line/{branch}), the **DotVVM Command Line** tool should be already registered in the project file:

```
<Project ToolsVersion="15.0" Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  ...
  <ItemGroup>
    <DotNetCliToolReference Include="DotVVM.CommandLine" Version="1.1.0" />
  </ItemGroup>
</Project>
```

This will allow to run commands starting with `dotnet dotvvm ...` in the project directory to perform various actions with the DotVVM project.


### Creating Pages, Master Pages and Markup Controls

**DotVVM Command Line** can create new pages, master pages and markup controls in ASP.NET Core projects.

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

#### Examples

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

> Support for [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/2-0) was added in **DotVVM 2.0**.