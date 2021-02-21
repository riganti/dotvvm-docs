## Best practices

+ The properties in viewmodel and all child objects should have plain `{ get; set; }`. There should be no logic in getters, setters or the constructor of the class. The getters and setters are called by the serialization mechanism and you never know the order in which setters are invoked.

+ The viewmodel commands are part of the presentation layer. They shouldn't communicate with the database, send e-mails etc. 
In general, the viewmodel methods should only gather data from the viewmodel, call some method from the business layer to do  the real job and after it's finished, update the viewmodel with the results. 

> Please don't use Entity Framework `DbContext` directly in the viewmodel. These things should be in lower layer than in the presentation one. Look at the [samples](/docs/samples) to see how to architect the application in the layers.

> We also don't recommend exposing Entity Framework entities as viewmodel properties. Remember that the viewmodel is serialized, so the user can see all the columns including various IDs which may be private. 

> Also, you may end up with errors because of the lazy loading, which might "expand" the entities and cause big data transfers, or fail on cyclic references which are not supported in JSON.  

> If you are using Entity Framework, create [Data Transfer Objects](https://en.wikipedia.org/wiki/Data_transfer_object) and use them in your viewmodel instead. You can use libraries like [AutoMapper](http://automapper.org/) to make the mapping between entities and DTOs really easy.
