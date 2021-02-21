# Troubleshooting DotVVM for Visual Studio

If you encounter any problems with the DotVVM extension (e.g. IntelliSense not working), try to do the following steps.

## IntelliSense is not working

### 1. Update to Latest Version

In Visual Studio, choose the **DotVVM / Check for Updates** menu item and if newer version is available, install it.

### 2. Check You Are Signed In

Some features from the **Professional** edition work only if you are signed in. 
In Visual Studio, choose the **DotVVM / About** and check whether your license is active and whether you are signed in.

### 3. Delete the Component Model Cache

Occasionally, some things break in Visual Studio MEF component cache. If you see message boxes that DotVVM package
didn't load correctly, shut down Visual Studio and delete the folder `c:\Users\<your user name>\AppData\Local\Microsoft\VisualStudio\v14.0\ComponentModelCache`
from Windows Explorer.

## "DotVVM.Compiler.exe not found in the project"

If you open a DotVVM project in Visual Studio, sometimes you can see the yellow bar with the following error message:

```DOTHTML
'DotVVM.Compiler.exe' not found in the project YourProjectName!
``` 

Also, IntelliSense in DOTHTML files is broken, the directives are being underlined etc. 

Visual Studio uses the DotVVM Compiler to read the settings in the `DotvvmStartup` file, or to precompile the DOTVVM views.

### How To Resolve the Issue

The most common reason for this behavior is that Visual Studio cannot find the `DotVVM` binaries, because they are not
present in the `packages` folder. 

You can resolve this simply by **building the solution**. In the default settings, the Visual Studio runs the **NuGet package 
restore** before the build, and this operation downloads missing NuGet packages. The `DotVVM` package contains the DotVVM Compiler
binary.

If the package restore doesn't help, you can look at the **Output** window in the Visual Studio. If you display the output from DotVVM,
you'll see the exception the compiler has returned, and the stack trace. Maybe you can figure out what's wrong from the exception -
it can be caused by something you have used in the `DotvvmStartup.cs`. Please, keep in mind, that the DotVVM for Visual Studio has 
to actually execute the `DotvvmStartup.cs` to be able to retrieve the configuration data.    

## Other issues

If the issue still occurs, <a href="/support">please contact us</a> and provide us with description of the problem and screenshots.
