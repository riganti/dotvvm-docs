# Explicit assembly loading

> This feature is new from **DotVVM 2.5**.

In order to use attached properties in the [DotHTML markup](~/pages/concepts/dothtml-markup/overview), DotVVM needs to scan the assemblies where these properties are defined. 

This may cause problems, especially in legacy ASP.NET applications where DotVVM is used for [modernization and migration to .NET Core](~/pages/quick-starts/modernize/add-dotvvm-to-existing-app). In many cases, the old projects reference libraries which doesn't exist on the specified location any more, or they are in a different version with a mess in [binding redirects](https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/redirect-assembly-versions). 

The behavior of NuGet in old .NET Framework projects also doesn't help to keep the project reference list clean, as package dependencies are kept in the project even if you uninstall a package which required them and they are not used any more. 

## Configure explicit assemblies

If DotVVM causes the application to fail on startup because of invalid assembly references, and if it cannot be prevented by removing such references (sometimes, these incorrect references come from NuGet packages or various third-party libraries), you can turn explicit assembly mode on and white-list assemblies which DotVVM is allowed to look into.

Place the following code in the `Configure` method in `DotvvmStartup.cs`:

```CSHARP
config.ExperimentalFeatures.ExplicitAssemblyLoading.Enable();

// add all assemblies that are needed in markup
config.Markup.AddAssembly("MyAssembly");
```

## White-list the assemblies

You will need to white-list all assemblies which are used in markup. 

* All assemblies with [custom controls](~/pages/concepts/control-development/overview) (e. g. if you place custom controls in a separate class library project)
* All assemblies containing class declarations, enums, and so on, that are referenced in the markup, either by the `@import` directive, or by a full name (e. g. `MyAssembly.MyNamespace.MyEnum.MyMember`)

You don't need to register `DotVVM.Framework` - it is white-listed automatically.

The easiest way to make sure you haven't missed any of the assemblies, is to use the [Compilation status page](~/pages/upgrading-from-older-versions/compilation-status-page).

## See also

* [Configuration overview](configuration)
* [View compilation modes](view-compilation-modes)
* [Control development](~/pages/concepts/control-development/overview)

