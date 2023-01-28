# Use Bootstrap 5 Dark Theme

Bootstrap for DotVVM allows you to change a default Bootstrap theme to customize the look and feel of their application. 
This feature is available through a new attached property, `Theme.Value`. The property can be added to any HTML tag or control and support binding or hard-coded values.

## Example

First, let's add theme property to the `body`. This way, the theme change will be applied to all components on the page.

```html
@viewModel DotVVM.Bootstrap5.Samples.ViewModels.MasterPageViewModel, DotVVM.Bootstrap5.Samples
<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>DotVVM.Bootstrap5.Samples</title>
</head>
<body Theme.Value="{value: Theme}">
    <main>
        <dot:ContentPlaceHolder ID="MainContent" />
    </main>
</body>
</html>
```

To make the binding work, `MasterPageViewModel` should have a public `Theme` property. 

```CSHARP
public class MasterPageViewModel : DotvvmViewModelBase
{
    public BootstrapTheme Theme { get; set; } = BootstrapTheme.Light;
}
```

Let's add a simple View, where `NavBar` control above includes a `NavBarDropDown` that will change the global theme.

```html
<bs:Container ID="TestContainer">
    <bs:NavBar data-ui="default">
        <BrandTemplate>
            <img height="30rem" src="/Resources/Images/tree.svg" />
        </BrandTemplate>
        <bs:NavBarSection>
            <bs:NavBarLink>
                Link to the first Page
            </bs:NavBarLink>
            <bs:NavBarLink>
                Link to the second Page
            </bs:NavBarLink>
        </bs:NavBarSection>
        <bs:NavBarSection>
            <bs:NavBarDropDown HeaderText="Change theme">
                <bs:DropDownItem Click="{command: ChangeToLight()}">
                    <bs:Icon Type="Sun_Fill" /> Change to Light
                </bs:DropDownItem>
                <bs:DropDownItem Click="{command: ChangeToDark()}">
                    <bs:Icon Type="Moon_Fill" /> Change to Dark
                </bs:DropDownItem>
        </bs:NavBarSection>
    </bs:NavBar>
    <bs:Alert Type="Primary" HeaderText="Hey!"
              IsDismissible="true"
              Text="Boostrap for DotVVM allows you to change a default Boostrap theme to customize the look and feel of their application.">
    </bs:Alert>
    <bs:Alert Type="Warning" HeaderText="Hey!"
              IsDismissible="true"
              Text="Boostrap for DotVVM allows you to change a default Boostrap theme to customize the look and feel of their application.">
    </bs:Alert>
    <bs:Alert Type="Success" HeaderText="Hey!"
              IsDismissible="true">
        <bs:Row Flex.AlignItemsAll="Start">
            <bs:Column>
                Note on the left
            </bs:Column>
            <bs:Column>
                Note on the right
            </bs:Column>
        </bs:Row>
    </bs:Alert>
</bs:Container>
```

Corresponding ViewModel includes only two methods.

```CSHARP
public void ChangeToLight()
{
    Theme = BootstrapTheme.Light;
}
public void ChangeToDark()
{
    Theme = BootstrapTheme.Dark;
}
```