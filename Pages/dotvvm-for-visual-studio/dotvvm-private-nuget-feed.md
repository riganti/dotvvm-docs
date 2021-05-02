# Using DotVVM private NuGet feed

The [Bootstrap for DotVVM](https://www.dotvvm.com/products/bootstrap-for-dotvvm) and [DotVVM Business Pack](https://www.dotvvm.com/products/dotvvm-business-pack) controls are not available on the official NuGet feed, but they are shipped via **DotVVM Private NuGet Feed**.

This feed requires authentication and it will show only the packages you have purchased.

> The licenses for DotVVM components come with 12 months of updates. If you purchase DotVVM Business Pack or Bootstrap for DotVVM, you will see all historic versions of the package on the private NuGet feed. Within the next 12 months, all versions that we release will appear on the NuGet feed automatically. If you don't renew the license, you will still see all versions you'd be able to access while your license was valid.

## Sign in to add the feed automatically

When you install the [DotVVM for Visual Studio](https://www.dotvvm.com/get-dotvvm) extension and run Visual Studio for the first time, the extension will display a welcome window which will allow you to sign in. 

If you've installed the extension earlier and cannot see the window, you can also open the sign in dialog by navigating to the **Extensions > DotVVM > About** menu item.

If you sign in in the extension, it will offer you to _install the DotVVM Private Nuget Feed automatically_. 

## Adding the feed manually

If you don't see the DotVVM Private NuGet Feed in your NuGet sources, or if you want to set up the private feed to be used on your build server where you don't have Visual Studio, you can take the following manual steps:

1. Locate the `nuget.exe` tool, or [download it from the NuGet website](https://dist.nuget.org/index.html). 

2. Open command line in the folder with `nuget.exe`, and run this script (don't forget to fill your own credentials):

```
nuget sources Add -Name "Dotvvm Feed" -Source "https://www.dotvvm.com/nuget/v3/index.json" -UserName "YOUR EMAIL ADDRESS" -Password "YOUR PASSWORD"
```

## Troubleshooting

1. If the **DotVVM for Visual Studio** fails to install the NuGet feed, make sure that you have the latest version of **NuGet Package Manager** extension
installed. You can update it in the **Tools > Extensions and Updates** menu in Visual Studio.

2. If you don't see any packages in the Dotvvm Private NuGet Feed, make sure you have [activated your license](https://www.dotvvm.com/customer/profile). After you purchase the licenses, you need to assign them to a specific user account.

3. Some packages in the feed are still not in final version. If you want to use pre-release versions, make sure you have checked the **Include Prerelease** box.

## See also

* [DotVVM for Visual Studio overview](overview)
* [Troubleshooting](troubleshooting)
* [Release notes](release-notes)