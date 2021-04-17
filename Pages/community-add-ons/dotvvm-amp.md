# DotVVM AMP
![DotVVM.AMP](DotVVM-AMP.png) 
[DotVVM.AMP](https://www.nuget.org/packages/DotVVM.AMP) nuget package allows easy automatic conversion of certain DotVVM pages to AMP pages. 

The page must meet several basic criteria to work without any further configurations / modifications:
1.	No postbacks ale allowed.
2.	No external JavaScript is present.
3.	All the CSS code is under 75 kB and does not include any !important directives.

When all those criteria are met, then the only thing left to do is to mark the route as AMP ready. The AMP version of your page will be generated alongside the original page with URL prefixed by `amp/{originalUrl}`.

## Enabling AMP conversion.
1)	Add [DotVVM.AMP](https://www.nuget.org/packages/DotVVM.AMP) package into your application.
2)	Modify DotvvmStartup.cs

    1)  Call `AddDotvvmAmp` on `DotvvmConfiguration` instance.
        ```
            public void Configure(DotvvmConfiguration config,   string applicationPath)
            {
                ...
                config.AddDotvvmAmp();
                ...
            }
        ```
    2)  Call `AddDotvvmAmpSupport` on `IDotvvmServiceCollection` instance.  
    In this method you can configure DotVVM.AMP. You can for example configure how invalid constructs are handled.
        ```
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
    3)   Modify route table registrations for routes, for which you want to have their AMP version, to use `AddWithAmp` instead of default Add method.
            ```
            private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
            {
                ...
            
                config.RouteTable.AddWithAmp("Default", "", "Views/ Default.dothtml", config);
                ...
            }
            ```

## Converting complex pages
The DotVVM.AMP can be set up to ignore invalid parts and the DotVVM view can be slightly modified to mark the sections of the page which will be included only in the DotVVM page or only in the AMP version. 

Possible modifications are:
-	exclusion of unsupported parts via `<amp:Exclude>` and `<amp:Include>` controls
-	using resource binding instead of value binding when possible
-	setting error handling mode for the type of error you are getting from `Throw` to `LogAndIgnore`.  
With this setting the error would be logged and DotVVM.AMP will try to solve or ignore the problem.

## Extensibility
There are several ways how to extend DotVVM.AMP functionality.

### Custom AMP components

The main one is adding support for additional amp controls via creation of DotVVM.AMP control.

If we wanted to create our own iframe replacement, then we need to:
1. Create the new iframe control. This is done in the same way how any other control is created.
2. Create control tree transformation rule.  
DotVVM.AMP uses those rules to replace standard components with their AMP variant during page compilation.
```C#
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
3. Register new transformation into control transforms registry.  
This is done via amp configuration.
```C#
options.AddDotvvmAmpSupport((configuration, logger) =>
{
    configuration.ControlTransforms.Register(new IframeTransform(configuration,logger));
});

```

### Modification to internal behavior
If we want to modify internal DotVVM.AMP behavior than we need to reimplement/modify some of its internal classes and register them into IOC under the corresponding interfaces.

The default registrations are done as port of `AddDotVVMSupport` call.

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


## Additional resources
- [Talk from DotVVM meetup](https://youtu.be/rJfK5t7mWsM?t=3196)
- [Source code](https://github.com/MichalTichy/DotVVM.AMP)
- [Thesis describing DotVVM.AMP in depth (CZECH ONLY)](https://www.vutbr.cz/en/students/final-thesis?zp_id=129088)