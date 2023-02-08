Generates a form for the object in the current [binding context](~/pages/concepts/data-binding/binding-context) based on [DotVVM Auto UI model metadata](~/pages/concepts/auto-ui/metadata).

This control renders HTML and CSS compatible with [Bootstrap 5](https://getbootstrap.com/). It contains a handful of properties which can customize it to be compatible with Bootstrap 3 and 4 - see [Sample 8](#sample8).

There are also other versions of this control which produce HTML and CSS classes expected by popular CSS frameworks: 

* [BulmaForm](../BulmaForm) for [Bulma](https://bulma.io/)

* [Form](../Form) - table-based form layout without any CSS framework

## Validation style of CheckBox in Bootstrap 5 and 4

Due to the way how DotVVM [CheckBox](~/controls/builtin/CheckBox) renders (`label` wrapped around the `input` element), you may need to add the following CSS to correctly display validation appearance of this control in the form.

```
<style>
    .is-invalid.form-check-label, .is-invalid .form-check-label {
        color: #dc3545;
    }
    .is-invalid .form-check-input {
        border-color: #dc3545;
    }
</style>
```