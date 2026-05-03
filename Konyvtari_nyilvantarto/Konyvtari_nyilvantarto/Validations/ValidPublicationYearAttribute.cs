using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto.Validations
{
    public class ValidPublicationYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is int year)
            {
                if(year < 0)
                {
                    return new ValidationResult("Publication year cannot be negative!");
                }
                if(year > DateTime.Now.Year)
                {
                    return new ValidationResult("Publication year cannot exceed the current year!");
                }
            }
            return ValidationResult.Success;
        }
    }
}