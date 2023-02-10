## Sample 2: Groups of properties

To generate columns just for some properties, you can use the `GroupName` property to tell the control which group of properties shall be considered.

The property can be assigned to a specific group using the `Display` attribute:

```CSHARP
[Display(..., GroupName = "basic")]
public string FirstName { get; set; }
```