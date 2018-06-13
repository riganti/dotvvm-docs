## Sample 4: Binding to a Collection

You don't have to use *GridViewDataSet*s if you don't want. Use a normal .NET collection as the `DataSource`. 

If the user wants to sort, there is the `SortChanged` command which fires when the user clicks the column header.

Notice that the method is on the main viewmodel object has parameters, however the command doesn't specify them, it is just `{command: Sort}`. 
That's because the `SortChanged` event expects a _delegate_ to a command, not a command itself. You can distinguish between standard events
and event delegates by the type in the **Events** table. 

If the type is **Command**, it is a standard command. If the type is **Command Delegate**, it is a delegate command.

The Sort method in the viewmodel accepts one parameter of **string**.