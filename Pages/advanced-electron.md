# Electron integration
[Electron](https://electron.atom.io/) is a cross platform framework for creating native application with web technologies like JavaScript, HTML and CSS. Electron is powered by **Chromium** and **Node.js**.

**DotVVM** provides integration with Electron. You can write cross platform application with C#. It uses WebSockets to communicate with Electron backend.
Integration with Electron is open source on **[Github](https://github.com/riganti/dotvvm-electron)**.

Integration with Electron is still in **beta**. The API may and probably will be changed.
## Getting Started
___

Required : **Node.js**, **ASP<nolink>.NET Core**

You can use our [**dotnet new template**](https://github.com/riganti/dotvvm-electron/tree/master/_template) for creating a new project.
Or if  you want to setup manually, you must install this packages.
1. Install NuGet package
```POWERSHELL
Install-Package DotVVM.Framework.Integration.Electron
```

2. Add required services with extension method **AddElectronIntegration** in **ConfigureServices** method in your Startup class.
```CSHARP
services.AddElectronIntegration();
```

3. To wire up WebSocket communication with electron you need to add extension method **UseElectronIntegration** in **Configure** method.
```CSHARP
app.UseElectronIntegration();
```

4. Add dependency to **dotvvm-electron** module in your package.json file.
```JSON
  "dependencies": {
    "dotvvm-electron": "~0.0.22-beta1"
    ..
    ..
  }
  ```

  In your main.js/index.js file, which you have defined in your **package.json** as a main/startup script, use dotvvm-electron module:
  ```JS
var dotvvmElectron = require('dotvvm-electron'); 
dotvvmElectron.run(__dirname, {
    relativeWebAppPath: 'webapp/dist/app',
});
  ```

  ```dotvvmElectron.run(__dirname [, options])```

+ ```__dirname``` String - In node.js the [__dirname](https://nodejs.org/docs/latest/api/modules.html#modules_dirname) is global object containing directory name of the current module.

+ ```options``` Object (optional)
  
  * ```relativeWebAppPath``` String (optional) - A relative path to standalone web app **without extension** (extension will be added according to the operating system)
  * ```indexPagePath``` String (optional) - a URL path of the first loaded page
  * ```browserWindowOptions``` [Object](https://electron.atom.io/docs/api/browser-window/#new-browserwindowoptions) (optional) - options passed to new **Browser Window**
  * ```browserWindowCreated``` Function (optional) - function called after creating a **Browser Window**
    * ```browserWindow``` [Object](https://electron.atom.io/docs/api/browser-window/)
  

## Sample
___

First, you have to resolve object **ElectronService** in ViewModel. ElectronService contains all available modules. Modules names corresponds with modules in [Electron API](https://electron.atom.io/docs/api/).

### Open MessageBox

We create method called **ShowMessageBox** in the ViewModel. In this method we are calling method **ShowMessageBox** on **Dialog** module. This call instruct Electron to show MessageBox with given options. 

ViewModel.cs
```CSHARP
public async Task ShowMessageBox()
{
    var options = new ShowMessageBoxOptions
    {
        Title = "New MessageBox"
    };
    await _electronService.Dialog.ShowMessageBox(options);
}
```

Now we just add button to view which will on click call our new method in the ViewModel. 

View.dotvvm
```HTML
<dot:Button Text="Show MessageBox" Click="{command: ShowMessageBox()}" />
```


## Next steps
___

For more information, see the following resources:
+ [List of all available modules and methods](https://github.com/riganti/dotvvm-electron)

+ [More samples](https://github.com/riganti/dotvvm-electron)
