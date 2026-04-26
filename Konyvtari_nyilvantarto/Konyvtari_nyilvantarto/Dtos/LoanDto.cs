using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Konyvtari_nyilvantarto
{
    public class LoanDto
    {
        public int ReaderId {get; set;}
        public string ReaderName { get; set; } = string.Empty;
        public int BookId {get; set;}
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;

        [Required(ErrorMessage ="Loan date is required!")]
        [ValidLoanDate]
        public DateTime LoanDate {get; set;}

        [Required(ErrorMessage ="Due date is required!")]
        public DateTime DueDate {get; set;}
        public int? LateFee {get; set;}
        
        [ValidReturnDate]
        public DateTime? ReturnDate {get; set;}
    }
}