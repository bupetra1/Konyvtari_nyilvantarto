using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto.Validations
{
    public class ValidPublicationYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is int year && year < 0)
            {

                return new ValidationResult("Year cannot be negative!");
                
            }

            return ValidationResult.Success;
        }
    }
}