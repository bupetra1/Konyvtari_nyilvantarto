using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Share.Dtos
{
    public class ReaderLoanListDto
    {

        public string ReaderName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;
        public DateOnly LoanDate {get; set;}
        public DateOnly DueDate {get; set;}
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