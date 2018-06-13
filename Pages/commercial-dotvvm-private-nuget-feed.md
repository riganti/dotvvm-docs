# DotVVM Private NuGet Feed

The [Bootstrap for DotVVM](/landing/bootstrap-for-dotvvm) controls are not available on the official NuGet feed.

Instead, we have our own private NuGet feed which contains all commercial products we have. You can also find pre-release versions of DotVVM Core in this feed.
This feed requires authentication and it will give you only the packages you have purchased.

When you install the [DotVVM for Visual Studio 2015](/landing/dotvvm-for-visual-studio-extension) and sign in, the extension will offer to install 
the *DotVVM Private Nuget Feed* for you. 

<br />

## Adding the Feed Manually

If you don't see the **DotVVM Private NuGet Feed**, you can also use these manual steps to install it.

1. Locate the `nuget.exe` tool, or [download it from the NuGet website](https://dist.nuget.org/index.html). 

2. Open command line in the folder with `nuget.exe` and run this script (don't forget to fill your own credentials):

```
nuget sources Add -Name "Dotvvm Feed" -Source "https://www.dotvvm.com/nuget/v3/index.json" -UserName "YOUR EMAIL ADDRESS" -Password "YOUR PASSWORD"
```

<br />

## Troubleshooting

1. If the **DotVVM for Visual Studio** fails to install the NuGet feed, make sure that you have the latest version of **NuGet Package Manager** extension
installed. You can update it in the **Tools > Extensions and Updates** menu in Visual Studio.

2. If you don't see any packages in the Dotvvm Private NuGet Feed, make sure you have [activated your license](/customer/profile).

3. Some packages in the feed are still not in final version. If you want to use beta versions, make sure you have checked the **Include Prerelease** box.