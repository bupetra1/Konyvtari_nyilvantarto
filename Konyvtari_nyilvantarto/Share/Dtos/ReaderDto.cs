using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    public class ReaderDto
    {
        public int ReaderId {get; set;}
        [Required(ErrorMessage ="Name is required!")]
        public string Name {get; set;} = string.Empty;
        [Required(ErrorMessage ="Address is required!")]
        public string Address {get; set;} = string.Empty;
        
        [Required(ErrorMessage ="BirthDate is required!")]
        [ValidBirthDate]
        public DateOnly BirthDate {get; set;}
    }
}