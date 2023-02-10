## Sample 4: Include properties based on view

Sometimes you want to reuse the same model object in multiple contexts, a.k.a. __"views"__.

You can decorate the properties with the `Visible` attribute to define in which views they shall appear.

```CSHARP
// will be shown only when ViewName == "List"
[Visible(ViewNames = "List")]
public string CountryName { get; set; }

// will be shown only when ViewName == "Insert" || ViewName == "Edit"
[Visible(ViewNames = "Insert | Edit")]
public int CountryId { get; set; }

// will be shown only when ViewName != "Insert" && ViewName != "Edit"
[Visible(ViewNames = "!Insert & !Edit")]
public int UserId { get; set; }
```

The `ViewName` property specifies the view name for the current context. That's why only the column for the `CountryName` property is displayed in the sample.