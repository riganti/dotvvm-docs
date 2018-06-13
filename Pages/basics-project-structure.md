# DotVVM Project Contents

After you [create a new DotVVM project](/docs/tutorials/how-to-start-dotnet-451/{branch}), there will be several files in your project:

* **Views\default.dothtml** - an example view.

* **ViewModels\DefaultViewModel.cs** - an example viewmodel.

* **Startup.cs** - an OWIN startup class which registers in the DotVVM and Static Files middleware.

* **DotvvmStartup.cs** - a DotVVM configuration class (see the [Configuration](/docs/tutorials/basics-configuration/{branch}) chapter).

* **web.config** - a configuration file for ASP.NET and IIS.

<img src="{imageDir}basics-project-structure-img1.png" alt="DotVVM Project Structure" />

<br />

# Views and ViewModels Folders

Most people prefer to separate views and viewmodels in the **Views** and **ViewModels** folders. 
Also, there is a naming convention, that the file **default.dothtml** corresponds to the **DefaultViewModel** class.

If you install the [DotVVM for Visual Studio](/landing/dotvvm-for-visual-studio-extension), it will respect that convention, so if you choose to add a new view in the *Views* folder, it will offer to add a viewmodel in the *ViewModels* folder.

However, you can use any convention that suits your needs, e.g. place views together with viewmodels in the same folder.

> In Visual Studio, you can use the **F7** key to navigate from your view to your viewmodel and **Shift-F7** to get back to your view.
