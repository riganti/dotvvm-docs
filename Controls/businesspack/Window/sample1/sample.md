## Sample 1: Basic Usage

The content inside the `<bp:Window>` element is treated as a `ContentTemplate` property which specifies the main content of the window.

The `IsDisplayed` property is bound to the `bool` property in the viewmodel which controls whether the window is shown or hidden. When the user hides the window, the property is set to `false` automatically.
