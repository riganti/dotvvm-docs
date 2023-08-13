# Upgrading from 4.0 to 4.1

See [Release notes of DotVVM 4.1](https://github.com/riganti/dotvvm/releases/tag/v4.1.0) for complete list of changes.

## Breaking changes

### Binding expressions cannot access private members

Because of a bug in DotVVM 4.0, if you used a private member in a data-binding expression, it worked in some circumstances (in resource bindings and in value bindings where the private member was serialized and sent to the client). This was fixed in 4.1, but it may break your application if you accidentally used this.

We recommend using the [Compilation status page](compilation-status-page) to make sure that all pages can be compiled.

### Objects are cloned instead of using the init property

DotVVM 4.0 had a bug which treated `{ get; init; }` properties as if they had normal setter. When an object with such properties was present in the viewmodel, DotVVM would call the init setter even after the object was already initialized, which might have led to an unexpected behavior. DotVVM 4.1 fixes the behavior.

### Changes in the `ReflectionUtils` class

The `ReflectionUtils.PrimitiveTypes` collection has been made private. Please use the `IsPrimitiveType` method to determine whether DotVVM treats the type as primitive.
