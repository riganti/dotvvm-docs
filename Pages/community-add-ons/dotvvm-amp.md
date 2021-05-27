# DotVVM AMP

![DotVVM.AMP](DotVVM-AMP.png) 

The [DotVVM.AMP](https://www.nuget.org/packages/DotVVM.AMP) NuGet package allows easy automatic conversion of certain DotVVM pages to AMP. 

The page must meet several basic criteria to work without any further configurations or modifications:
1.	No postbacks are allowed.
2.	No external JavaScript is present.
3.	All the CSS code is under 75 kB and does not include any `!important` directives.

When all those criteria are met, then the only thing left to do is to mark the route as AMP ready. The AMP version of your page will be generated alongside the original page with URL prefixed by `amp/{originalUrl}`.

## Configure the AMP support

1.	Add the [DotVVM.AMP](https://www.nuget.org/packages/DotVVM.AMP) package into your application.

2.	Add the following code into `DotvvmStartup.cs`:

```CSHARP
public void Configure(DotvvmConfiguration config, string applicationPath)
{
    ...
    config.AddDotvvmAmp();
}
```

3. Configure the AMP package in `DotvvmStartup.cs`. For example, you can configure how to handle invalid constructs:

```CSHARP
public void ConfigureServices(IDotvvmServiceCollection options)
{
    ...
    options.AddDotvvmAmpSupport((config,_) =>
        {
            config.AttributeHandlingMode = ErrorHandlingMode.LogAndIgnore;
            config.HtmlTagHandlingMode = ErrorHandlingMode.LogAndIgnore;
            config.StyleRemoveForbiddenImportant = true;
        });
    ...
}
```

4. Modify the route table registrations for routes, for which you want to have their AMP version, to use `AddWithAmp` instead of default `Add` method:

```CSHARP
private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
{
    ...

    config.RouteTable.AddWithAmp("Default", "", "Views/ Default.dothtml", config);
    ...
}
```

## Manual tweaks in pages

The DotVVM.AMP can be set up to ignore unsupported constructs automatically, or you can slightly modify the DotVVM view to mark the sections of the page which will be included only in the DotVVM page or only in the AMP version. 

The possible modifications are:
* Exclusion of unsupported parts via `<amp:Exclude>` and `<amp:Include>` controls
* Using resource binding instead of value binding when possible
* Setting error handling mode for the type of error you are getting from `Throw` to `LogAndIgnore`. 

With this setting, the error would be logged and DotVVM.AMP will try to solve the problem, or just ignore it.

## Extensibility

There are several ways how to extend DotVVM.AMP functionality.

### Custom AMP components

The main option is adding support for additional AMP controls via creation of DotVVM.AMP control.

For example, to create your own `iframe` replacement, you can take the following steps:

1. Create the new DotVVM control for rendering an `iframe`. This is done in the same way [how any other control is created](~/pages/concepts/control-development/overview).

2. Register a control tree transformation rule. DotVVM.AMP uses these rules to replace standard components with their AMP variants during page compilation:

```CSHARP
public class IframeTransform : ControlReplacementTransformBase
{
    public IframeTransform(DotvvmAmpConfiguration ampConfiguration, ILogger logger = null ) : base ampConfiguration, logger)
    {
    }

    public override bool CanTransform(DotvvmControl control)
    {
        return control is HtmlGenericControl generic & generic.TagName == "iframe";
    }

    protected DotvvmControl CreateReplacementControl(DotvvmControl con)
    {
        return new AmpIframe();
    }
 }
```

3. Register new transformations into control transforms registry. This is done via DotVVM.AMP configuration:

```CSHARP
options.AddDotvvmAmpSupport((configuration, logger) =>
{
    configuration.ControlTransforms.Register(new IframeTransform(configuration,logger));
});
```

### Modify the internal behavior

If you want to modify internal DotVVM.AMP behavior, you need to reimplement or modify some of its internal classes and register them into the service collection under the corresponding interfaces. The default registrations are done as port of `AddDotvvmAmp` call.

Examples of possible interfaces which could be found useful to reimplement are:

- `IAmpPresenter` 
    - Main connecting element of the DotVVM.AMP.  
    - Registers various preprocessors such as `CssBundlerPreprocessor`.
- `IAmpStylesheetResourceCollection`
    - Handles CSS processing.
- `IAmpOutputRenderer`
    - Controls rendering of the view.
- `IAmpDotvvmViewBuilder`
    - Builds DotVVM view and applies AMP transformations on it.
- `IAmpControlTransformsRegistry`
    - Handles AMP transformations management and application.
- `IAmpValidator`
    - Validates resulting page.
- `IAmpRouteManager`
    - Handles routing and mapping of AMP versions to the original version and visa versa.
- `IAmpExternalResourceMetadataCache`
    - Stores and discovers the metadata regarding the external resources.
    - Mostly used to identify dimensions of external images.

## See also

- [Talk from DotVVM meetup](https://youtu.be/rJfK5t7mWsM?t=3196)
- [Source code](https://github.com/MichalTichy/DotVVM.AMP)
- [Thesis describing DotVVM.AMP in depth (CZECH ONLY)](https://www.vutbr.cz/en/students/final-thesis?zp_id=129088)
