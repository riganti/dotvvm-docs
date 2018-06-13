# Formatting Dates and Numbers

If you need the user to enter dates or numeric values, you may need to use the user-friendly formatting for these values.
The formatting uses the culture in which the HTTP request was processed. See the [Globalization](/docs/tutorials/basics-globalization/{branch}).

## Formatting Values

The [Literal](/docs/controls/builtin/Literal/{branch}) can apply a format string to viewmodel values. If you need to output a date or number value in the page, you can use the following syntax:

```DOTHTML
<dot:Literal Text="{value: BirthDate}" FormatString="dd/MM/yyyy" />
<dot:Literal Text="{value: TotalPrice}" FormatString="c2" />
```

DotVVM uses the same format string syntax you know from C# with the following limitations:

* [Standard Numeric Format Strings](https://msdn.microsoft.com/en-us/library/dwhawy9k.aspx): DotVVM supports `d`, `g`, `n`, `c` and `p` format specifiers.

* [Custom Numeric Format Strings](https://msdn.microsoft.com/en-us/library/0c899ak8.aspx): DotVVM supports `0`, `.` and `#` tokens.

* [Standard Date and Time Format Strings](https://msdn.microsoft.com/en-us/library/az4se3k1.aspx): DotVVM supports everything except `O`, `U` and `R` format specifiers.

* [Custom Date and Time Format Strings](https://msdn.microsoft.com/en-us/library/8kb3ddd4.aspx): DotVVM supports everything except the `K`, `:`, `\` and `%` tokens.

## Editing Formatted Values

You can enforce the date or number format in the [TextBox](/docs/controls/builtin/TextBox/{branch}) control using the `FormatString` property. 
To make sure the format string is interpreted correctly, it is strongly recommended to add also the `ValueType="Number"` or `ValueType="DateTime"`. 

```DOTHTML
<dot:TextBox Text="{value: BirthDate}" ValueType="DateTime" FormatString="d" />
<dot:TextBox Text="{value: TotalPrice}" ValueType="Number" FormatString="n2" />
```

## Validation Numeric and Date Values

If the user enters a value that cannot be parsed, the `null` value will be stored in the viewmodel property. 
If the property is of a non-nullable type, it will get the default value on the server (e.g. `0` for `int` type).

You can use the `Required` attribute to validate numeric and DateTime values. If the value cannot be parsed, the client-side `Required` validator reports an error because it sees the `null` value in the property.

If you need to make the value optional, but it has to be in a correct format in case it is entered, you can use the `DotvvmEnforceClientFormat` attribute. 
If the field is empty or the format is correct, the property is valid. But if the value cannot be parsed, the validation reports an error.

```CSHARP
// if the field is empty or the value cannot be parsed, this will report an error
[Required]
public int RequiredNumber { get; set; }

// if the value cannot be parsed, this will report an error
// however if the field is empty, the validation succeeds and the property will be null
[DotvvmEnforceClientFormat]
public DateTime? OptionalDate { get; set; }
```
