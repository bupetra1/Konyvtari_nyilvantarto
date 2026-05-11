using Share.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    public interface IReaderRepository
    {
        Task<IEnumerable<ReaderBookListDto>> GetAvailableBooksAsync();
        Task<IEnumerable<ReaderLoanListDto>>? GetLoansByReaderIdAsync(int readerId);
    }
}