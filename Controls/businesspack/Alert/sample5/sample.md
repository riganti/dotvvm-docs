## Sample 5: Common Usage

The typical use of the `Alert` control is to have 2 properties in the viewmodel (`AlertText` and `AlertType`) which are bound to the properties of the `Alert` control.

Notice that the properties are marked with the `Bind` attribute setting the direction to `ServerToClient`. That's because we don't need to send the property values to the server when we are making a postback. We only need to send the new value to the client if the properties are changed.

We suggest to put the `AlertText` and `AlertType` properties into the base viewmodel class and create a method which does this kind of exception handling. Then you can use it on all places in your application.