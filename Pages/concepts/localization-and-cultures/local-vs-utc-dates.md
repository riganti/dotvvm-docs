# Local vs UTC dates

We feel obligated to note that dealing with timezones is hard and there is no easy magic solution that will ensure you won't mess up the offsets or time zone handling.

## Default handling of DateTime

From the first version, DotVVM tried to avoid the problem by not interfering with it and keeping all dates and times unchanged (meaning that the digits in the human-readable representation on the client and on the server will always be the same). 

The basic rule is this - if there is a property called `MyDate` of `DateTime` and I look at its `Hour` property, it should be the same number as the client will see when we bind the property in a [TextBox](~/controls/builtin/TextBox) control, and vice-versa. 

Specifically, the `Kind` property of the `DateTime` is ignored by DotVVM - we want to make sure that there will be the same numbers.  

That's why DotVVM represents dates in the viewmodel as strings in a static format (using the `Date` object in JavaScript would shift the date to the browsers local timezone). 

If you need to work with the dates on the client-side, use the `dotvvm.serialization.parseDate` and `dotvvm.serialization.serializeDate`. See the [Read and modify viewmodel from JS](~/pages/concepts/client-side-development/read-and-modify-viewmodel-from-js#dates) for more info.

## Converting UTC time to the browser timezone

Considering the principles mentioned in the previous section, DotVVM doesn't distinguish between local and UTC times. DotVVM doesn't attempt to convert the date into browser timezone - it keeps it as it is.

However, there are some cases when you have the date in the UTC format and want to convert it into the user's timezone (the timezone of the browser). 

A common example is a date of some past event in the time (adding a message to the chat, submitting the order). On the server, we can store the date as UTC, but we want to display it in the user's local time.

That's why DotVVM adds the extension method `ToBrowserLocalTime` to the `DateTime` type. You can use it for UTC values to convert it to the browse timezone.

```DOTHTML
{{value: MyDate.ToBrowserLocalTime().ToString("H:mm")}}
```

> The `ToBrowserLocalTime()` method is new in DotVVM 4.0.

When DotVVM translates this method to JavaScript, it can also provide a _reverse translation_. That's why you can use the method also like this (without the `ToString` method - use the `FormatString` property of the [TextBox](~/controls/builtin/TextBox) instead):

```DOTHTML
<!-- reverse translation works here - when the users enter the date, it will be converted to the UTC and sent to the server -->
<dot:TextBox Text="{value: MyDate.ToBrowserLocalTime()}" FormatString="yyyy-M-d H:mm" />
```

Please note that the method doesn't do any conversion when evaluated on the server (e. g. in the [server rendering](~/pages/concepts/server-side-rendering) mode or using [resource binding](~/pages/concepts/data-binding/resource-binding)).

## See also

* [Multi-language applications](multi-language-applications)
* [Formatting dates and numbers](formatting-dates-and-numbers)
* [RESX files](resx-files)

