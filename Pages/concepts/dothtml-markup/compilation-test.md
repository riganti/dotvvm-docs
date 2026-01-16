# Compilation test

During development or upgrading from previous versions of DotVVM, it is useful to ensure that files with DotHTML markup do not have any compile errors. 

To look at the issues in markup files, you can use the [Compilation Status Page](~/Pages/upgrading-from-older-versions/compilation-status-page)

We also recommend to add a test that runs markup compilation on all pages and fails if there are any errors. To make this even easier, you can use `WebApplicationFactory<TStartup>` from the `Microsoft.AspNetCore.Mvc.Testing` NuGet package as seen below: 

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

* [Compilation status page](~/Pages/upgrading-from-older-versions/compilation-status-page)
* [DotHTML markup](~/pages/concepts/dothtml-markup/overview)
* [Common control properties](~/pages/concepts/dothtml-markup/common-control-properties)
* [Data-binding](~/pages/concepts/data-binding/overview)
