# Validation overview

DotVVM supports the _Model validation_ mechanism known from other ASP.NET technologies, like MVC, Web API or Web Forms. 

The validation is triggered [commands](~/pages/concepts/respond-to-user-actions/static-commands). 

To use validation in DotVVM, you need to decide three things:

* **What is validated**: By default, every command triggers the validation for the entire viewmodel. You can change the [validation target](target) to some child object in the viewmodel, so only a part of the viewmodel will be validated for the particular command. You can even [disable the validation](#disable-validation) for particular controls entirely.

* **How does it look like**: When validation errors are found, they need to be indicated to the user. We have several [validation controls](controls) which can display error messages, apply CSS classes on invalid form fields, and so on. 

* **Define validation rules**: You need to specify the validation rules for properties, or even objects in the viewmodel. How to specify the rules is descibed in the rest of this chapter.

> Validation in [static commands](~/pages/concepts/respond-to-user-actions/static-commands) is currently not supported, but the work on this feature has already been started, and it will be available in the future releases of DotVVM.

## Define validation rules 

There are three ways how you can define the validation rules. You can use __validation attributes__ to define rules for individual viewmodel properties, you can implement your own validation logic by implementing the `IValidatableObject` interface on objects in the viewmodel, and you can add errors in the `ModelState` object directly.

### Validation attributes

You can validate the viewmodel properties by applying the validation attributes from the `System.ComponentModel.DataAnnotations` namespace. 

This is useful when you need to check the format of individual properties. 

The most commonly used attributes are:

* `Required` - value must not be empty
* `EmailAddress` - value must be in a format of an e-mail address
* `Range` - value must be a number from a specified range
* `RegularExpression` - value must match the specified regular expression
* `Compare` - value must be the same as in another property

Most attributes can specify either the `ErrorMessage` argument directly, or use the `ErrorMessageResourceType` and `ErrorMessageResourceName` arguments to provide localized error messages from a resource file.

```CSHARP
[Required]
public string NewTaskTitle { get; set; }

[Required(ErrorMessage = "The password is required!")]
public string Password { get; set; }

[Required(ErrorMessageResourceType = typeof(MyResourceFile), ErrorMessageResourceName = "PasswordLabel")]
public string Password { get; set; }
```

You can even use custom validation attributes (by creating a class which implements the `IValidationAttribute` interface). 

> Please note that is't not easy to use dependency injection in validation attributes (they are static), so it may be difficult to validate business rules (which may need to look in the database). For example, creating a validation attribute which checks the uniqueness of the e-mail address in the database, is not a good idea. Use the [ModelState](#using-modelstate) to report violations of business rules.

### IValidatableObject

In order to validate consistency of an entire object, any class can implement the `IValidatableObject` interface and use its `Validate` method to return a list of validation errors:

```CSHARP
using DotVVM.Framework.ViewModel.Validation;

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
            yield return this.CreateValidationResult<AppointmentData>(
                "The begin date of the appointment must be lower than the end date.", 
                t => t.BeginDate, t => t.EndDate    // one (or more) expressions indicating which properties are invalid
            );
        }
    }
}
```

This is helpful to provide _formal validation rules_ that verify the state of the object. 

> Please note that this method doesn't make it easy for dependency injection, so it may be difficult to validate business rules (which may need to look in the database). For example, placing the detection of overlapping appointments in the `Validate` method is not a good idea. Use the [ModelState](#using-modelstate) to report violations of business rules.

### ModelState

By default, the validation is triggered automatically on all postbacks. When all validation attributes and `IValidatableObject` rules pass, the [command](~/pages/concepts/respond-to-user-actions/commands) method is invoked. You can perform additional validation checks in the command method itself and report additional validation errors to the user. 

This is commonly used to perform validations of _business rules_ which often require access to the database or other resources, e. g. to make sure that an e-mail address is not registered yet. It would be difficult to do such checks in validation attributes, or in the `IValidatableObject` implementation.

The [request context](~/pages/concepts/viewmodels/request-context) contains the `ModelState` object which holds a list of validation errors. You can add your own errors to the collection to report violations of business rules to the users.

If you need to report the errors to the user, you can use the `Context.FailOnInvalidModelState()` which interrupts execution of the current HTTP request and returns the `ModelState` errors to the user. The browser will show the validator controls, same as for the validation errors provided by attributes or `IValidatableObject`.

In the following sample, we validate the `EmailAddress` property using the validation attributes. When these checks pass, the `Subscribe` method is invoked. 

If the registration of the user fails (we use a custom exception to indicate the cause of the problem), we add a validation error to the `Context.ModelState.Errors` collection and return the validation errors to the user. You can use a useful extension method `AddModelError`.

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
            this.AddModelError(v => v.EmailAddress, "The e-mail address is already registered!");

            // interrupt request execution and report the validation errors
            Context.FailOnInvalidModelState();
        }
        ...
    }
}
```

You can see that the business rule itself is handled in the business layer of the application (i. e. in some `subscriptionService`). The business layer throws an exception which is then caught in the viewmodel and interpreted as a validation error. 

Since this pattern is quite common in DotVVM applications, you can use [filters](~/pages/concepts/viewmodels/filters/overview) to transform business layer exceptions to model state errors globally. 

## Disable validation

You can also disable validation on a part of the page or on a specific control, by using `Validation.Enabled="false"`. You often need to do this e. g. for delete and cancel buttons where the values in the form don't need to be valid. 

Also, you might need this e.g. on the `Changed` event of `TextBox` when you need to pre-fill some values for the user, but the form may not be complete yet, and thus it is not valid at such time.

```DOTHTML
<dot:TextBox Text="{value: Address}" 
             Changed="{command: GetGpsLocationForAddress()}"
             Validation.Enabled="false" />
<!-- There are additional fields in the form which are required, but we need the command to be executed even if these fields are empty. -->
```

> If you turn on the **debug mode** in the [configuration](~/pages/concepts/configuration), a red notification in the top right corner of the screen appears if the postback was canceled because of validation errors. This helps you to discover validation errors when you don't have [validator controls](controls) on the page yet.

## See also

* [Validation target](target)
* [Validation controls](controls)
* [Validator control](~/controls/builtin/Validator)
* [ValidationSummary control](~/controls/builtin/ValidationSummary)
* [Client-side validation](client-side)
* [Extensibility](extensibility)
