# Custom Presenters

In most applications, you have several situations where you need to generate some XML, JSON or other type of a content (RSS feed etc.), or just compose the HTTP response yourself. 

In OWIN, you can write your own middleware which handles the communication, or you can use another framework for that (e.g. ASP.NET Web API for REST interfaces).

But sometimes you need to integrate with various DotVVM services (e.g. routing), or you just don't want to introduce additional libraries in your project.
That's the situation when you need a custom presenter. It is a lightweight alternative to OWIN middleware which lets you to handle the HTTP request yourself, and flawlessly 
integrates with DotVVM routing.

To create a custom presenter, you have to create a class that implements the `IDotvvmPresenter` interface. In the `ProcessRequest` method you can handle the OWIN request yourself.
The main advantage of custom presenter over OWIN middleware is that you can use DotVVM routes, use route parameters, and you also have the
 [Request Context](/docs/tutorials/basics-request-context/{branch}) object.

```CSHARP
    public class RssPresenter : IDotvvmPresenter
    {
        ...

        public Task ProcessRequest(DotvvmRequestContext context)
        {
            context.OwinContext.Response.ContentType = "application/rss+xml";

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
            var writer = XmlWriter.Create(context.OwinContext.Response.Body, new XmlWriterSettings() { Indent = true });
            feed.SaveAsRss20(writer);
            writer.Flush();

            return Task.FromResult(0);
        }
    }
}
```

Finally, you have to register the presenter in the `DotvvmStartup.cs` file:

```CSHARP
config.RouteTable.Add("Rss", "feeds/rss", null, null, () => new RssPresenter());
```