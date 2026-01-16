# Testing controls

From DotVVM 4.0, there is a new NuGet package called `DotVVM.Testing` which helps control developers to write unit test for DotVVM controls.

> Please note that this package helps only to make sure the server-side part of the control works and that the control renders a correct HTML code. For proper end-to-end testing of DotVVM controls, you will need to use the control in an actual application and use tools like Selenium to interact with the control.

## Helper classes

The `DotvvmTestHelper` class has static methods `CreateConfiguration` and `CreateContext` to create mocked  `DotvvmConfiguration` and `IDotvvmRequestContext` for the purpose of testing.

The `ControlTestHelper` class is useful for testing custom controls. There is a `RunPage(typeof(MyViewModel), "your dothtml code")` which executes the DotVVM page and returns the HTML output parsed using the `AngleSharp` library.

```
static readonly ControlTestHelper cth = new ControlTestHelper(config: config => {
    config.Markup.AddCodeControls("cc", exampleControl: typeof(MyControl));
});

[TestMethod]
public async Task BasicWrappedHtmlControl()
{
    var r = await cth.RunPage(typeof(BasicTestViewModel), """
            <cc:MyControl MyProperty="my-class" />
        """
    );
    Assert.IsNotNull(r.Html.QuerySelector("a.my-class"));
}
```

We can also trigger commands using the test helper `RunCommand` method. It will automatically store the viewmodel changes in `r.ViewModelJson`, allowing us to verify the command executed successfully.

```
[TestMethod]
public async Task BasicWrappedHtmlControl()
{
    var r = await cth.RunPage(typeof(BasicTestViewModel), """
            <cc:MyControl MyCommand={command: TestMethod()} />
        """
    );
    r.RunCommand("TestMethod()"); // note there must be just one command with this string
    Assert.AreEqual(321, (int)r.ViewModelJson["MyProperty"]);
}
```

For more detailed example, see the [CompositeControlTests.cs](https://github.com/riganti/dotvvm/blob/main/src/Tests/ControlTests/CompositeControlTests.cs#L32) class in the DotVVM framework codebase - it shows how the control tests look like.

`BindingTestHelper` is useful for testing custom method translations - there are methods to create various bindings and compile them to JS.

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Composite controls](composite-controls)
* [Control properties](composite-controls)
