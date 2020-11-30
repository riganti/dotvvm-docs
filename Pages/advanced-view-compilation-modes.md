# View compilation modes

DotHTML needs to be compiled before serving it to the client. This process is not done durning project compilation but in runtime. Due to this behavior first load of page can take slightly longer than subsequent page loads.

In different environments we may want to modify when the compilation will occur. During development we want our application to startup as fast as possible, and we may not need to compile all the pages. But in production environment we usually want all the views to be compiled before first user visits our page.

Due to this reasons the view compilation behavior can be modified using compilation modes. 
We have 3 basic modes whose behavior can be further modified by additional options contained in `ViewCompilationConfiguration`.

- Lazy
  - Default mode.
  - Views are compiled only when first user accesses the page associated with given view.
  - **This mode should be used for development.**
- DuringApplicationStart
  - Views are compiled before application startup is done.  
    **The application will start to respond to incoming requests after all views are compiled.**
  - *This mode should be used for production environments where we are using slot swapping. Slots wont be swapped until the application is ready and starts to respond to incoming requests.*
- AfterApplicationStart
  - View are compiled after application startup.  
    The application will start to respond to incoming requests and than will start view compilation.
  - **This mode should be used for production environments.**

The view compilation modes are being set in `Configure` method in `Startup.cs` as part of `UseDotVVM` method call.


```CSHARP
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath,modifyConfiguration:
                configuration =>
                {
                    configuration.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;                 

                });
```

In addition to view compilation modes the view can be further modified by other settings in  `ViewCompilationConfiguration`.

When using `DuringApplicationStart` and `AfterApplicationStart` mode than set whether the compilation will be done in parallel or not using the `CompileInParallel` option. Setting this to *false* can be helpful when running on machine with low number of cores.

When using `AfterApplicationStart` than by setting the `BackgroundCompilationDelay` option you can defer the compilation. This setting is helpful when there are other more important things done during application startup.

```CSHARP
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath,modifyConfiguration:
                configuration =>
                {
                    configuration.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;
                    configuration.Markup.ViewCompilation.CompileInParallel = false;
                    configuration.Markup.ViewCompilation.BackgroundCompilationDelay = TimeSpan.FromMinutes(5);

                });
```