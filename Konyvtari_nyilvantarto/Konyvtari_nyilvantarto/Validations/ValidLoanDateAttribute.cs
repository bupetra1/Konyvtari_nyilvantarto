using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto.Validations
{
    public class ValidLoanDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime date && date.Date < DateTime.Today)
            {

                return new ValidationResult("Loan date cannot be earlier than the current day!");
                
            }

            return ValidationResult.Success;
            
        }  
    }
}