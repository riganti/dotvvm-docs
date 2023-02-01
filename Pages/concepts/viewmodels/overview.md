# Viewmodels overview

The viewmodel has two main responsibilities:

+ **It manages the state of the page** - i. e. values in all form fields, rows in a [GridView](~/controls/builtin/GridView), items in a [ComboBox](~/controls/builtin/ComboBox), and so on.

+ **It processes commands invoked by the user** - e. g. defines what happens when the user clicks on a [Button](~/controls/builtin/Button). 

```CSHARP
public class CalculatorViewModel 
{
    // The state of the page is represented by public properties.
    // If you serialize the viewmodel, you should have all the information to restore the page exactly same state in the future.

    public int Number1 { get; set; }

    public int Number2 { get; set; }

    public int Result { get; set; }


    // The commands that can be invoked from the page are public methods.
    // They can access the state of the viewmodel and change it.

    public void Calculate() 
    {
        Result = Number1 + Number2;
    }
}
```

> Be careful. The viewmodels viewmodels are sent to the client in the JSON format - any property can be changed by the user even though the UI doesn't allow such change. 

> Always [validate](~/pages/concepts/validation/overview) that the values submitted by the user are correct, and that the user has appropriate permissions to perform the operation. 

> Do not put sensitive information in the viewmodels. Check out the [viewmodel protection](viewmodel-protection) chapter for more details.

## Types supported in viewmodels

The state of the page is represented using public properties. Please note that the **viewmodel must be JSON-serializable**, because DotVVM needs to send it to the browser together with the page HTML. 

Therefore, the viewmodel can contain properties of the following types:

* supported primitive types
    * `string`
    * numeric types - `int` and `double` are preferred (you can use `byte`, `sbyte`, `short`, `ushort`, `uint`, `long`, `ulong`, `float`, and `decimal`, but the precision can be lost during the JSON serialization)
    * `bool`
    * `Guid`
    * `DateTime`, `DateOnly` and `TimeOnly`
    * enums
* nullable versions of supported primitive types (e. g. `int?`, `DateTime?`...)
* objects with properties of supported types, and a public parameterless constructor
* records with properties of supported types
* collections of supported objects, or primitive types
    * Arrays: `T[]`
    * Lists: `List<T>`
    * Dictionaries: `Dictionary<K,V>` (**new in version 3.1**)

> Please note that the `TimeSpan` and `DateTimeOffset` are not supported in the current version. 

## Base class

Viewmodels in DotVVM commonly inherit from the `DotvvmViewModelBase` class. Although it is not mandatory, we recommend to follow this convention.

The `DotvvmViewModelBase` class offers several useful mechanisms that you may need:

* The [`Context` property](request-context) gives you information about the current HTTP request.
* You can override the `Init`, `Load` and `PreRender` methods that are called during the viewmodel lifecycle.

If you cannot use the `DotvvmViewModelBase` base class, you can implement the `IDotvvmViewModel` interface to get a similar functionality.

## Viewmodel lifecycle

In the following diagram, you can see the lifecycle of the HTTP request in DotVVM. 

The left side shows what's going on when the client access the page first time (the HTTP GET request). The right side shows what happens when a [command](~/pages/concepts/respond-to-user-actions/commands) is invoked (e.g. when the user clicks a button to call a method in the viewmodel).

![Viewmodel lifecycle](viewmodels-img1.png)

## Other features

DotVVM supports [dependency injection](~/pages/concepts/configuration/dependency-injection/overview) in the viewmodels - it is easy for the viewmodels to request any services they may need in their commands. We recommend to declare the injected services as private fields in the viewmodel class - this will exclude them from the serialization automatically.

```CSHARP
public class MyViewModel : DotvvmViewModelBase 
{

    private MyService _myService;

    public MyViewModel(MyService myService)
    {
        this._myService = myService;
    }

}
```

If you need to store sensitive information in the viewmodel, or you want to sign some properties to be sure the users didn't change them, see the [viewmodel protection](viewmodel-protection) chapter for more details.

You can instruct DotVVM to exclude some properties from serialization, or to transfer them only in one way (e. g. from the server to the client). This can be configured using the [Bind attribute](binding-direction).

## Roslyn Analyzers

Since version DotVVM 4.0, DotVVM ships with Roslyn Analyzers that help with quick identification of common problems when declaring viewmodels. 

The analyzers are packaged within the main DotVVM NuGet package and therefore should be automatically registered by your IDE when referencing the new framework version. 

These analyzers will notify you about potential problems problems in your code-base by issuing warnings.

The analyzers implement several checks that evaluate whether viewmodels are serializable of not.
* **DotVVM02** - Use only serializable properties in viewmodels
   * Guard analyzing the properties and their types to determine whether they are JSON-serializable by DotVVM. This will notify you when you try to use an unsupported type in the viewmodel.
* **DotVVM03** - Do not use public fields in viewmodels
   * Guard notifying users to use public properties to store the state of viewmodel instead. Forgotten getter and setter in viewmodel properties are a quite common mistake. 

## See also

* [Request context](request-context)
* [Viewmodel best practices](work-with-data/best-practices)
* [GridView data sets](work-with-data/gridview-data-sets)
* [Dependency injection](~/pages/concepts/configuration/dependency-injection/overview)
* [Binding direction](binding-direction)
* [Viewmodel protection](viewmodel-protection)
