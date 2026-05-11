using Share.Dtos;

namespace Frontend.Services
{
    public interface IReaderRepository
    {
        Task<List<ReaderDto>> GetReadersAsync();
        Task<bool> CreateReaderAsync(CreateReaderDto reader);
        Task<bool> UpdateReaderAsync(int readerId, ReaderDto reader);
        Task<bool> DeleteReaderAsync(int readerId);
    }
}