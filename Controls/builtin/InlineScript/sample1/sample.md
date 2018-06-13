## Sample 1: Basic InlineScript

The content of the InlineScript control is rendered as javascript in the page.

The framework adds a global object **dotvvm** which can be used to access client-side features.
You can use the following events fired by the framework.

* **init**: Executed when the DotVVM scripts and all required resources are loaded and the viewmodel is applied to the page.
* **beforePostback**: Executed before the postback is sent to the server.
* **afterPostback**: Executed after the postback response is received from the server.
* **error**: Executed when the postback fails due to a some error on a server.
* **spaNavigating**: Executed when you leave some page in the SPA container.
* **spaNavigated**: Executed when you enter some page in the SPA container.

