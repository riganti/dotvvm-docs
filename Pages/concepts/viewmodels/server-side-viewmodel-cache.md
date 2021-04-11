# Server-side viewmodel cache

> This feature is new in **DotVVM 2.4**.

Thi **server-side viewmodel cache** can dramatically reduce the amount of data transferred between the server and the client.

## Principle

Normally, when DotVVM completes a HTTP request, the viewmodel is not needed any more, and is eventually collected by the garbage collector. It still lives in the client's browser, and can be restored from the serialized data when a [command](~/pages/concepts/respond-to-user-actions/commands) is invoked. 

When the viewmodel cache is enabled, DotVVM assigns a unique ID to the viewmodel, and store it in a memory cache (serialized as BSON).

When the user makes a postback, DotVVM can send only the properties that was changed on the client-side, instead of serializing and transmitting the entire object. Thanks to this, a lot of network traffic can be saved – almost 100% in extreme cases. 

Imagine a page with a `GridView` control, and a delete button in every row. When you click on the button, the viewmodel on the client hasn't changed at all - it is still the same as it is in the server cache. Thus, the diff will be empty, and the postback will transfer only a tiny JSON object with an identification of the button that was clicked – no viewmodel data at all.

## Resilliency

Since the viewmodel cache is stored in memory of the web application process, it can be easily lost (e. g. when the process is restarted). 

If the client makes a postback with a viewmodel ID that is unknown to the server, DotVVM will return a special response to the client, and the request will be retried immediately with a full viewmodel. This is done automatically by DotVVM - the user won't notice anything (except for a slightly longer response caused by the second request).

## Memory requirements

The cached viewmodels are deduplicated by its content. DotVVM uses a hash of the serialized viewmodel as a cache key, so when you use this feature on a public facing site which has the same "static" viewmodel for many users, the viewmodel will be kept in memory only once. 

The mechanism of caching viewmodels is extensible, so you can use other storage than memory by implementing the `IViewModelServerStore` interface. 

There is a [Server-side viewmodel cache diagnostics tool](https://github.com/riganti/dotvvm-diagnostics-server-side-cache) that can visualize how much traffic the viewmodel cache saved, and how much memory the cached viewmodels occupy.

## Enable the cache

The feature can be enabled for all routes, or only for specific routes in the application. 

To enable this feature, add the following code snippet in `DotvvmStartup.cs`:

```CSHARP
config.ExperimentalFeatures.ServerSideViewModelCache.EnableForAllRoutes();

// enable the feature just for specific routes 
// config.ExperimentalFeatures.ServerSideViewModelCache.EnableForRoutes("Default", "MyProfile");
```

We recommend to enable it on less frequently used pages at first, and watch the memory consumption on the server – the cache should be cleaned up when the server gets low on memory, but still – we encourage to start using this feature carefully.

## See also

* [Commands](~/pages/concepts/respond-to-user-actions/commands)
* [Optimize command performance](~/pages/concepts/respond-to-user-actions/optimize-command-performance)
* [Server-side viewmodel cache diagnostics tool](https://github.com/riganti/dotvvm-diagnostics-server-side-cache)
