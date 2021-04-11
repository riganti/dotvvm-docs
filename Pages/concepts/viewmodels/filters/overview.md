# Overview

Filters allow to intercept calls to these methods from the UI. They can be used to validate request, log or handle any errors, and more.

DotVVM can apply [action](action-filters) or [exception filters](exception-filters) on individual methods, on specific viewmodel classes, or globally for all viewmodels in your application.

> Please note that the filters are currently supported only in [command](~/pages/concepts/respond-to-user-actions/commands) binding, not in static commands.

## Usage of filters

Because the base classes of the filters inherit from the `Attribute` class, you can apply those filters on a viewmodel or a method as an attribute.

```CSHARP
[MyValidationFilter]
public class DemoViewModel 
{
	[MyCustomFilter]
	public void Command1() 
	{
	}

	public void Command2() 
	{
	}
}
```

In the example above, there is a `MyValidationFilter` applied on the viewmodel class, which means that every [command](~/pages/concepts/respond-to-user-actions/commands) referencing a method in the viewmodel will use this filter. 

If you call the `{command: Command1()}` from a button in the page, `MyValidationFilter` and also the `MyCustomFilter` will be applied. 

> Like the `Authorize` attribute (which is also an action filter), the filter is executed if the command binding in the page references the method. If you call `Command1()` from the `Command2()` method and the binding in the page references the `Command2` method, the `MyCustomFilter` will not be applied.

## Register filter globally

If you need to apply a filter globally, navigate in the `DotvvmStartup.cs` class and register the filter in the `config.Runtime.GlobalFilters` collection in the `DotvvmConfiguration` object.

```CSHARP
config.Runtime.GlobalFilters.Add(new ErrorHandlingActionFilter());
```

## Combine filters

You can apply multiple filters on a viewmodel or a method. The filters are called (in the order they were registered in the `GlobalFilters` collection), or in the order of the attributes on the class or a method.

All the filter methods except the `OnCommandExecutedAsync` method are executed in the following order:

+ Global filters (in the order they were registered)

+ Filter applied on the viewmodel class (in the order they were registered)

+ Filters applied on the individual methods (in the order they were registered)
 
The `OnCommandExecutedAsync` methods uses the reverse order of action filters.

## See also

* [Action filters](action-filters)
* [Exception filters](exception-filters)