# Viewmodels overview

In **DotVVM**, the **viewmodel** can be any JSON-serializable class. The viewmodel does two important things:

+ It represents the state of the page (values in all form fields, options for ComboBoxes etc.).

+ It defines commands which can be invoked by the user (e.g. button clicks). 

```CSHARP
public class CalculatorViewModel 
{
    // The state of the page is represented by public properties.
    // If you serialize the viewmodel, you should have all the information to be able to restore the page in the exactly same state later.

    public int Number1 { get; set; }

    public int Number2 { get; set; }

    public int Result { get; set; }


    // The commands that can be invoked from the page are public methods.
    // They can modify the state of the viewmodel.

    public void Calculate() 
    {
        Result = Number1 + Number2;
    }
}
```

## Viewmodel limitations

The state part of the viewmodel is done using public properties. Please note that the **viewmodel must be JSON-serializable**. 

Therefore, a "DotVVM-friendly" viewmodel can contain properties of these types:

* `string`, `Guid`, `bool`, `int` and other numeric types and `DateTime`, including nullable ones (e.g. `decimal?`)

* enum

* another DotVVM-friendly object

* collections (array, `List<T>`) of DotVVM-friendly objects, enums or primitive types

> Please note that the `TimeSpan` and `DateTimeOffset` are not supported in the current version. 

## DotvvmViewModelBase

We recommend to inherit all viewmodels from `DotvvmViewModelBase`. It is not required - any class can be the viewmodel. 

But the `DotvvmViewModelBase` class gives you several useful mechanisms that you may need (e.g. the `Context` property). If you cannot use it, consider using the `IDotvvmViewModel` interface to be able to get the `Context` property and the viewmodel events.

If the viewmodel derives from the `DotvvmViewModelBase`, you can override the `Init`, `Load` and `PreRender` methods. 

In the following diagram, you can see the lifecycle of the HTTP request. The left side shows what's going on when the client access the page first time (the HTTP GET request). The right side shows what happens during the postback (e.g. when the user clicks a button to call a method in the viewmodel).

<p><img src="{imageDir}basics-viewmodels-img1.png" alt="DotVVM Page Lifecycle" /></p>

> Sending the entire viewmodel to the server and back may be inefficient in many real-world scenarios. DotVVM offers different ways of calling methods on the server which don't require presence of the viewmodel. See the [Optimizing PostBacks](/docs/tutorials/basics-optimizing-postbacks/{branch}) page for more info.

