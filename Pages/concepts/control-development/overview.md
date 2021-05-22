# Control development overview

Building custom controls is not only for advanced developers and scenarios. We really encourage you to learn how to write your own **DotVVM** controls because it will help you even in very small apps. It will also boost your productivity because you'll be able to reuse significant amount of code across multiple pages or even across multiple projects.

In DotVVM, there are two types of controls - **markup controls** and **code-only controls**. 

## Types of controls

### Markup controls

[Markup controls](markup-controls) are just a piece of DOTHTML markup which you can put in its own file and use it from multiple places.

For example, if you write a shopping site, you need the user to enter a billing and delivery address. Both of them use the same set of fields like name, number and street, city, state, ZIP code etc. It would be great to create a control called `AddressEditor`, and use it on every place you need to edit the addresses.

Moreover, you can take this ready-made control, and use it in another project because in almost all apps you need the user to give you an address. The control can maintain its own state and have its own internal logic, e.g. guess the city name from the ZIP code. This is commonly done by shiping a control viewmodel together with the control. This viewmodel is then embedded as a child in the viewmodel of the page.

### Code-only controls

[Code-only controls](code-only-controls) are used whenever you need to render a precise piece of HTML and incorporate bindings with it.

Imagine you want to use some jQuery plugin which makes a color picker out of an `input` tag. Normally, you would just place the `input` tag into the page, and then call a piece of JavaScript code which would take the input and create the color picker widget. 

However, you would need to do all of this on every page where you need to use such control. In order to make the control universal, you want it to support the data-binding. When the user selects a color in the color picker, you need to update the underlying property in the viewmodel. And whenever the property value in the viewmodel changes, you need to update the color in the color picker. 

The control may need to bring some scripts or even CSS styles with it, or may provide special behavior when it comes to validation, and so on. You can  pack such control into a NuGet package and reuse it in many projects.

## Commercial controls

You don't need to write all controls yourself. We have created several packs of commercial controls which can save much time:

* [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm)
* [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack).

## DotVVM contrib

If you author some DotVVM controls and think they may be useful to the community, check out our [DotVVM Contrib](https://github.com/riganti/dotvvm-contrib) repository - it contains dozens of community-contributed components which are shipped as separate NuGet packages. We'd be happy for any contributions.

Also, this repo can be used as a learning material or inspiration for creating your own controls - many control development concepts are covered there.

## See also

* [Markup controls](markup-controls)
* [Code-only controls](code-only-controls)
* [Adding interactivity using Knockout binding handlers](interactivity)
* [Custom postback handlers](custom-postback-handlers)
* [Binding system extensibility](binding-extensibility)
* [Binding extension parameters](binding-extension-parameters)
* [Custom JavaScript translators](custom-javascript-translators)