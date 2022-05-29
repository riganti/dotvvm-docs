# Testing controls

From DotVVM 4.0, there is a new NuGet package called `DotVVM.Testing` which helps control developers to write unit test for DotVVM controls.

> Please note that this package helps only to make sure the server-side part of the control works and that the control renders a correct HTML code. For proper end-to-end testing of DotVVM controls, you will need to use the control in an actual application and use tools like Selenium to interact with the control.

## Helper classes

The `DotvvmTestHelper` class has static methods `CreateConfiguration` and `CreateContext` to create mocked  `DotvvmConfiguration` and `IDotvvmRequestContext` for the purpose of testing.

The `ControlTestHelper` class is useful for testing custom controls. There is a `RunPage(typeof(MyViewModel), "your dothtml code")` which executes the DotVVM page and returns the HTML output parsed using the `AngleSharp` library.

See the [CompositeControlTests.cs](https://github.com/riganti/dotvvm/blob/main/src/Tests/ControlTests/CompositeControlTests.cs#L32) class in the DotVVM framework codebase - it shows how the control tests look like. 

`BindingTestHelper` is useful for testing custom method translations - there are methods to create various bindings and compile them to JS.

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Composite controls](composite-controls)
* [Control properties](composite-controls)