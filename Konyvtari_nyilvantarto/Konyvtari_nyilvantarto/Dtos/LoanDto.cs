using System.ComponentModel.DataAnnotations;

namespace Konyvtari_nyilvantarto
{
    public class LoanDto
    {
        public int ReaderId {get; set;}
        public string ReaderName { get; set; }
        public int BookId {get; set;}
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        
        [Required(ErrorMessage ="Loan date is required!")]
        public DateTime LoanDate {get; set;}

        [Required(ErrorMessage ="Due date is required!")]
        public DateTime DueDate {get; set;}
        public int? LateFee {get; set;}
        public DateTime? ReturnDate {get; set;}
    }
}