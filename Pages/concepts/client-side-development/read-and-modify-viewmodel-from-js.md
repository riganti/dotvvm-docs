# Read & modify viewmodel from JS

DotVVM stores the page viewmodel in `dotvvm.viewModels.root.viewModel` object. The viewmodel corresponds with the C# class, and all properties and array elements are wrapped in Knockout observables.

To read data from the viewmodel, you need to evaluate every observable in the property path:

```CSHARP
// C# - server-side expression
this.EventAttendees[2].FirstName

// JavaScript - client-side expression
dotvvm.viewModels.root.viewModel.EventAttendees()[2]().FirstName()
```

To set data, you need to call the observable and pass the new value as an argument:

```CSHARP
// C# - server-side expression
this.EventAttendees[2].FirstName = "test";

// JavaScript - client-side expression
dotvvm.viewModels.root.viewModel.EventAttendees()[2]().FirstName("test");
```

### Working with dates

DotVVM uses special handling for `Date` values in JavaScript. To prevent automatic conversions to local time on the client side, DotVVM stores date & time values as strings in the following format: `yyyy-MM-ddTHH:mm:ss.fffffff`.

To convert JavaScript `Date` into DotVVM representation, use the following function:

```JS
dotvvm.serialization.serializeDate(date, convertToUtc);
```

To convert DotVVM date representation to JavaScript `Date`, use the following function:

```JS
dotvvm.globalize.parseDotvvmDate(date);
```
