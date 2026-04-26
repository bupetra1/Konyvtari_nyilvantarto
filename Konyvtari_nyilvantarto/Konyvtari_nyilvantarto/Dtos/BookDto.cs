using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto
{
    public class BookDto
    {
        public int Id{get; set;}

        [Required(ErrorMessage ="Book title is required!")]
        public string Title{get; set;}

        [Required(ErrorMessage ="Book author is required!")]
        public string Author{get; set;}
        public string? Publisher{get; set;}
        public DateTime? PublicationYear {get; set;}
    }
}