namespace Konyvtari_nyilvantarto.Dtos
{
    public class CreateLoanDto
    {
        public int ReaderId {get; set;}
        public int BookId {get; set;}
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