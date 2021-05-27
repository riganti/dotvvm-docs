# View compilation mode

> This feature is new from **DotVVM 2.5**.

Because of the performance reasons, all [DotHTML files](~/pages/concepts/dothtml-markup/overview) (pages, markup controls, master pages) need to be compiled before their first use. This process is not done during project compilation, but at runtime, as it needs to access the [configuration](overview) which is defined in C# code (the application must be launched in order to retrieve the control registrations, route names, and so on). 

Because of that, the first load of the page can take slightly longer than subsequent page loads.

In different environments, you may want to modify when the compilation takes place:

* For the development inner loop, we want our application to start as quickly as possible, as we compile and run it over and over. Thus, we may not need to compile all the pages. 
* In production, we usually want all the views to be compiled before the user visit our site.

Due to these differences, the view compilation behavior can be modified using the view compilation mode settings. There are three modes, and their behavior can be further modified by additional options contained in the `ViewCompilationConfiguration` object.

## Lazy mode

This is the default compilation mode. The views are compiled only when first user accesses the page that uses them. 

**This mode is great for development inner loop.** It can also be used in production if there is not a massive traffic on the site.

## AfterApplicationStart mode

The views are compiled on the background after the application startup routine. The application will start responding to incoming HTTP requests as soon as possible, and a background task which compiles all the views will be starter. 

If uses enter a page that hasn't been compiled yet, the compilation will be done "on-demand", same as in the `Lazy` mode.

**This mode is ideal for simple production environments which do not use deployment slots.** It has the advantages of the default mode (fast application startup), and thanks to the background compilation of the pages, it decreases the chance that users who hit the page for the first time will need to wait for the compilation.

## DuringApplicationStart mode

In this mode, the views are compiled as part of the application startup procedure. 

This means that the __startup takes significantly longer__, and **the application will start to respond to incoming HTTP requests only after all views are compiled.**

**This mode should be used only in environments where deployment slot swapping is used. The slots won't be swapped until the application is ready and responds to the first HTTP request.**

## Configuration options

The view compilation mode can be set in the `Configure` method in `Startup.cs` as part of the `UseDotVVM` method call.

```CSHARP
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath, modifyConfiguration:
    config =>
    {
        if (!config.Debug) 
        {
            config.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;
        }
    });
```

In addition to specifying the compilation mode, the view compilation can be further modified by other settings in `ViewCompilationConfiguration`.

When using `DuringApplicationStart` and `AfterApplicationStart` mode, you can set whether the compilation will be done in parallel using the `CompileInParallel` option. Setting this property to `false` can be helpful when the app is running on machine with a small number of CPU cores.

When using `AfterApplicationStart`, you can use the `BackgroundCompilationDelay` option to defer the compilation. This setting is helpful if there are other initialization tasks done after the application startup (e. g. populating caches).

```CSHARP
var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath, modifyConfiguration:
    config =>
    {
        if (!config.Debug) 
        {
            config.Markup.ViewCompilation.Mode = ViewCompilationMode.AfterApplicationStart;

            // we are running on a machine with only 2 CPU cores, and the apps loads some data from the database after the startup
            // compile on one core and delay it for 30 seconds
            config.Markup.ViewCompilation.CompileInParallel = false;
            config.Markup.ViewCompilation.BackgroundCompilationDelay = TimeSpan.FromSeconds(30);
        }
    });
```

## See also

* [Configuration overview](overview)
* [Explicit assembly loading](explicit-assembly-loading)

