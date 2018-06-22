# Master Pages

In most web apps, you need all pages to share the header, menu and footer. **DotVVM** supports a mechanism called **Master Pages**. 
If you are familiar with _ASP.NET Web Forms_, the concept of master pages is exactly the same.

There are two kinds of pages:

1. The **Master Page** is a file with `.dotmaster` extension. It defines the layout of the page including the `<html>`, `<head>` and `<body>` tags.
The master page can contain one or more `<dot:ContentPlaceHolder>` controls. These controls mark the places where the content of the page will be placed.     

2. The **Content Pages** is a file with the `.dothtml` extension. This page contain one or more `<dot:Content>` controls. The contents of these controls 
will be placed in the corresponding `ContentPlaceHolder`s in the master page.  

<div>
<ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active">
        <a href="#sample1_masterpage" role="tab" data-toggle="tab">site.dotmaster</a>
    </li>        
    <li role="presentation">
        <a href="#sample1_contentpage" role="tab" data-toggle="tab">default.dothtml</a>
    </li>
</ul>
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="sample1_masterpage">

```DOTHTML
    @viewModel DotvvmSample.ViewModels.SiteViewModel, DotvvmSample

    <html>
    <head>...</head>
    <body>
        <header>
            <!-- Common header, menu, logo etc. -->
        </header>
        <section>
            <!-- The content will be placed inside -->
            <dot:ContentPlaceHolder ID="MainContent" />
        </section>
        <footer>
            ...
        </footer>
    </body>
    </html>
```

</div>
<div role="tabpanel" class="tab-pane" id="sample1_contentpage">

```DOTHTML
    @viewModel DotvvmSample.ViewModels.DefaultViewModel, DotvvmSample
    @masterPage Views/site.dotmaster

    <dot:Content ContentPlaceHolderID="MainContent">
        <!-- This content will be placed inside the dot:ContentPlaceHolder control -->
    </dot:Content>
```

</div>
</div>
</div>

The content page uses the `@masterPage` directive at the top. This directive specifies which master page will be used. 

The path in the `@masterPage` must be a relative path to the `.dotmaster` file from the root directory of the website.

## Routing

The master pages should NOT be registered in the route table in the `DotvvmStartup.cs` file. They work only as templates for the content pages. 

The route always points to the `.dothtml` file. If the `@masterPage` directive is found, the contents are embedded in the placeholders from the specified master page.

## ViewModels of the Master Page

Even the master page must specify the `@viewmodel` directive. This is required because all bindings are compiled and DotVVM needs to know the viewmodel type.

The type specified in the `@viewModel` directive doesn't have to be a class. It can be an interface. 

The viewmodel of the content page must be assignable to the viewmodel type of the master page.

+ If the viewmodel of the master page is an interface, the content page viewmodel must implement this interface.

+ If the viewmodel of the master page is a class, the content page viewmodel must inherit from this viewmodel (or be the same as the master page viewmodel).

## Master Page Nesting

You can also nest a master page in another master page and so on. Just use the `@masterPage` directive in the master page to specify parent master page.

## Default PlaceHolder Content

The `<dot:ContentPlaceHolder>` doesn't have to be empty. If the content page has no corresponding `<dot:Content>` for any placeholder,
the content of the `ContentPlaceHolder` will be used as a default value.

```DOTHTML
    <dot:ContentPlaceHolder ID="OptionalContent">
        <!-- This will be visible if there is no dot:Content with ContentPlaceHolderID="OptionalContent" in the content page. -->
    </dot:ContentPlaceHolder>
```

## How to Create the Master Page

All you have to do is to choose **DotVVM Master Page** template in the **New Item** dialog.

<p><img src="{imageDir}basics-master-pages-img2.png" alt="Creating a Master Page" /></p>

After you confirm the selection, another window will appear. In this window, you can specify the name and location of the class that will be 
the viewmodel of the master page. 

If you already have a base viewmodel or an interface for the master page viewmodel, just uncheck the _Create ViewModel_ option.

Make sure that the master page uses the correct viewmodel. If not, delete the `@viewModel` directive and the IntelliSense will help you
to pick the correct class or interface.

<p><img src="{imageDir}basics-master-pages-img3.png" alt="Picking the correct class" /></p>

Finally, write your HTML code. On all places where you need to embed something from the content page, use the `<dot:ContentPlaceHolder>` control.

```DOTHTML
<dot:ContentPlaceHolder ID="SomeUniqueId">
</dot:ContentPlaceHolder>
```

Most master pages need only one or two placeholders. You can use as many `ContentPlaceHolder`s as you need however.

> The commercial version of [DotVVM for Visual Studio](/landing/dotvvm-for-visual-studio-extension) can show the IntelliSense for the `ContentPlaceHolderId` 
property and much more. 



## How to Create the Content Page

Creating the content page is very similar. Just add a new **DotVVM Page** and in the dialog window, and don't forget to click on the **Embed in Master Page** checkbox.

Then, select the correct master page from the list.

<p><img src="{imageDir}basics-master-pages-img4.png" alt="Embedding a page in the master page" /></p>

The wizard will generate the `<dot:Content>` controls based on the `<dot:ContentPlaceHolder>` controls present in the master page.

> Don't forget that the viewmodel in the content page must be assignable to the viewmodel type specified in the master page.
