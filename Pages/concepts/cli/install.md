# DotVVM Command Line

When you create DotVVM project (ASP.NET Core) using [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension) or using [dotnet new](/docs/tutorials/how-to-start-command-line/{branch}), the **DotVVM Command Line** tool should be already registered in the project file:

```
<Project ToolsVersion="15.0" Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  ...
  <ItemGroup>
    <DotNetCliToolReference Include="DotVVM.CommandLine" Version="2.0.0" />
  </ItemGroup>
</Project>
```

This will allow to run commands starting with `dotnet dotvvm ...` in the project directory to perform various actions with the DotVVM project.


## Creating Pages, Master Pages and Markup Controls

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

### Examples

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


## Working with REST API Binding clients

> This feature is new in **DotVVM 2.0**.

The DotVVM Command Line tool can also be used to add and update [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/{branch}) clients. 

<table class="table table-bordered">
    <tr>
        <th>Task</th>
        <th>Syntax</th>
        <th>Options</th>
    </tr>
    <tr>
        <td>Add API Client</td>
        <td><code>dotnet dotvvm api create</code></td>
        <td>
            <ul>
                <li><code>{SwaggerJsonUrl}</code> - URL of the Swagger JSON metadata</li>
                <li><code>{Namespace}</code> - namespace in which the API clients will be declared</li>
                <li><code>{CSharpClientPath}</code> - relative path to the generate C# client file</li>
                <li><code>{TypescriptClientPath}</code> - relative path to the generate TypeScript client file</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>Update API Client</td>
        <td><code>dotnet dotvvm api regen</code></td>
        <td>
            <ul>
                <li><code>{SwaggerJsonUrl}</code> - (optional) URL of the Swagger JSON metadata; if not specified, all clients will be regenerated</li>
            </ul>
        </td>
    </tr>
</table>

The metadata of REST API Bindings are stored in `dotvvm.json` file. If any of the parameters needs to be updated, change them in this file.

Please note that the API client needs to be registered in `DotvvmStartup.cs`. See [REST API Bindings](/docs/tutorials/basics-rest-api-bindings/{branch}) chapter for more details.

### Examples

1. Registering the API client:

```
dotnet dotvvm api create http://localhost:43852/swagger/v1/swagger.json DotVVM2.Demo.RestApi.Api Api/ApiClient.cs wwwroot/Scripts/ApiClient.ts
``` 

2. Updating the generated clients:

```
dotnet dotvvm api regen
```
