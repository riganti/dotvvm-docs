## Sample 2: Multiple RadioButtons

The RadioButton has also a property `CheckedItem` which point to a property in the viewmodel. This is an alternative to the `Checked` property - 
they cannot be combined.

The property referenced by the `CheckedItem`, holds the value of the radio button which is checked. 

In the example, the second radio button will be checked when the page loads. That's because the initial value of the `Color` property is "g".
If you click another radio button, the property value will be updated immediately