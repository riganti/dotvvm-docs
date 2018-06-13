# Troubleshooting DotVVM for Visual Studio

If you encounter any problems with the DotVVM extension (e.g. IntelliSense not working), try to do the following steps.

<br />

## 1. Update to Latest Version

In Visual Studio, choose the **DotVVM / Check for Updates** menu item and if newer version is available, install it.

<br />

## 2. Check You Are Signed In

Some features from the **Professional** edition work only if you are signed in. 
In Visual Studio, choose the **DotVVM / About** and check whether your license is active and whether you are signed in.

<br />

## 3. Delete the Component Model Cache

Occasionally, some things break in Visual Studio MEF component cache. If you see message boxes that DotVVM package
didn't load correctly, shut down Visual Studio and delete the folder `c:\Users\<your user name>\AppData\Local\Microsoft\VisualStudio\v14.0\ComponentModelCache`
from Windows Explorer.

<br />

If the issue still occurs, <a href="/support">please contact us</a> and provide us with description of the problem and screenshots.
