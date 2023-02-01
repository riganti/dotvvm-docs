# Read & modify viewmodel from JS

> The internal representation of the viewmodel has changed in DotVVM 3.0 - the viewmodel is not stored in the hierarchy of Knockout observables, but in an immutable JS object. The Knockout API is still available, but any changes to the observables are written in the immutable state object, and subscribe handlers are now called asynchronously on the next animation frame. See the [release notes](https://github.com/riganti/dotvvm/releases/tag/v3.0) for more details.

## State object

DotVVM stores the page viewmodel in an immutable JS object, which can be accessed by accessing the `dotvvm.state` property. The viewmodel corresponds with the C# class, and all objects contain the `$type` property with the unique identification of the type.

```JS
{
    "$type": "q2gBXiTzvZbF6dPeR25iQ2MD0f8=",
    "AlertText": null,
    "AlertType": "Success",
    ...
}
```

**The state object is frozen**, so any direct modifications to it will be ignored.
If you want to modify the object, use the `patchState` function.

```JS
dotvvm.state.Something = "don't do this!!";  // DON'T DO THIS - the value won't be set, and the call will be silently ignored (no warnings or errors)

dotvvm.patchState({ Something: "new value" });  // this is the correct approach
// Knockout observables will be notified in the next animation frame
```

## Knockout observables

The Knockout observable hierarchy, that was the store of the viewmodel in the previous versions of DotVVM, is still present in the framework, because it is used by all the controls to provide the MVVM experience. 

### Old-fashioned way

You can access the viewmodel observable object using the following call:

```JS
dotvvm.viewModels.root.viewModel
```

To retrieve the value of the observable, you need to call it without arguments: `MyObservable()`. If there is an array, all its elements are also observables - that's why you need to use the following approach: 

```CSHARP
// Equivalent C# expression: 
// this.EventAttendees[2].FirstName

dotvvm.viewModels.root.viewModel.EventAttendees()[2]().FirstName()
```

To modify data, you need to call the observable and pass the new value as an argument:

```CSHARP
// Equivalent C# expression: 
// this.EventAttendees[2].FirstName = "test";

dotvvm.viewModels.root.viewModel.EventAttendees()[2]().FirstName("test");
```

When you set the observable value, the change will be written in the `dotvvm.state`. 

### New API in DotVVM 3.0

The observables got a new API in DotVVM 3.0, which is a preferred way of manipulation with the observables.

There is the `state` read-only property which returns the state. If the observable contains an object, you'll get the unwrapped object, which is frozen - you won't be able to change it directly.

If you want to modify the state, you can use either `setState` or `patchState` functions:

```JS
// replace state
dotvvm.viewModels.root.viewModel.EventAttendees.setState([ 
    { Id: 14, FirstName: "Jim", LastName: "Hacker" },
    { Id: 15, FirstName: "Humphrey", LastName: "Appleby" },
    { Id: 16, FirstName: "Bernard", LastName: "Woolley" }
]);

// patch state
dotvvm.viewModels.root.viewModel.EventAttendees.patchState([ 
    {}, // no changes to the first element
    {}, // no changes to the second element
    { FirstName: "new value" }  // just patch the first name
]);
```

Since the viewmodels contain the `$type` properties which carry the information about object types, this API rejects invalid state changes - this is called __coercion__. 

The coercer can perform some automatic conversions (like convert a number to string, and similar) - you'll get a warning in the dev console if automatic coercion happens. If the coercer cannot adjust the types correctly, you'll get a JavaScript error. 

## Dates

DotVVM uses special handling for `Date` values in JavaScript. To prevent automatic conversions to local time on the client side, DotVVM stores date & time values as strings in the following format: `yyyy-MM-ddTHH:mm:ss.fffffff`.

To convert JavaScript `Date` into DotVVM representation, use the following function:

```JS
dotvvm.serialization.serializeDate(date, convertToUtc);
```

To convert DotVVM date representation to JavaScript `Date`, use the following function:

```JS
dotvvm.serialization.parseDate(date, convertFromUtc);
```

The coercer will convert `Date` to DotVVM string representation automatically, when you try to set the value. However, if you read the value, you'll see the string. If you need `Date`, use the `dotvvm.serialization.parseDate` function.

Similarly, `TimeOnly` is stored as `HH:mm:ss` and `DateOnly` is stored as `yyyy-MM-dd`

### Time zone handling

Be careful about time zones - since the `Date` object converts all dates to the local time zone, which can cause problems when such dates are transferred to the server, DotVVM decided to ignore the offset argument, and gathers just the years, months, days, hours, minutes, seconds, and milliseconds values. 

Our intent is to make sure that when you send a `DateTime` value `2000-01-02 03:04:05:666` to the client (no matter if the `Kind` was `Local` or `Utc`), you'll see the same digits in the date (although the date is converted to the user's local timezone). 

## See also

* [Client-side development overview](overview)
* [JS directive](js-directive/overview)
* [DotVVM JavaScript events](dotvvm-javascript-events)
* [Access validation errors from JS](access-validation-errors-from-js)
* [Control development](~/pages/concepts/control-development/overview)
