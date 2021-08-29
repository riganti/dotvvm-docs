# Create pages and controls

The [DotVVM Command-Line tool](install) can create new [pages](~/pages/concepts/dothtml-markup/overview), [master pages](~/pages/concepts/layout/master-pages), or [markup controls](~/pages/concepts/control-development/markup-controls) in ASP.NET Core projects.

> Creating pages, master pages, and controls is supported only in ASP.NET Core projects using the new project system. For .NET Framework projects, we recommend [using Visual Studio](~/pages/quick-starts/build/first-page).


## Common syntax

All creation commands have the following structure:

```bash
dotnet dotvvm add [<target>] <kind> [options] <name>
```

* `[<target>]` - an optional path to the DotVVM project where a new item should be created. If left unspecified, the current working directory is used.
* `<kind>` - the kind of the item being added.
* `[options]` - options specific to the kind of item being added. See sections below for details.
* `<name>` - the name of the item being added.


## Add a page or a Master page

### Syntax

```bash
dotnet dotvvm add [<target>] page [options] <name>
dotnet dotvvm add [<target>] master [options] <name>
```

### Options

* `-m, --master <master>` - the @master page of the new page.
* `-d, --directory <directory>` - the directory where the new page is to be placed [default: Views/].


## Add a ViewModel

### Syntax

```bash
dotnet dotvvm add [<target>] viewmodel [options] <name>
```

### Options

* `-d, --directory <directory>` - the directory where the new ViewModel is to be placed [default: ViewModels/].
* `-b, --base <base>` - the base class of the ViewModel.


## Add a Markup control

### Syntax

```bash
dotnet dotvvm add [<target>] control [options] <name>
```

### Options

* `-d, --directory <directory>` - the directory where the new control is to be placed [default: Controls/].
* `-c, --code-behind` - creates a C# code-behind class for the control.


## Example

1. Create the `Views/Site.dotmaster` master page:

```
dotnet dotvvm add master Site
```

2. Create the `Views/Page1.dothtml` page and embed it in the `Views/Site.dotmaster`:

```
dotnet dotvvm add page -m Site Page1
```

3. Create the `Controls/MyControl.dotcontrol` user control with the code behind file:

```
dotnet dotvvm add control -c Controls/MyControl.dotcontrol
```


## See also

* [Install DotVVM CLI](install)
* [Lint DotHTML](lint-dothtml)
* [Generate REST API clients](generate-rest-api-clients)
