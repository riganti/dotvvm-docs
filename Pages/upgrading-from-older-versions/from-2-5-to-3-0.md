# Upgrading from 2.5 to 3.0

See [Release notes of DotVVM 3.0](https://github.com/riganti/dotvvm/releases/tag/v3.0) for complete list of changes.

## Breaking changes

### Null propagation now passes nulls into methods

Previously, any binding expression which evaluated to `null` caused to evaluate the parent expression to `null` as well. If there was a method call in the expression, and any of the arguments evaluated to `null`, the entire method call was skipped and evaluated to `null`. We changed this behavior so the `null` is passed in the method.

### Script resources use defer by default

DotVVM resources were marked as deferred, which means that all other scripts that depend on DotVVM need to use `defer`, otherwise they would load before DotVVM and fail. We have changed the default behavior of `ScriptResource` so the `Defer` property is `true` by default, but it can break scripts which explicitly set it to `false`.

### Async IUploadedFileStorage and IReturnedFileStorage

We changed these interfaces to use async methods everywhere. The behavior hasn't changed, you'll only need to add `await` to the `GetFile` or `ReturnFile` calls, and change them to `GetFileAsync` / `ReturnFileAsync`.

### Type-safe viewmodels

Objects in the viewmodel now always have the `$type` property which contains unique ID of the concrete type. Also, DotVVM validates all values in the viewmodel as part of the "coercion" process. 

If you use unsupported types in the viewmodels, you may get JavaScript errors and the page won't load. We have released a few hotfixes to support `Dictionary<K,V>`, `TimeSpan` and `DateTimeOffset` to not break the page. _These types are still not officially supported, but if you happen to use them in the viewmodel, the page will not throw JS errors._

### Client-side events and postback pipeline

We changed the order of some DotVVM JavaScript events, and renamed some properties in event arguments objects passed to the even listeners. We have removed `xhr` parameter from all event arguments as we are now using `fetch` API.

### JavaScript value for CheckBox was changed

The `input[type=checkbox]` value is now `"on"`/`"off"` instead of `true`/`false`. If you interact with checkboxes from your JS code, make sure these new values are reflected.

### DotVVM Knockout binding handles renamed

If you used some Knockoug binding handlers added by DotVVM in your own control, check out if they still work. We have renamed and reorganized some of them - for example, `dotvvmEnable` was renamed to `dotvvm-enabled`.



