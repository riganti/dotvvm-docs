# Upgrading from 2.4 to 2.5

See [Release notes of DotVVM 2.5 Preview 2](https://github.com/riganti/dotvvm/releases/tag/v2.5.0-preview02) and [Release notes of DotVVM 2.5](https://github.com/riganti/dotvvm/releases/tag/v2.5.0) for complete list of changes.

## Breaking changes

### REST API bindings upgraded to latest Swashbuckle.AspNetCore

The `DotVVM.Api.Swashbuckle.AspNetCore` package now references `Swashbuckle.AspNetCore 5.4.1` which works with both ASP.NET Core 2.x and 3.x. If the API consumed by DotVVM app is hosted in ASP.NET Core 2.x, or if it explicitly uses the `Newtonsoft.Json` serializer in 3.x, you'll need to install `Swashbuckle.AspNetCore.NewtonsoftJson` package as well, otherwise the Swagger metadata won't be produced correctly.

### DotVVM doesn't rely on DefaultSettings

If you made some changes in `Netwonsoft.Json`'s `DefaultSettings` serializer configuration object, these changes were used by DotVVM until DotVVM 2.5. From this version, we are using our own instance of `JsonSerializerSettings`, so these changes are not applied to DotVVM serialization any more. 

If you registered your own converters so you could transfer your own objects in the viewmodel, and then used JavaScript to work with such objects, this code will probably break. 

## See also

* [From 2.5 to 3.0](from-2-5-to-3-0)