# Generate REST API clients

The [DotVVM Command-Line tool](install) can be used to add and update [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview) clients.

> Even though the DotVVM CLI requires .NET Core, it can be used on .NET Framework projects with OWIN as well, since .NET Core is not a runtime component and is used only on the developer's machine.


## Add an API client

### Syntax

```bash
dotnet dotvvm api [<target>] create [options] <definition>
```

### Arguments

* `[<target>]` - an optional path to the DotVVM project where a new API client should be created. If left unspecified, the current working directory is used.
* `[<definition>]` - path or a URL to an OpenAPI definition.

### Options

* `--namespace <namespace` - the namespace of the generated C# API client.
* `--cs-path <cs-path>` - path to the generated C# client.
* `--ts-path <ts-path` - path to the generated TypeScript client.


## Update an API client

### Syntax

```bash
dotnet dotvvm api [<target>] regen [options] [<definition>]
```

### Arguments

* `[<target>]` - an optional path to the DotVVM project where an API client should be regenerated. If left unspecified, the current working directory is used.
* `[<definition>]` - an optional path or a URL to the OpenAPI definition whose API client should be updated. If left unspecified, all API clients defined in the config file will be regenerated.

### Options

* `--config <config>` - path to the DotVVM API configuration file (`dotvvm-api.json` by default).

## Metadata

The metadata of the REST API Bindings are stored in a `dotvvm-api.json` file. If any of the parameters need to be updated, you can change them there.

Please note that the API client needs to be registered in `DotvvmStartup.cs`. See [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview) chapter for more details.

## Examples

1. Registering the API client:

```bash
dotnet dotvvm api create http://localhost:43852/swagger/v1/swagger.json DotVVM2.Demo.RestApi.Api Api/ApiClient.cs wwwroot/Scripts/ApiClient.ts
``` 

2. Updating the generated clients:

```bash
dotnet dotvvm api regen
```

## See also

* [Install DotVVM CLI](install)
* [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview)
* [Lint DotHTML](lint-dothtml)
* [Create pages and controls](create-pages-and-controls)
