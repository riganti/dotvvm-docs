---
name: dotvvm-control-docs
describe: Guidelines for creating documentation for DotVVM controls.
---

## Docs repo structure

The documentation repository contains the `Controls` folder which has subfolders for every component package:
* `builtin` for controls in DotVVM framework
* `bootstrap3`, `bootstrap4` or `bootstrap5` for Bootstrap for DotVVM
* `business-pack` for DotVVM Business Pack
* `tailwind` for DotVVM Tailwind UI

## Component package folder contents
This subfolder then contains the following files that are generated during the compilation of components:
* `doc.xml` - generated metadata from the `/// <summary>...</summary>` comments in code
* `metadata.json` - outpuf of DotVVM control metadata generator that scans the assembly and discovers all controls and their properties
These files are auto-generated and there is no reason to edit them manually.

For every control, there must be a folder named as the control (e.g. `Accordion`, `Button`).

## Control folder structure

* Every control folder must contain the `control.md` file which contains a brief description of the control functionality. One or two sentences are enough.
* Optionally, there is `output.md` file explaining what HTML the control renders. This is relevant only for `builtin` components because the users style them themselves. For commercial component packages, the users don't care what the control renders, as it comes with styles already.

Then, there are one or more samples for every control. The samples should cover all functionality of the control, especially:
* Show all use-cases and properties of the component, for example:
  * if the control can be bound by the `DataSource` property or its items may be specified manually inside the control, there should be one sample for each of those two usages.
  * if the user can specify either a text or a template, we should show both usages - you can either make two separate samples, or use the control twice in the sample, once with `HeaderText` and once with  `HeaderTemplate`
  * all properties the control offers should be used at least once
* Reasonable number of samples - merge related properties into a single sample
  * if the control uses header, content, and footer, there is no need to have three separate samples for each - make one sample with `HeaderText`, `FooterText`, and the other with `HeaderTemplate` and `FooterTemplate`
  * if the controls uses `DataSource`, we can show the usage of all `Item*` properties together in a single sample.
* Keep the samples simple and short
  * for enum properties, there is no need to show all possible values of the enum - pick a few examples; the only exception is the semantic coloring of buttons and other elements - the user usually wants to see all options they have
  * when showing grids, two or three columns are fine - the samples are shown in an iframe of ~1000px width and ~600px height, so were are limited in space
  * ideal sample has no more than 20 lines of DotHTML markup and up to 50 lines of C# code (if needed); you may break these limits only when there is no other option (such as components that guide entire page layout and have multiple templates or child components)
* Interactivity
  * when showing interactive controls, the sample should do something - we don't need a button that does nothing when the user click on it
  * when showing controls with routes or hyperlinks, we should not point away from the sample app - either use the current URL and play with query string (and display it so the user can see what happened), or link to another sample in the same folder
* Extending framework controls
  * if the control only extends some framework controls, the samples should cover only the added functionality or styling, and the `sample.md` should link to the base control (usually in the `builtin` section).

Each sample has a folder that is named `sample1`, `sample2`, and so on.

## Sample structure

The sample folder must contain `sample.md` file that briefly describe the control. Example:

```
## Sample 1: Basic Usage

The `Button` control has a `Text` property that represents the text displayed in the button. If you need to use HTML elements inside the button, you can place the content inside the `Button` control instead.

In the `Click` event you can specify which method will be called in the viewmodel.
```

Then, there is `sample.json` specifying list of files:
```
{
  "files": [
    { "name": "page.dothtml" },    
    { "name": "ViewModel.cs" }
  ]
}
```

The folder than must contain these files that contain DotHTML markup or a C# code of the page viewmodel.

* `.dothtml` files
  * Can be only the fragment of the markup, not a complete page. The `@viewModel` and other directives are specified only when they are needed for the sake of the sample (most controls don't need them)
  * When sample app is generated, the file is embedded in a HTML template which contains the `<body>` element - assume that the content of the file will be somewhere in the page body.
  * No need to apply any styles and CSS classes, unless it is necessary for the sake of the example.
* `.cs` files
  * Use this structure:
    ```
    using DotVVM.Framework.ViewModel;

    namespace DotvvmWeb.Views.Docs.Controls.builtin.Button.sample1
    {
        public class ViewModel : DotvvmViewModelBase
        {
            ...
        }
    }
    ```
  * File scoped namespaces are not supported.
  * Assume the project has Nullable=disable, is compiled against .NET 8 uses C# language version 14.
  
## Inferring control properties and usage patterns

When looking at the control source code, the properties and usage can be inferred from these rules:

* `CompositeControl` - properties are defined as parameters of the `GetContents` method:
    * if the property is nullable or has default value, it is optional; otherwise, it is required
    * if the property is `ValueOrBinding<T>`, it supports both data-binding (`<dot:Button Text={value: SomeBinding} />`) and hard-coded values (`<dot:Button Text="Save" />`)
    * if the property is `IValueBinding<T>`, it supports only data-binding
    * primitive types (`string`, enums, numbers, dates etc). support only hard-coded values
    * other types - see below
* other base classes - properties are defined using this pattern:
    ```
    [MarkupOptions(Reguired = true, AllowBinding = false)] 
    public Size Size
    {
        get { return (Size)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }
    public static readonly DotvvmProperty SizeProperty =
        DotvvmProperty.Register<Size, DataPager>(c => c.Size, Size.Default);
    ```
    * by default, both data-binding and hard-coded values are supported; these can be turned off using `MarkupOptions` attribute.
* properties of type `ITemplate` or `List<DotvvmControl>` means that the property is specified as an inner content:
        ```
        <dot:Repeater>
            <ItemTemplate>
                some content 
            </ItemTemplate>
        </dot:Repeater>
        ```
        * the control can have default property (specified in `ControlMarkupOptions` attribute) - in such case, the `ItemTemplate` in the previous sample could be omitted and the content could go directly in the repeater
* properties of `ICommandBinding`, `Command`, `Action`, `Func` support only binding and are used like this: `<dot:Button Click={command: SomeCommand} />`
* capabilities (the type is marked with `DotvvmControlCapability` attribute) are groups of properties that belong together. You need to inspect the type to see what properties it contains. The properties of the capability are treated like they were defined on the control itself - for example, `TextOrContentCapability` adds `Text` and `Content` properties on the control.
* if the property is marked with `DotvvmControlCapability(prefix)`, it means that is properties are added to the control with a prefix - for example, the capability contains `Text` property, but it is exposed as `ItemText` on the control
* if the property contains data context change attributes, it means that the binding context changes (for example, in controls with `DataSource`, some bindings are evaluated on elements of the collection) - example: `<dot:ComboBox DataSource="{value: Countries}" ItemTextBinding="{value: CountryName}" />` where `CountryName` is a property of objects in the `Countries` collection. You can use `_parent` variable in the binding to get to the parent binding context if needed.

### XML docs on control properties

* All DotVVM controls should have the `/// <summary>...</summary>` doc comment
* All control properties (declared as properties) should have the `/// <summary>...</summary>` doc cumment
* In composite controls, we document parameters of `GetContents` method using `/// <param name="type">...</param>
* Properties declared in capabilities should also have the `/// <summary>...</summary>` doc comment
* If the control uses a capability, the comment of the capability property may be too generic and not entirely relevant for the particular usage of the control. To override the comment for the property coming from the capability, use this custom doc comment on the `GetContents` method: /// <capability-param name="headerText">Gets or sets the text in the control header.</capability-param>
* Don't use `<remarks>`, `<see cref=...>` and other .NET documentation elements - our docs website cannot render them. To reference controls or property names, use backtick as you'd do in Markdown.

## Advanced usages - preprocessing when generating sample app
The samples are used to generate a standalone app that has a page for every sample, and can be run locally. 

Sometimes, the documentation needs to show something different than what must be in the real sample to be functional - this is the case of `ContentPlaceHolder` which need to have a page and a master page. 

The `sample.json` file may specify regex replacements that are applied when the sample app is being generated. This is used only rarely - most controls dont need it. 

There is a variable named `%SampleUniqueName%` that contains the current name of the sample. The control may export registration to `DotvvmStartup.cs` to register additional routes, controls, and so on - this is done by creating a file `DotvvmStartup.cs` - it implements `IDotvvmStartup` and speficies `Configure` method to add entries to DotVVM configuration.

Example: 

```
{
  "files": [
    {
      "name": "master.dotmaster",
      "replacements": [
        {
          "pattern": "(\\<!.*\\<body\\>)",
          "replacement": "",
          "singleLine": true
        },
        {
          "pattern": "(\\</body\\>.*)$",
          "replacement": "",
          "singleLine": true
        },
        {
          "pattern": "RouteName=\"(SampleA)\"",
          "replacement": "%SampleUniqueName%_SampleA"
        },
        {
          "pattern": "RouteName=\"(SampleB)\"",
          "replacement": "%SampleUniqueName%_SampleB"
        }
      ]
    },
    { "name": "SampleA.dothtml" },
    { "name": "SampleB.dothtml" },
    { "name": "ViewModel.cs" },
    {
      "name": "DotvvmStartup.cs",
      "doNotExport": true
    }
  ]
}
```