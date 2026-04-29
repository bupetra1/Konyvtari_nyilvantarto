using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto.Validations
{
    public class ValidReturnDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime date && validationContext.ObjectInstance is LoanDto loan)
            {

                if(date < loan.LoanDate){    
                    return new ValidationResult("Return date cannot be earlier than the loan day!");
                }
            }

            return ValidationResult.Success;
            
        }  
    }
}