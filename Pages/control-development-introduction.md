# Introduction to Control Development

Building custom controls is not only for advanced developers and scenarios. We really encourage you to learn how to write your own **DotVVM** controls because it will help you even in very small apps. It will also boost your productivity because you'll be able to reuse significant amount of code across multiple pages or even across multiple projects.

In DotVVM, there are two types of controls - **markup controls** and **code-only controls**. 

## Markup Controls

**Markup controls** are just a piece of DOTHTML markup which you can put in its own file and use it from anywhere.

For example, if you write a shopping site, you need the user to enter a billing and delivery address. 

Both of them have the same set of fields like name, number and street, city, state, ZIP code etc.

It would be great to create a control called `AddressEditor` and use it on every place you need to edit the addresses.

Moreover, you can take this ready-made control and use it in another project because in almost all apps you need the user to give you an address.

The control can maintain its own state and have its own internal logic, e.g. guess the city name from the ZIP code.

## Code-only Controls

**Code-only controls** are used whenever you need to render a precise piece of HTML and incorporate bindings with it.

Imagine you want to use some jQuery plugin which makes a color picker out of an `input` tag. 

Normally, you would just place the `input` tag into the page and then call a piece of javascript code which takes the input and creates the color picker. 

Also, you want the data-binding to work with this control. When the user selects a color in the color picker, you need to update the underlying property in the viewmodel. And whenever the property value in the viewmodel changes, you need to update the color in the color picker. 

You can also pack such control in a NuGet package and reuse it in many projects.

> You don't need to write all controls yourself. We have created several packs of commercial controls: [Bootstrap for DotVVM](https://www.dotvvm.com/landing/bootstrap-for-dotvvm) and [DotVVM Business Pack](https://www.dotvvm.com/landing/dotvvm-business-pack).
