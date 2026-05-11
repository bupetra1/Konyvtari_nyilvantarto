using System.ComponentModel.DataAnnotations;

namespace Share.Dtos
{

    /// <summary>
    /// Data transfer object containing the necessary information to create a new loan record.
    /// </summary>
    public class CreateLoanDto
    {
        /// <summary>
        /// The unique identifier of the reader who is borrowing the book.
        /// </summary>
        [Required(ErrorMessage = "Reader ID is required!")]
        public int ReaderId {get; set;}

        /// <summary>
        /// The unique identifier of the book being borrowed.
        /// </summary>
        [Required(ErrorMessage = "Book ID is required!")]
        public int BookId {get; set;}

        /// <summary>
        /// The deadline date by which the borrowed book must be returned.
        /// </summary>
        [Required(ErrorMessage = "Due date is required!")]
        public DateOnly DueDate {get; set;}

        /// <summary>
        /// The date the loan is initiated. This is automatically set to the current local date.
        /// </summary>
        public DateOnly LoanDate => DateOnly.FromDateTime(DateTime.Now);
    }
}