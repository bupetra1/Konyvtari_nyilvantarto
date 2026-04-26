namespace Konyvtari_nyilvantarto.Repositories
{
    public interface IReaderRepository
    {
        IEnumerable<BookDto> GetAvailableBooks();
        IEnumerable<LoanDto> GetLoansByReaderId(int readerId);
    }
}