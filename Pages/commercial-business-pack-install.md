# Installing DotVVM Business Pack

To use the [Business Pack](/landing/business-pack) controls, you have use the [DotVVM Private Nuget Feed](/docs/tutorials/commercial-dotvvm-private-nuget-feed/{branch}).

1. Install the `DotVVM.BusinessPack` package from the DotVVM Private Nuget Feed.

2. Open your `DotvvmStartup.cs` file and add the following line at the beginning of the `Configure` method.

```CSHARP
config.AddBusinessPackConfiguration();
``` 

This will register all Business Pack controls under the `<bp:*` tag prefix, and it also registers several Business Pack resources. 

<br />

## Theme Editor

**DotVVM Business Pack** comes with a default theme which is built-in the Nuget package. 

You can get more themes, or craft your own, in the [Business Pack Theme Editor](/docs/tutorials/commercial-business-pack-theme-editor/{branch}). 

<br />

## Limitations

The current version of [DotVVM Business Pack](/landing/business-pack) uses **jQuery 2.1.1** or newer. 
