using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Konyvtari_nyilvantarto.Dtos
{
    public class ReaderDto
    {
        [Required(ErrorMessage ="Name is required!")]
        public string Name {get; set;} = string.Empty;
        [Required(ErrorMessage ="Address is required!")]
        public string Address {get; set;} = string.Empty;
        
        [Required(ErrorMessage ="BirthDate is required!")]
        [ValidBirthDate]
        public DateOnly BirthDate {get; set;}
    }
}