# Installing DotVVM Business Pack

To use the [Business Pack](/landing/business-pack) controls, you have use the [DotVVM Private Nuget Feed](/docs/tutorials/commercial-dotvvm-private-nuget-feed/{branch}).

1. Install the `DotVVM.BusinessPack` package from the DotVVM Private Nuget Feed.

2. Open your implementation of `IDotvvmServiceConfigurator` (typically at `DotvvmStartup.cs` file) and add the following line at the beginning of the `ConfigureService` method.

```CSHARP
   public void ConfigureServices(IDotvvmServiceCollection options)
   {
        options.AddBusinessPack();
   }
``` 

This will register all Business Pack controls under the `<bp:*` tag prefix, and it also registers several Business Pack resources. 



## Theme Editor

The Theme editor is currently not supported for Business Pack 2.x.



## Limitations

The current version of [DotVVM Business Pack](/landing/business-pack) uses **jQuery 2.1.1** or newer. 
