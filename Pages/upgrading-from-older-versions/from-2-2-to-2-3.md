# Upgrading from 2.2 to 2.3

See [Release notes of DotVVM 2.3](https://github.com/riganti/dotvvm/releases/tag/v2.3.0) for complete list of changes.

## Breaking changes

The `CheckBox` control was rendering different HTML structure for the case when its `Text` property was empty. This structure did not include the `label` element, that is in most cases used as a base element for restyling of the checkbox design. Now, the `label` element is rendered in both cases, which may break some CSS. 

## See also

* [From 2.3 to 2.4](from-2-3-to-2-4)