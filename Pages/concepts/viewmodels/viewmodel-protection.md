# ViewModel Protection

Sometimes, you may need to persist some value between postbacks, but you don't want to show it to the client
or allow him to change it.

If you put the value in some viewmodel property, it can be read and modified by the client, even if you don't display it in the view. If the user opens the browser developer tools, he can access and manipulate with the viewmodel. Or he can use some HTTP proxy to read or modify data that the browser exchanges with the server.

> Always validate any values that come from the client and check that the user has a permission to modify them! 

In order to protect data in the viewmodel from being read or modified, you can use the `Protect` attribute.

## Encrypt ViewModel Property

```CSHARP
[Protect(ProtectMode.EncryptData)]
public string SecretValue { get; set; }
```

If you use the setting above, the value will not appear in the viewmodel directly. Instead, it will be stored in the
`$encryptedValues` entry of the viewmodel. This entry is encrypted and signed, so no one should be able to read it. If the signature doesn't match, DotVVM will throw an exception and refuse to process the request. 

> Properties marked with the encryption turned on cannot be used in the `value` bindings.   

## Sign ViewModel Property

Sometimes, you need to display the value to the client, but you don't want the client to be able to modify it.
You can sign the property value.

```CSHARP
[Protect(ProtectMode.SignData)]
public string ClientReadOnlyValue { get; set; }
```

If you use the setting above, the value will be present in the viewmodel and in addition, it will be stored in the `$encryptedValues` section.

When the server deserializes the viewmodel, the property value will be ignored. Instead, the value from the `$encryptedValues` section will be extracted and stored in the property.

The attackers will be able change the property value in the client viewmodel, however when they try to do the postback, their change will be ignored on the server because the value of the property is extracted from the encrypted section.


## Notes

The encryption and signing uses the `IDataProtectionProvider` interface. 

In OWIN version, DotVVM uses the `appBuilder.GetDataProtectionProvider()` method to retrieve the default data protection provider. This method is declared in the `Microsoft.Owin.Security` NuGet package. 

In ASP.NET Core version, the encryption and signing uses the `app.AddDataProtection()` method to retrieve the default data protection provider. This method is declared in the `Microsoft.AspNetCore.DataProtection` NuGet package.

DotVVM uses the URL and current user identity to derive an encryption key. It means that also the URL and user identity is protected by default. If anyone captures a viewmodel with encrypted values, he won't be able to use the encrypted values to make a postback under a different user identity or to a different URL.

> To improve the security, we strongly encourage you to include primary keys of data displayed on the page (e.g. the ID of currently displayed order) in the URL. If they are stored in the viewmodel, you should sign or encrypt them to prevent the viewmodel being used to modify another record.
