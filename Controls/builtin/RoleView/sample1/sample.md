## Sample 1: RoleView

The `Roles` property contains a comma-separated list of roles. 

The `IsMemberTemplate` defines the content displayed to the users who are member of at least one of the role in the `Roles` property.

The `IsNotMemberTemplate` defines the content displayed to other users.

By default, the control is hidden completely to the users who are not authenticated. If you want to display the `IsNotMemberTemplate`
even to the anonymous users, set the `HideForAnonymousUsers` property to `false`.