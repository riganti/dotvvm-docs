
# Metrics

DotVVM is instrumented with [System.Diagnostics.Metrics](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation), which should be consumable by most observability platforms (i.e. OpenTelemetry, Prometheus exporter).
The metrics which we consider important and generally useful:

* `dotvvm_viewmodel_size_bytes` histogram - Size of the returned viewmodel JSON in bytes.
   - Labels `route=RouteName` and `request_type=Navigate/SpaNavigate/Command/StaticCommand`
* `dotvvm_request_duration_seconds` - Time it took to stringify the resulting JSON view model.
   - Labels `route`, `request_type` and `dothtml_file`
   - You can use ASP.NET Core statistics about request duration, this metric makes it possible to split the measurement by DotVVM route name. 
* `dotvvm_viewmodel_serialization_seconds` histogram - Time it took to serialize view model to JSON objects.
   - Labels `route` and `request_type`
* `dotvvm_control_lifecycle_seconds` histogram - Time it took to process a request on the specific route
   - Labels `route` and `lifecycle_type=PreInit/Init/Load/PreRender/PreRenderComplete`
* `dotvvm_command_invocation_seconds` - Time it took to invoke a specific command method. Compared to `request_duration_seconds` this only includes the time spent in the command method, and is labeled by the executed binding (`command`)
   - Labels `command` and `result=Ok/Exception/UnhandledException`
* `dotvvm_staticcommand_invocation_seconds` - Similar to `command_invocation_seconds`, but for staticCommand invocations
   - Labels `command` and `result=Ok/Exception/UnhandledException`
* `dotvvm_viewmodel_validation_errors_total` histogram - Number view model validation errors returned to the client.
   - Labels `route` and `request_type`
* `dotvvm_uploaded_file_bytes` - Total size of user-uploaded files
* `dotvvm_returned_file_bytes` - Total size of returned files. Measured when the file is returned, not when downloaded by the client

If you are using [Server-side viewmodel cache](https://www.dotvvm.com/docs/4.0/pages/concepts/viewmodels/server-side-viewmodel-cache) you might be also interested in the related metrics in order to measure if/how is the cache helping:

* `dotvvm_viewmodel_cache_hit_total` counter - number of cache hits
* `dotvvm_viewmodel_cache_miss_total` counter - number of cache misses - on a cache miss, the request fails and client has to retry uploading the entire view model.
* `viewmodel_cache_loaded_bytes_total` counter - number of bytes loaded from the cache, you may interpret it as an upper bound estimate on the bandwidth saved

The more niche / internal metrics are:
* `dotvvm_view_compiled_total` - number of views compiled (should stay constant at runtime).
* `dotvvm_view_compilation_seconds_total` - time spent in view compiler.
* `dotvvm_property_initialized_total` - number of DotVVM properties initialized (should be constant at runtime).
* `dotvvm_binding_initialized_total` - number of bindings instantiated.
* `dotvvm_binding_compiled_total` - number of binding expressions compiled (if it continues growing, it signifies problem with a missing cache).
* `dotvvm_request_rejected_total` - number of requests rejected (for security reasons) on the specific route.
* `dotvvm_resource_serve_seconds` - time it took to lookup and serve a resource.
* `dotvvm_lazy_csrf_token_created_total` - number of lazy CSRF tokens created.


All metrics are listed in the [DotvvmMetrics.cs source code file](https://github.com/riganti/dotvvm/blob/main/src/Framework/Framework/Hosting/DotvvmMetrics.cs).

## Prometheus configuration

If you want to use the [prometheus-net](https://github.com/prometheus-net/prometheus-net) library to expose the metrics in [prometheus](https://prometheus.io/) format, we recommend calling the `Prometheus.MeterAdapter.StartListening` method as soon as possible (at the start of Startup/DotvvmStartup is a good place), and configuring the buckets for histograms.

```csharp
MeterAdapter.StartListening(new MeterAdapterOptions {
    ResolveHistogramBuckets = instrument => {
        // prometheus-net does not know which buckets will make sense for each histogram and System.Diagnostics.Metrics API
        // does not provide a way to specify it. The ResolveHistogramBuckets function will be called for each exported histogram in to define the buckets.
        return DotvvmMetrics.TryGetRecommendedBuckets(instrument) ?? MeterAdapterOptions.DefaultHistogramBuckets;
    }
});

```

