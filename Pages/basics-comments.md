# Comments

Sometimes, you need to comment something out of the display. 

In DotVVM, there are two types of comments.

## Client-side Comment

The **client-side comments** have the same syntax and rules as the HTML comment.

```DOTHTML
<p>
    <!-- The client-side comment is rendered to the HTML output. -->
</p>
```

DotVVM keeps these comments as they are, so they are rendered and sent to the browser.

They are not displayed to the user, but anyone can read them when looking at the page source or using the developer tools in the browser. 

## Server-side Comment

To remove a fragment of code from the rendered output, use the **server-side comments**.

```DOTHTML
<p>
    <%-- This server-side comment is removed from the HTML output. --%>
</p>
```

You can also use server-side comments inside tags, like this:

```DOTHTML
<dot:GridView DataSource="{value: Source}" <%-- server comment in attributes --%> class="table">
    ...
</dot:GridView>
```

> If you want to comment parts of the markup with controls, it is better to use the server-side comment, so the commented code cannot be read by the user.
