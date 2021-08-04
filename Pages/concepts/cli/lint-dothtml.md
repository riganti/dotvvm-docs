# Lint DotHTML

The [DotVVM Command-Line tool](install) can be used to lint (list compiler errors and warnings in) [pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), and [markup controls](~/pages/concepts/control-development/markup-controls) in both ASP.NET Core and OWIN projects without running them.

## Syntax

```bash
dotnet dotvvm lint [options] [<target>]
```

## Arguments

* `[<target>]` - an optional path to the DotVVM project where a new API client should be created. If left unspecified, the current working directory is used.

## Options

* `--no-build` - the `<target>` project is built with `dotnet build` by default. This switch disables that behavior. The `<target>` project must be built before running this project.
* `--configuration <configuration>` - the configuration used to build the `<target>` project or to locate its binary if `--no-build` is present. Is set to `Debug` by default.
* `--framework <framework>` - the [target framework](https://docs.microsoft.com/en-us/dotnet/standard/frameworks) used to build the `<target>` project or to locate its binary if `--no-build` is present. If left unspecified, the first entry in the `TargetFrameworks` (or `TargetFramework`) MSBuild property is used.

## See also

* [Install DotVVM CLI](install)
* [Create pages and controls](create-pages-and-controls)
* [Generate REST API clients](generate-rest-api-clients)
