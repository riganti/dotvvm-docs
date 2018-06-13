## Sample 3: Changed Event and UpdateAfterKeydown

By default, if you type something in the TextBox, the value will be propagated in the viewmodel when the TextBox loses its focus.
However, sometimes you need to update the TextBox immediately. Therefore, the TextBox also has the `UpdateTextAfterKeydown` which 
will propagate the value in the viewmodel after each key press.

The `Changed` event is triggered when the value of the control changes.