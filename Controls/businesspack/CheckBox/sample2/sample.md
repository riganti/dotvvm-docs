## Sample 2: Multiple CheckBoxes

The CheckBox has also a property `CheckedItems` which can hold any collection. This is an alternative to the `Checked` property - they cannot be combined.
The collection holds values of all checkboxes which are checked. The value of the checkbox which should be inserted in the collection, is defined in the
`CheckedValue` property.



The first two checkboxes will be checked by default because the collection holds initial values "r" and "g".
If you uncheck or check a checkbox, the collection will be updated immediately.


You can also modify the collection contents in the viewmodel code which will update the checkbox check states appropriately.
