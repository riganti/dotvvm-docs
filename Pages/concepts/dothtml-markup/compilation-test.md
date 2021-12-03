# Compilation test

In case you want to be sure that all your .dohtml, .dotcontrol and .dotmaster pages compile before you publish them to your live website you can write a simple test, that will check the compilation of your pages using the `IDotvvmViewCompilationService` that is registered to service collection as a part of DotVVM framework. 

To make this even easier you can use `WebApplicationFactory<TStartup>` from the `Microsoft.AspNetCore.Mvc.Testing` NuGet package as seen below: 

```CSHARP
using DotVVM.Framework.Compilation;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace WebApplication.Tests;

public class ViewCompilationTests
{
    [Fact]
    public async Task CompileAllViews_Succeeds()
    {
        //Arrange
        var webApplicationFactory = new WebApplicationFactory<Startup>();
        var dotvvmViewCompilationService = webApplicationFactory.Services.GetRequiredService<IDotvvmViewCompilationService>();

        //Act
        var wasCompilationSuccessful = await dotvvmViewCompilationService.CompileAll();
        
        //Assert
        Assert.True(wasCompilationSuccessful);
    }
}

```