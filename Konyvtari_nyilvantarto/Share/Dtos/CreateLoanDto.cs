using System.ComponentModel.DataAnnotations;

namespace Share.Dtos
{
    public class CreateLoanDto
    {
        [Required(ErrorMessage = "Reader ID is required!")]
        public int ReaderId {get; set;}
        [Required(ErrorMessage = "Book ID is required!")]
        public int BookId {get; set;}
        [Required(ErrorMessage = "Due date is required!")]
        public DateOnly DueDate {get; set;}

        public DateOnly LoanDate
        {
            get
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }
    }
}