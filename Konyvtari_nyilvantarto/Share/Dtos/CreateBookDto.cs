using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    public class CreateBookDto
    {
        [Required(ErrorMessage ="Book title is required!")]
        public string Title{get; set;} = string.Empty;

        [Required(ErrorMessage ="Book author is required!")]
        public string Author{get; set;} = string.Empty;
        public string? Publisher{get; set;}

        [ValidPublicationYear]
        public int? PublicationYear {get; set;}
    }
}