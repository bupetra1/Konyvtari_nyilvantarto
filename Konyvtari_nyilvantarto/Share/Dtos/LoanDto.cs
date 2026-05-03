using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Share.Dtos
{
    public class LoanDto
    {
        public int LoanId {get; set;}
        public int ReaderId {get; set;}
        public string ReaderName { get; set; } = string.Empty;
        public int BookId {get; set;}
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;

        [Required(ErrorMessage ="Loan date is required!")]
        [ValidLoanDate]
        public DateOnly LoanDate {get; set;}

        [Required(ErrorMessage ="Due date is required!")]
        public DateOnly DueDate {get; set;}
        
        [ValidReturnDate]
        public DateOnly? ReturnDate {get; set;}

        public int LateFee
        {
            get
            {
                int daysLate = 0;
                if(ReturnDate is null && DueDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    daysLate = DateOnly.FromDateTime(DateTime.Now).DayNumber - DueDate.DayNumber;
                }
                if(ReturnDate is not null && DueDate < ReturnDate)
                {
                    daysLate = ReturnDate.Value.DayNumber - DueDate.DayNumber;
                }
                return daysLate switch
                {
                    >=1 and <11 => 100*daysLate,
                    >=11 and <16 => 100*daysLate*2,
                    >=16 => 100*daysLate*3,
                    _ => 0

                };
                
            }
        }
    }
}