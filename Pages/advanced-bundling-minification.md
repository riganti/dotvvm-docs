# Bundling and Minification

In DotVVM, each control in the page can request some resources (scripts and stylesheets) to be added in the `head` or `body` elements. When the page HTML is rendered, DotVVM takes all such resources, sorts them so all their dependencies are met, and then adds the `script` and `link` elements in the page. 

To extend this mechanism, the `DotvvmConfiguration` object allows you to register custom objects which implement the `IResourceProcessor` interface. These objects can perform additional actions with the collection of resources that will be rendered. 

## Bundling

This can be used e.g. for bundling. You can bundle several resources in one file and configure DotVVM to include the bundle in the page instead of including the individual resources. You can use the `BundlingResourceProcessor` class for this. 

Please note that this class doesn't do the bundling automatically. You need to combine the script files or stylesheets yourself and register it as a new resource, which you specify in the bundle registration.

```CSHARP
var bundling = new BundlingResourceProcessor();
bundling.RegisterBundle(dotvvmConfiguration.Resources.FindNamedResource("myBundle"), "script1", "script2");
dotvvmConfiguration.Resources.DefaultResourceProcessors.Add(bundling);
```

For example, if any control requests the `script1` resource, DotVVM will insert the entire bundle in the page instead of the individual `script1` resource file.  

## Minification

Currently, DotVVM doesn't help with minification of resources. However, you can write your own `IResourceProcessor` that will replace debug resources with the minified ones when running in the production environment.