## Sample 2: Dismiss

The `AllowDismiss` property will add an icon to the right side of the alert, allowing the user to hide the alert. The `Dismissed` command is triggered when the alert is hidden by user.

The `DismissIconCssClass` can be used to specify the CSS class for the dismiss icon. The default value is `fa fa-close` (the FontAwesome close icon).

You can also use the `AutoHideTimeout` property to hide the alert automatically after being displayed for some time. The `Dismissed` command is not triggered in this case.