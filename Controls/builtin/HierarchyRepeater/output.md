The `LevelWrapperTagName` property specifies which HTML tag should wrap each collection, while `ItemWrapperTagName` specifies which HTML tag should wrap each item (including its children).
Both properties are optional and if not specified, no wrapper is added (Knockout [virtual elements](https://knockoutjs.com/documentation/custom-bindings-for-virtual-elements.html) are used instead).

The entire repeater is also wrapped in `WrapperTagName`, unless `RenderWrapperTag=false` is specified. This property defaults to `div`.

The generated HTML differs in Server and Client `RenderSettings.Mode`s. Server mode does not include the client-side template, and thus the hierarchy will not update if new elements are added. Server-rebdered HTML has the following structure:

```html
<div>
    <ul>
        <li>
            [template 1]
            <ul>
                <li>
                    [template 1.1]
                    <!-- empty level tags (<ul></ul>) are not included -->
                </li>
            </ul>
        </li>
        <li>
            [template 2]
        </li>
    </ul>
</div>
```

Client-side rendering will result in the same hierarchy of DOM elements. Only the wrapper tag (`div`) and first level (`ul`) are rendered server-side, the rest is handled using a knockout `template` binding.
