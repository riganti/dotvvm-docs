The control renders a classic HTML table:

```DOTHTML
<table>
  <thead>
    <tr>
	  <th><a href="...">Id</a></th>
	  <th><a href="...">Customer Name</a></th>
	</tr>
  </thead>

  <tbody>
    <tr>
	  <td><span data-bind="..."></span></td>
	  <td><span data-bind="..."></span></td>
	</tr>
  </tbody>
</table>
```

In the [Client rendering mode](~/pages/concepts/server-side-rendering), only one row is rendered and the 
Knockout *foreach* binding is applied to the *tbody* element.

In the [Server rendering mode](~/pages/concepts/server-side-rendering), all rows are rendered on the server.