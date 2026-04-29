using Konyvtari_nyilvantarto.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Share
{
    public class LoanDTO
    {
        public int ReaderId { get; set; }
        public string ReaderName { get; set; } = string.Empty;
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loan date is required!")]
        [ValidLoanDate]
        public DateTime LoanDate { get; set; }

        [Required(ErrorMessage = "Due date is required!")]
        public DateTime DueDate { get; set; }
        public int LateFee { get; set; }

        [ValidReturnDate]
        public DateTime? ReturnDate { get; set; }
    }
}
