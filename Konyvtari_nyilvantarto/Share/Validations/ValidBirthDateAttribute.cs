using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto.Validations
{
    public class ValidBirthDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime date)
            {
                if(date.Year < 1900)
                {
                    return new ValidationResult("Year cannot be earlier than 1900!");
                }

                if (date > DateTime.Now)
                {
                    return new ValidationResult("Birthdate cannot be in the future!");
                }
            }

            return ValidationResult.Success;
            
        }  
    }
}