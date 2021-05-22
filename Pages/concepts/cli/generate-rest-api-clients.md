# Generate REST API clients

The [DotVVM Command Line tool](install) can also be used to add and update [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview) clients.

Even though the DotVVM CLI requires .NET Core, it can be used also for .NET Framework projects with OWIN since it is not a runtime component. It is used only on the developer machines.

## Supported commands

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

The metadata of REST API Bindings are stored in `dotvvm.json` file. If any of the parameters needs to be updated, you can change them in this file.

Please note that the API client needs to be registered in `DotvvmStartup.cs`. See [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview) chapter for more details.

## Examples

1. Registering the API client:

```
dotnet dotvvm api create http://localhost:43852/swagger/v1/swagger.json DotVVM2.Demo.RestApi.Api Api/ApiClient.cs wwwroot/Scripts/ApiClient.ts
``` 

2. Updating the generated clients:

```
dotnet dotvvm api regen
```

## See also

* [Install DotVVM CLI](install)
* [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview)
* [Create pages and controls](create-pages-and-controls)