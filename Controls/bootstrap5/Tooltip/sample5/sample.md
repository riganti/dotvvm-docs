## Sample 5: HTML sanitization

All HTML passed into `Tooltip` by default goes through Bootstraps HTML sanitizer.
HTML sanitizer filters out all non whitelisted tags and attributes.  
[List of allowed tags and attributes.](https://getbootstrap.com/docs/4.3/getting-started/javascript/#sanitizer)  

To disable HTML sanitization you must set `DisableHtmlSanitization` to true.

>Without sanitization you could have be vulnerable to XSS attacks.