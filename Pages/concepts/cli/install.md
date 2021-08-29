# Install the CLI

The **DotVVM Command-Line tool** can be used for several purposes:

* To [lint](lint-dothtml) `*.dothtml`, `*.dotmaster`, and `*.dotcontrol` files and reveal errors without having to run the application.
* To add [pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), or [markup controls](~/pages/concepts/control-development/markup-controls) to the project.
* To generate [REST API bindings](~/pages/concepts/respond-to-user-actions/rest-api-bindings/overview).

> In the future, we plan to add additional features, like generating Selenium page objects for DotHTML pages, and more.

## Install the CLI tool

To install DotVVM CLI globally, run the following command in the terminal:

```bash
dotnet tool install -g DotVVM.CommandLine
```

## Remove the DotNetCliToolReference

If you've been using ASP.NET Core with .NET Core 2.2 or lower, you'll probably have the following entry in the `.csproj` file:

```xml
  <ItemGroup>
    <DotNetCliToolReference Include="DotVVM.CommandLine" Version="2.0.0" />
  </ItemGroup>
```

This can be safely removed, as the `DotNetCliToolReference` was deprecated, and installing command-line tools via `dotnet tool install` is now the preferred way.

## See also

* [Lint DotHTML](lint-dothtml)
* [Create pages and controls](create-pages-and-controls)
* [Generate REST API clients](generate-rest-api-clients)
