# DotVVM Release Policy

DotVVM open-source framework and the commercial NuGet packages have independent release cycles. The major and minor releases (`x.y`) are synchronized, but each area can publish a revised package (`x.y.z`) or a preview (`x.y.z-previewAA-final`) at any time. 

| Area                                       | Distributed via           |
|--------------------------------------------|---------------------------|
| DotVVM framework + DotVVM.Contrib packages | official NuGet            |
| Bootstrap for DotVVM                       | DotVVM Private NuGet Feed |
| DotVVM Business Pack                       | DotVVM Private NuGet Feed |

## Version compatibility

The basic rule is that the **packages are compatible when the major and minor release numbers are the same**. 

> **DotVVM 4.1.x** is compatible with **DotVVM Business Pack 4.1.x** and **Bootstrap for DotVVM 4.1.x**. 
>
> **DotVVM 4.0.x** is compatible with **DotVVM Business Pack 4.0.x** and **Bootstrap for DotVVM 4.0.x**. 

If you are getting `MissingMethodException` or similar errors which indicate binary incompatibility between DotVVM DLLs, it is most probably caused by using package versions with a different major-minor pair.

## Versioning scheme

There are two types of releases:

* **Stable releases** with a pattern of `x.y.z` (major, minor, revision).

* **Public preview releases** with a pattern of `x.y.z-previewAA-final`. The previews are numbered with a two-digit number (`AA`), but there may be gaps in the sequence because some previews get replaced by newer ones before they are good enough to be published publicly.

## Bugfix policy

We classify bugs based on the impact into three categories:

* **Preview-only impact** - this is a category of bugs which were introduced in a preview release and they will be fixed before the stable release. These issues are fixed *only in the preview releases*.

    > Example: A bug was introduced in *DotVVM 4.2.0-preview03-final*. This bug will be fixed **only in the preview branch** (DotVVM 4.2).

* **Low impact** - this is a category of bugs which happened to be in a stable release. There is small or even no impact on most of the users. When the bug is fixed, a new revision (`x.y.*`) of the latest stable release is published.

    > Example: A bug with low impact was found. The latest stable version at that moment was *DotVVM 4.1.3*. This bug will be fixed only **in the latest stable branch, which is DotVVM 4.1**. It will be published as *DotVVM 4.1.4*.

* **High or security impact** - this is a category of bugs which happened to be in a stable release. The issue has high impact (performance issue on sites with high traffic, incompatibility with new .NET release, or similar), or it is a security-related issue. When the bug is fixed, a new revision of all branches since the last major release (`x.*`) is published. 

    > Example: A bug with high impact was found. The latest stable version at that moment was *DotVVM 4.1.3*. This bug will be fixed **in the all stable branches since DotVVM 4.0**, which means *DotVVM 4.0* and *DotVVM 4.1*.

## Review policy (open-source framework)

Any change in the code is done through a [pull request](https://github.com/riganti/dotvvm/pulls) in the GitHub repository. 

The team has weekly calls where we collectively review each pull request. Our rule is that at least two pairs of eyes must review all changes in the code before the PR can be merged.

## Support policy

We do our best to fix bugs as soon as possible. The time to publishing a fix greatly depends on the quality of the bug report. 

In general, it helps us if the reported issue comes with a simple repro project, or if it contains enough information for us to be able to find the cause. Such issues can be fixed in a matter of days (including the review process on a pull request).

If you need guaranteed time or priority bug fixes, please [contact us](https://www.dotvvm.com/contact). We will be happy to provide paid support services with SLA contract.

