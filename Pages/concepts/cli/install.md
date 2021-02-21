# Install the CLI

When you create DotVVM project (ASP.NET Core) using [DotVVM for Visual Studio](https://www.dotvvm.com/landing/dotvvm-for-visual-studio-extension) or using [dotnet new](/docs/tutorials/how-to-start-command-line/{branch}), the **DotVVM Command Line** tool should be already registered in the project file:

```
<Project ToolsVersion="15.0" Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  ...
  <ItemGroup>
    <DotNetCliToolReference Include="DotVVM.CommandLine" Version="2.0.0" />
  </ItemGroup>
</Project>
```

This will allow to run commands starting with `dotnet dotvvm ...` in the project directory to perform various actions with the DotVVM project.

