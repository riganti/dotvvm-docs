# Custom Tracing of DotVVM Events

This guide describes how to achieve something similar like the [MiniProfiler widget](./advanced-miniprofiler.md), but for any tracing technology you like.


## `IRequestTracer` interface

You can implement a `IRequestTracer` interface and register the instance in the `ServiceCollection` and DotVVM will automatically call the methods on it.

### `TraceEvent`

The main method of the tracer is `TraceEvent` method. It receives name of the event as a string. We use string just to allow anyone to extend the set of events, but DotVVM itself will call you with following events.

* `BeginRequest` - when DotVVM gets the request from Asp.Net Core or OWIN. It's called before a matching route is selected, so the `context.Route` is going to be `null`.
* `ViewInitialized` - when the initial control tree is created, but before DotVVM initializes the view model
* `ViewModelCreated` - when the view model is creates, before it is deserialized and before `Init` is called
* `InitCompleted` - after `Init` is called on view model and all controls
* `ViewModelDeserialized` - after the view model is deserialized (only called for post backs)
* `LoadCompleted` - after `Load` is called on view model and all controls
* `CommandExecuted` - after the command is executed (only called for post backs)
* `PreRenderCompleted` - after `PreRender` is called on view model and all controls
* `ViewModelSerialized` - after the view model is serialized to JSON
* `OutputRendered` - when output HTML or JSON is written to the response body
* `StaticCommandExecuted` - after static command method is executed

### `EndRequest`

The `EndRequest(IDotvvmRequestContext context)` is called in case the request is handled successfully. The other overload `EndRequest(IDotvvmRequestContext context, Exception exception)` is used, in case an exception occurs.

All these method return a `Task`, so you can do any async operation. However, we encourage you to avoid long running operations since the tracing is called quite number of times during the request. If you want to monitor timing between the events of a DotVVM application, slow tracing may ruin your measurements.

## Sample tracer

With this knowledge, we can create a simple tracer, that will just write the timings to standard output. We'll start a `Stopwatch` when we get the `BeginRequest` event and then just print the elapsed time.

In our sample, we don't use async IO as writing using `Console.WriteLine` is more convenient.

```csharp
public class SampleRequestTracer : IRequestTracer
{
    Stopwatch sw = new Stopwatch();
    public Task TraceEvent(string eventName, IDotvvmRequestContext context)
    {
        if (eventName == "BeginRequest")
            sw.Start();
        Console.WriteLine($"Trace {sw.Elapsed}: {eventName}");
        return Task.CompletedTask;
    }

    public Task EndRequest(IDotvvmRequestContext context)
    {
        Console.WriteLine($"Trace {sw.Elapsed}: End of request");
        return Task.CompletedTask;
    }

    public Task EndRequest(IDotvvmRequestContext context, Exception exception)
    {
        Console.WriteLine($"Trace {sw.Elapsed}: Error occured ({exception})");
        return Task.CompletedTask;
    }
}
```

It should be registered `IServiceCollection` (in the `ConfigureServices` method of Asp.Net Core startup class):

```csharp
services.AddScoped<IRequestTracer, SampleRequestTracer>();
```

Also note that only requests to the page itself are traced. Requests for resources are not included in DotVVM tracing.
