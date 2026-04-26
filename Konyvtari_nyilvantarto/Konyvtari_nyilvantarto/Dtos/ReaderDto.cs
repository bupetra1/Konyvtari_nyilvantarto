using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Konyvtari_nyilvantarto
{
    public class ReaderDto
    {
        public int Id {get; set;}
        [Required(ErrorMessage ="Name is required!")]
        public string Name {get; set;}
        [Required(ErrorMessage ="Address is required!")]
        public string Address {get; set;}
        
        [Required(ErrorMessage ="BirthDate is required!")]
        [ValidBirthDate]
        public DateTime BirthDate {get; set;}
    }
}