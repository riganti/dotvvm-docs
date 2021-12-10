# Compilation test

In case you want to be sure that all your DotHTML files compile before you publish them to your live website, you can write a test that will check the compilation of your pages using `IDotvvmViewCompilationService`. 

To make this even easier, you can use `WebApplicationFactory<TStartup>` from the `Microsoft.AspNetCore.Mvc.Testing` NuGet package as seen below: 

```CSHARP
using DotVVM.Framework.Compilation;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;    // in case you are using MSTest instead of XUnit

namespace WebApplication.Tests;

// [TestClass] in case you are using MSTest instead of XUnit
public class ViewCompilationTests
{
    [Fact]
    // [TestMethod] in case you are using MSTest instead of XUnit
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

## See also

* [DotHTML markup](~/pages/concepts/dothtml-markup/overview)
* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)