# View compilation mode

Because of the performance reasons, DotHTML files need to be compiled before their first use. This process is not done during project compilation, but at runtime. Due to this behavior, the first load of page can take slightly longer than subsequent page loads.

In different environments, we may want to modify when the compilation occurs. During the development, we want our application to start as quickly as possible as we compile and run it over and over. Thus, we may not need to compile all the pages. In the production environment, we usually want all the views to be compiled before the first user visits our site.

Due to these differences, the view compilation behavior can be modified using the view compilation mode settings. 

We have three modes whose behavior can be further modified by additional options contained in the `ViewCompilationConfiguration` object.

- `Lazy`
  - Default mode.
  - Views are compiled only when first user accesses the page associated with given view.
  - **This mode should be used for development.**
- `AfterApplicationStart`
  - View are compiled on the background after the application startup routine.  
  - The application will start responding to incoming requests, and start a background task that compiles all the views. If the user enters a page that hasn't been compiled yet, the compilation will be done "on-demand" as in the `Lazy` model
  - **This mode should be used for production environments without deployment slots.**
- `DuringApplicationStart`
  - The views are compiled as part of the application startup routine. This means that the startup takes significantly longer.
    **The application will start to respond to incoming requests after all views are compiled.**
  - *This mode should be used for production environments where slot swapping is used. Slots won't be swapped until the application is ready and starts responding to incoming requests.*

The view compilation modes can be set in the `Configure` method in `Startup.cs` as part of the `UseDotVVM` method call.

```CSHARP
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath,modifyConfiguration:
                configuration =>
                {
                    configuration.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;                 

                });
```

In addition to specifying the compilation mode, the view compilation can be further modified by other settings in `ViewCompilationConfiguration`.

When using `DuringApplicationStart` and `AfterApplicationStart` mode, you can set whether the compilation will be done in parallel using the `CompileInParallel` option.  Setting this property to `false` can be helpful when running on machine with low number of cores.

When using `AfterApplicationStart`, you can use the `BackgroundCompilationDelay` option to defer the compilation. This setting is helpful when there are other more important things done after the application startup (populate caches and more).

```CSHARP
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath,modifyConfiguration:
                configuration =>
                {
                    configuration.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;
                    configuration.Markup.ViewCompilation.CompileInParallel = false;
                    configuration.Markup.ViewCompilation.BackgroundCompilationDelay = TimeSpan.FromMinutes(5);

                });
```
