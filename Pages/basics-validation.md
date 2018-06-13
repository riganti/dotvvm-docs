# Validation

DotVVM supports the _Model Validation_ mechanism known from other ASP.NET technologies, like MVC, Web API or Web Forms. 

You can validate either the whole viewmodel, or specific child object. By default, every button triggers the validation on the whole viewmodel. You can change the validation target to some child object of the viewmodel. You can also disable the validation for particular buttons.

You can use the default validation attributes, write your own ones, and add your own validation logic by implementing the `IValidatableObject` interface.

## Validation Attributes

You can validate the viewmodel properties by applying the validation attributes on them, e.g. the `Required` attribute.

Optionally, you can set the `ErrorMessage` parameter directly, or use the `ErrorMessageResourceType` and `ErrorMessageResourceName` of the `Required` attribute to get localized error message from the resource file.

```CSHARP
[Required]
public string NewTaskTitle { get; set; }

[Required(ErrorMessage = "The password is required!")]
public string Password { get; set; }

[Required(ErrorMessageResourceType = typeof(MyResourceFile), ErrorMessageResourceName = "PasswordLabel")]
public string Password { get; set; }
```

You can use custom validation attributes (they implement the `IValidationAttribute` interface).

## Complex Validation Logic

In order to support complex scenarios, the viewmodel can implement the `IValidatableObject` interface and use the `Validate` method to return a list of validation errors:

```CSHARP
public class AppointmentData : IValidatableObject
{
    [Required]
    public DateTime BeginDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BeginDate >= EndDate)
        {
            yield return new ValidationResult(
                "The begin date of the appointment must be lower than the end date.",
                new[] { nameof(BeginDate) }     // path of the property
            );
        }
    }
}
```

## Working With ModelState

By default, the validation is triggered automatically on all postbacks. When all validation attributes and `IValidatableObject` rules pass, the [command binding](/docs/tutorials/basics-command-binding/{branch}) is executed.

You can perform additional validation checks in the command method itself and report additional validation errors to the user. 
This is used to perform validations that require access to SQL database, e.g. make sure that an e-mail address is not registered yet. 
It would be difficult to do these checkes in validation attributes or in the `IValidatableObject` implementation.

The `Context` in DotVVM contains the `ModelState` object which contains a list of validation errors. You can add your own errors to the collection.

If you need to report the errors to the user, you can use the `Context.FailOnInvalidModelState()` which interrupts execution of the current HTTP request and returns the validation errors to the user. The user'w browser will show the validator controls.

In the following sample you can see, that we validate the `EmailAddress` property using the validation attributes. When these checks pass, the `Subscribe` method can be called. If the registration of the user fails (we use a custom exeption to indicate the problem), you can add a validation error to the `Context.ModelState.Errors` collection and return the validation errors to the user.

```CSHARP
public class RegisterViewModel : DotvvmViewModelBase 
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; }

    public void Subscribe() 
    {
        ...

        try 
        {
            subscriptionService.RegisterEmailAddress(EmailAddress);
        }
        catch (EmailAddressAlreadyRegisteredException) 
        {
            // add the error to the list of validation errors
            Context.ModelState.Errors.Add(new ViewModelValidationError() 
            {
                PropertyPath = nameof(EmailAddress),
                ErrorMessage = "The e-mail address is already registered!"
            });

            // interrupt request execution and report the validation errors
            Context.FailOnInvalidModelState();
        }
        ...
    }
}
```