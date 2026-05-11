using Share.Dtos;
namespace Frontend.Services
{
    public interface IBookRepository
    {
        Task<List<BookDto>> GetBooksAsync();
        Task<bool> CreateBookAsync(CreateBookDto book);
        Task<bool> UpdateBookAsync(int bookId, BookDto book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
