## Sample 1: Basic Usage

The `ModalDialog` control has the `HeaderText` property which sets the title of the popup. 

By default, the dialog title is rendered as `<h4>` header, but you can adjust it using the `HeaderTagName` property.
If you need to customize the header using your own HTML content, use the  `HeaderTemplate` property instead.

The contents of the dialog are specified using the `ContentTemplate` property. This is the only required property.

The button row can be customized using the `FooterTemplate` property. 


The `ModalDialog` can be displayed or hidden using the `IsDisplayed` property. Just bind it to a `bool` property in the viewmodel and set it to `true` or `false`. 
