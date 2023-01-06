using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class ViewModel : DotvvmViewModelBase
{
    public bool Value { get; set; }

    public List<string> ResultItems { get; set; } = new List<string>();

    public void Validate()
    {

    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Value)
        {
            yield return new ValidationResult($"Value must be true.", new[] { nameof(Value) });
        }

        if (!ResultItems.Any())
        {
            yield return new ValidationResult($"At least one checkbox must be checked", new[] { nameof(ResultItems) });
        }
    }
}