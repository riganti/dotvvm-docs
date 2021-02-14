# Troubleshooting "DotVVM.Compiler.exe not found in the project" Error

If you open a DotVVM project in Visual Studio, sometimes you can see the yellow bar with the following error message:

```DOTHTML
'DotVVM.Compiler.exe' not found in the project YourProjectName!
``` 

Also, IntelliSense in DOTHTML files is broken, the directives are being underlined etc. 

Visual Studio uses the DotVVM Compiler to read the settings in the `DotvvmStartup` file, or to precompile the DOTVVM views.

## How To Resolve the Issue

The most common reason for this behavior is that Visual Studio cannot find the `DotVVM` binaries, because they are not
present in the `packages` folder. 

You can resolve this simply by **building the solution**. In the default settings, the Visual Studio runs the **NuGet package 
restore** before the build, and this operation downloads missing NuGet packages. The `DotVVM` package contains the DotVVM Compiler
binary.

If the package restore doesn't help, you can look at the **Output** window in the Visual Studio. If you display the output from DotVVM,
you'll see the exception the compiler has returned, and the stack trace. Maybe you can figure out what's wrong from the exception -
it can be caused by something you have used in the `DotvvmStartup.cs`. Please, keep in mind, that the DotVVM for Visual Studio has 
to actually execute the `DotvvmStartup.cs` to be able to retrieve the configuration data.    
