## Sample 4: Restrictions

If you require more granular control over what dates can be selected, you can use the `Restrictions` property. We currently support the following types of restrictions:

- **DayOfWeekRestriction** - Allows to disable selection of a specific day of week. You can also specify the time interval you need to disable using the `StartTime` and `EndTime` properties.
- **DateRangeRestriction** - Allows to disable a secific range of dates (one day, one month, etc.). You just need to set the `StartDate` and `EndDate` properties.

Restrictions can be combined with `MinDate` and `MaxDate` properties and are verified both on client-side and server-side.