using Konyvtari_nyilvantarto.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    public interface IReaderRepository
    {
        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
        Task<IEnumerable<LoanDto>>? GetLoansByReaderIdAsync(int readerId);
    }
}