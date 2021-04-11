# Viewmodel protection

Sometimes, you may need to persist some value between postbacks, but you don't want to show it to the users, or allow them to change it.

If you put the value in a viewmodel property, by default it can be read and modified by the client, even if you don't display it in the view or offer a UI control that can edit the value. 

If the users open the browser developer tools, they can access and manipulate with the viewmodel. Or they can use some HTTP proxy to read or modify data that the browser exchanges with the server.

> Always validate any values that come from the client and make sure the user has a permission to modify them! 

In order to protect data in the viewmodel from being read or modified, you can use the `Protect` attribute.

## Encrypt properties

```CSHARP
[Protect(ProtectMode.EncryptData)]
public string SecretValue { get; set; }
```

If you use the setting above, the value will not appear in the viewmodel directly. Instead, it will be stored in the `$encryptedValues` entry of the viewmodel. 

This entry is encrypted and signed, so no one except the server should be able to read it. If the signature doesn't match, DotVVM will throw an exception and refuse to process the request. 

> Properties marked with the encryption turned on cannot be used in the `value` bindings.

## Sign properties

Sometimes, you need to display the value to the client, but you don't want the user to be able to modify it. In such cases, you can sign the property value.

```CSHARP
[Protect(ProtectMode.SignData)]
public string ClientReadOnlyValue { get; set; }
```

If you use the setting above, the value will be present in the viewmodel, but it will also be stored in the `$encryptedValues` section.

When the server deserializes the viewmodel, the property value will be ignored. Instead, the value from the `$encryptedValues` section will be extracted and used as the value of the property.

The attackers will be able change the property value in the client viewmodel, however when they try to invoke a [command](~/pages/concepts/respond-to-user-actions/commands), their change will be ignored because the value of the property will be extracted from the encrypted section.

## Improve security by placing primary key in the URL

DotVVM uses the URL and current user identity to derive an encryption key. It means that the URL and user identity are protected by default. 

If anyone captures a viewmodel with encrypted values, they won't be able to use the encrypted values to make a postback under a different user identity, or on a different URL.

> To improve the security, we strongly encourage you to include primary keys of data displayed on the page (e.g. the ID of currently displayed order) in the URL. If you store such information in the viewmodel to use it as an argument in postbacks, you should sign or encrypt such value to prevent the viewmodel being used to modify another record.

## Notes

The encryption and signing uses the `IDataProtectionProvider` interface which is a part of the ASP.NET infrastructure.

* In ASP.NET Core, the encryption and signing uses the `app.AddDataProtection()` method to retrieve the default data protection provider. This method is declared in the `Microsoft.AspNetCore.DataProtection` NuGet package.

* In OWIN version, DotVVM uses the `appBuilder.GetDataProtectionProvider()` method to retrieve the default data protection provider. This method is declared in the `Microsoft.Owin.Security` NuGet package. 

## See also

* [Viewmodels overview](overview)
* [Binding direction](binding-direction)
* [Commands](~/pages/concepts/respond-to-user-actions/commands)