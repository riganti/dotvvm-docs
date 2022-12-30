# Getting started with Bootstrap 5 for DotVVM

To use the [Bootstrap 5 for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm) controls, you have use the [DotVVM Private Nuget Feed](~/pages/dotvvm-for-visual-studio/dotvvm-private-nuget-feed).

1. Install the `DotVVM.Controls.Bootstrap5` package from the DotVVM Private Nuget Feed.

2. Open your `DotvvmStartup.cs` file and add the following line at the beginning of the `Configure` method.

```CSHARP
config.AddBootstrap5Configuration();
``` 

You might need to add the following `using` at the beginning of the file.

```CSHARP
using DotVVM.Framework.Controls.Bootstrap5;
```

This will register all Bootstrap controls under the `<bs:*` tag prefix, and it also registers several Bootstrap resources. Bootstrap 5 for DotVVM also includes Bootstrap Icons font. All icons could are available using [<bs:Icon> control](~/controls/bootstrap5/Icon).


## CSS Variables Configuration

Bootstrap 5 allows altering properties with [CSS variables](https://getbootstrap.com/docs/5.2/customize/css-variables/). You can replace them with your own values in your CSS file.
 
```CSS

/* Override global variable */
body {
    --bs-body-font-size: 2rem;
}

/* Override accordion variable */
.accordion-button:not(.collapsed) {
    --bs-accordion-active-bg: #00ff90 !important;
}

```

## Usage
The usage of each control is shown in the [controls documentation](~/controls/bootstrap5/Accordion).
