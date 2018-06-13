## Sample 1: ClaimView

The `Claim` property specifies the type of claim the user must have.

The `Values` property optionally contains a comma-separated list of accepted values. If it is missing, all values are accepted.

The `HasClaimTemplate` defines the content displayed to the users who have the `Claim` with at least one of accepted values.

The `HasNotClaimTemplate` defines the content displayed to other users.

By default, the control is hidden completely to the users who are not authenticated. If you want to display the `HasNotClaimTemplate`
even to the anonymous users, set the `HideForAnonymousUsers` property to `false`. 