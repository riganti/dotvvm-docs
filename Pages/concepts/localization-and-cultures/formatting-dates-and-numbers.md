# Formatting dates and numbers

If you need the user to enter dates or numeric values, you may need a user-friendly formatting for these values.

The formatting uses the culture in which the HTTP request was processed. See the [Multi-language applications](multi-language-applications) chapter for more details.

## Formatting values

The [Literal](~/controls/builtin/Literal), [TextBox](~/controls/builtin/TextBox), and other controls can specify a `FormatString` property. If you need to output a date or number value in the page, you can use the following syntax:

```DOTHTML
<dot:Literal Text="{value: BirthDate}" FormatString="dd/MM/yyyy" />
<dot:Literal Text="{value: TotalPrice}" FormatString="c2" />
```

DotVVM can also translate the `ToString` method on date and numeric types:

```DOTHTML
<p>Total price: {{value: TotalPrice.ToString("c2")}}</p>
```

DotVVM uses the same format string syntax you know from C#, with the following limitations:

* [Standard Numeric Format Strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings): DotVVM supports `d`, `g`, `n`, `c` and `p` format specifiers.

* [Custom Numeric Format Strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings): DotVVM supports `0`, `.` and `#` tokens.

* [Standard Date and Time Format Strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings): DotVVM supports everything except `O`, `U` and `R` format specifiers.

* [Custom Date and Time Format Strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings): DotVVM supports everything except the `K`, `:`, `\` and `%` tokens.

## Editing formatted values

You can enforce the date or number format in the [TextBox](~/controls/builtin/TextBox) control using the `FormatString` property. 

When the user enters a value in such control, it will be parsed based on the current culture, and re-formatted in case the entered value didn't follow the format strictly. See the [validation](~/pages/concepts/validation/overview) chapter for more information on how to validate correctness of user-entered values.

```DOTHTML
<dot:TextBox Text="{value: BirthDate}" FormatString="d" />
<dot:TextBox Text="{value: TotalPrice}" FormatString="n2" />
```

## Validate numeric and date values

If the user enters a value that cannot be parsed, DotVVM will try to set `null` in the property:

* If the property supports `null` values (e. g. uses the `int?` type), it will get the default value on the server (e.g. `0` for `int` type).

* If the property doesn't allow `null` values (e. g. uses the `int` type), a validation error will be produced when a [command](~/pages/concepts/respond-to-user-actions/commands) is triggered. 

You can use the `Required` attribute to validate numeric and DateTime values. If the value cannot be parsed, the client-side `Required` validator reports an error because it sees the `null` value in the property. See the [validation](~/pages/concepts/validation/overview) chapter for more information on how to indicate validation errors.

## See also

* [Multi-language applications](multi-language-applications)
* [RESX files](resx-files)
* [Literal control](~/controls/builtin/Literal)
* [TextBox control](~/controls/builtin/TextBox)