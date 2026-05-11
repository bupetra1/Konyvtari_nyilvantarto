using System.Net.Http.Json;
using Share.Dtos;

namespace Frontend.Services
{
    public class BookService : IBookRepository
    {
        public Task<bool> CreateBookAsync(CreateBookDto book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBookAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookAsync(int bookId, BookDto book)
        {
            throw new NotImplementedException();
        }
    }
}
