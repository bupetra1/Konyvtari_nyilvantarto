using System.ComponentModel.DataAnnotations;
using Share.Validations;

namespace Share.Dtos
{
    /// <summary>
    /// Data transfer object representing comprehensive details of a book loan, 
    /// including associated reader data, book details, and dynamically calculated fees.
    /// </summary>
    public class LoanDto
    {
        /// <summary>
        /// The unique identifier of the loan record.
        /// </summary>
        public int LoanId {get; set;}
        /// <summary>
        /// The unique identifier of the reader who borrowed the book.
        /// </summary>
        public int ReaderId {get; set;}

        /// <summary>
        /// The full name of the reader.
        /// </summary>
        public string ReaderName { get; set; } = string.Empty;

        /// <summary>
        /// The unique identifier of the borrowed book.
        /// </summary>
        public int BookId {get; set;}

        /// <summary>
        /// The title of the borrowed book.
        /// </summary>
        public string BookTitle { get; set; } = string.Empty;

        /// <summary>
        /// The author of the borrowed book.
        /// </summary>
        public string BookAuthor { get; set; } = string.Empty;


        /// <summary>
        /// The date when the book was borrowed. Must be a valid date.
        /// </summary>
        [Required(ErrorMessage ="Loan date is required!")]
        [ValidLoanDate]
        public DateOnly LoanDate {get; set;}

        /// <summary>
        /// The deadline date by which the book must be returned.
        /// </summary>
        [Required(ErrorMessage ="Due date is required!")]
        public DateOnly DueDate {get; set;}
        
        /// <summary>
        /// The actual date the book was returned. A null value indicates the book is still currently borrowed.
        /// </summary>
        [ValidReturnDate]
        public DateOnly? ReturnDate {get; set;}

        /// <summary>
        /// The dynamically calculated late fee based on the due date and return date.
        /// Calculation is performed by the <see cref="LateFeeCalculator"/>.
        /// </summary>
        public int LateFee => LateFeeCalculator.CalculateLateFee(DueDate, ReturnDate);
    }
}