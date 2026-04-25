namespace Konyvtari_nyilvantarto.Repositories
{
    public interface IReaderRepository
    {
        IEnumerable<BookDto> GetAvailableBooks();
        IEnumerable<LoanDto> GetLoans(int studentId);
    }
}