using System.ComponentModel.DataAnnotations;
using Konyvtari_nyilvantarto.Validations;

namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object for displaying a summary list of loans for a specific reader,
    /// including book details and calculated late fees.
    /// </summary>
    public class ReaderLoanListDto
    {
        /// <summary>
        /// The full name of the reader.
        /// </summary>
        public string ReaderName { get; set; } = string.Empty;

        /// <summary>
        /// The title of the borrowed book.
        /// </summary>
        public string BookTitle { get; set; } = string.Empty;

        /// <summary>
        /// The author of the borrowed book.
        /// </summary>
        public string BookAuthor { get; set; } = string.Empty;

        /// <summary>
        /// The date the book was borrowed.
        /// </summary>
        public DateOnly LoanDate {get; set;}

        /// <summary>
        /// The deadline date for returning the book.
        /// </summary>
        public DateOnly DueDate {get; set;}

        /// <summary>
        /// The actual date the book was returned. A null value indicates the book is still currently borrowed.
        /// </summary>
        public DateOnly? ReturnDate {get; set;}

        /// <summary>
        /// The dynamically calculated late fee for an overdue book. 
        /// The fee increases progressively based on the number of days late.
        /// </summary>
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