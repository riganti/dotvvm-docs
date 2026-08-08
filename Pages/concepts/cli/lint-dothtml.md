# Lint DotHTML

The [DotVVM Command-Line tool](install) can be used to lint (list compiler errors and warnings in) [pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), and [markup controls](~/pages/concepts/control-development/markup-controls) in both ASP.NET Core and OWIN projects without running them.

## Syntax

```bash
dotnet dotvvm lint [options] [<target>]
```

## Arguments

* `[<target>]` - an optional path to the DotVVM project to lint. If left unspecified, the current working directory is used.

## Options

* `--no-color` - disables ANSI colors in diagnostic output. By default, file names, errors, warnings, and affected markup are colorized to improve readability.
* `--no-build` - the `<target>` project is built with `dotnet build` by default. This switch disables that behavior. The `<target>` project must be built before running this project.
* `--verbose-build-output` - shows the MSBuild output from the restore and build steps.
* `--configuration <configuration>` - the configuration used to build the `<target>` project or to locate its binary if `--no-build` is present. Is set to `Debug` by default.
* `--framework <framework>` - the [target framework](https://docs.microsoft.com/en-us/dotnet/standard/frameworks) used to build the `<target>` project or to locate its binary if `--no-build` is present. If left unspecified, the first entry in the `TargetFrameworks` (or `TargetFramework`) MSBuild property is used.

## Output

Diagnostics include the markup file and location.

```text
Views/Errors/MissingViewModel.dothtml(1,0): error: The @viewModel directive is missing in the page 'Views/Errors/MissingViewModel.dothtml'!
    1: <!DOCTYPE html>
    2: <html>
    3: <body>
    4:     Content
    5: </body>
    6: </html>
```

## Application startup

The lint command does not run `Startup.cs` or `Program.cs`. It only looks for `DotvvmStartup.cs` and runs `Configure` and `ConfigureServices` to discover controls and register services related to DotVVM.

The linter may fail if `ConfigureServices` (in `DotvvmStartup`) works with services that depend on services registered elsewhere. In such case, you will need to move the dependencies to `DotvvmStartup` service registrations, or you can use the `IsDotvvmCompiler` property of `IDotvvmServiceCollection` to detect whether the code is invoked from the linter, and skip registering services with these outer dependencies.

## See also

* [Install DotVVM CLI](install)
* [Create pages and controls](create-pages-and-controls)
* [Generate REST API clients](generate-rest-api-clients)
