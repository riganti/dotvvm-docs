## Sample 1: Basic Usage

The `Text` property specified the text on the button. Alternatively, you can use the `Content` property to create a custom dropdown button toggle. Additionally `ButtonType` property can change the toggle type to `Link`.

The `DropDirection` property can specify whether the menu drops up, down or to the side.

The `IsCollapsed` property indicates whether the menu is open or not. You can also use it in data-binding.

Place `<bs:DropDownItem>` controls inside the `<bs:DropDown>` control and use their `NavigateUrl` property to specify the link URL. 
You can place them inside the `<Items>` element, however it is not required because the `Items` is the default property of `DropDown`.