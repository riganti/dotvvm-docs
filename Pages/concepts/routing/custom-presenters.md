# Custom Presenters

In most applications, there is several situations where you need to generate some XML, JSON or other type of a content (RSS feed etc.), or just compose the HTTP response yourself. 

In both OWIN and ASP.NET Core, you can write your own middleware which handles the communication, or you can use another framework for that (e.g. ASP.NET Web API for REST interfaces).

But sometimes you need to integrate with various DotVVM services (e.g. file uploads, routing), or you just don't want to introduce additional libraries in your project.
That's the situation when you need a custom presenter. It is a lightweight alternative to OWIN middleware which lets you to handle the HTTP request yourself, and flawlessly 
integrates with DotVVM routing.

To create a custom presenter, you have to create a class that implements the `IDotvvmPresenter` interface. In the `ProcessRequest` method you can handle the OWIN request yourself.
The main advantage of custom presenter over the middleware is that you can use DotVVM routes, use route parameters, action filters and you also have the
 [Request Context](/docs/tutorials/basics-request-context/{branch}) object.

```CSHARP
    public class RssPresenter : IDotvvmPresenter
    {
        ...

        public Task ProcessRequest(IDotvvmRequestContext context)
        {
            context.GetOwinContext().Response.ContentType = "application/rss+xml";

            // load data
            var articles = ...;
            
            // generate RSS feed
            var items = articles
                .Select(a =>
                {
                    var item = new SyndicationItem(a.Title, a.Description, a.Url)
                    {
                        PublishDate = a.BeginDate
                    };
                    item.Authors.Add(new SyndicationPerson() { Name = a.AuthorName });
                    return item;
                });

            // create feed
            var feed = new SyndicationFeed("Sample RSS Feed", "DotVVM Sample", new Uri("https://www.dotvvm.com"), items);

            // write the XML to the output
            var writer = XmlWriter.Create(context.GetOwinContext().Response.Body, new XmlWriterSettings() { Indent = true });
            feed.SaveAsRss20(writer);
            writer.Flush();

            return Task.FromResult(0);
        }
    }
}
```

Finally, you have to register the presenter in the `DotvvmStartup.cs` file:

```CSHARP
config.RouteTable.Add("t", "u", typeof(RssPresenter));
```

The `RssPresenter` must be registered in the `IServiceCollection`. See [IoC/DI Containers](/docs/tutorials/advanced-ioc-di-container/{branch}) for more information.

Alternatively, you can register your own factory method that will be used to create the instance of the presenter. In that case, the presented doesn't need to be registered in `IServiceCollection`:

```CSHARP
config.RouteTable.Add("t", "u", serviceProvider => new RssPresenter());
```

## Action Filters

You can apply [Action Filters](/docs/tutorials/advanced-action-filters/{branch}) or the [Authorize attribute](/docs/tutorials/advanced-authentication-authorization/{branch}) on the presenter class.