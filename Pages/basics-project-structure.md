# DotVVM Project Contents

After you [create a new DotVVM project](/docs/tutorials/how-to-start-dotnet-451/{branch}), there will be several files in your project:

* **Views\default.dothtml** - an example view.

* **ViewModels\DefaultViewModel.cs** - an example viewmodel.

* **Startup.cs** - an OWIN / ASP.NET Core startup class which registers in the DotVVM and Static Files middleware.

* **Program.cs** - the entry-point of your app (.NET Core projects only).

* **DotvvmStartup.cs** - a DotVVM configuration class (see the [Configuration](/docs/tutorials/basics-configuration/{branch}) chapter).

* **web.config** - a configuration file for ASP.NET and IIS. In ASP.NET Core projects, this file is optional, and is used only when you run the application inside IIS.

<table>
    <tr>
        <td>
            <h5 style="text-align: center">.NET Framework Project</h5>
            <div style="text-align: center">
                <img src="{imageDir}basics-project-structure-img1.png" alt="DotVVM Project Structure (.NET Framework Project)" />
            </div>
        </td>
        <td>
            <h5 style="text-align: center">.NET Core Project</h5>
            <div style="text-align: center">
                <img src="{imageDir}basics-project-structure-img2.png" alt="DotVVM Project Structure (.NET Core Project)" />
            </div>
        </td>
    </tr>
</table>

<br />

## Views and ViewModels Folders

Most people prefer to separate views and viewmodels in the **Views** and **ViewModels** folders. 
Also, there is a naming convention, that the file **default.dothtml** corresponds to the **DefaultViewModel** class.

If you install the [DotVVM for Visual Studio](/landing/dotvvm-for-visual-studio-extension), it will respect that convention, so if you choose to add a new view in the *Views* folder, it will offer to add a viewmodel in the *ViewModels* folder.

However, you can use any convention that suits your needs, e.g. place views together with viewmodels in the same folder.

> In Visual Studio, you can use the **F7** key to navigate from your view to your viewmodel and **Shift-F7** to get back to your view.
