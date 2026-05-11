using System.Net.Http.Json;
using Share.Dtos;

namespace Frontend.Services
{
    public class BookService : IBookRepository
    {
        private readonly HttpClient _httpClient;
        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }   
        public async Task<bool> CreateBookAsync(CreateBookDto book)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/librarian/CreateBook", book);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating book: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/librarian/DeleteBook?bookId={bookId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
                return false;
            }
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BookDto>>("api/librarian/books") ?? new List<BookDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
                return new List<BookDto>();
            }
        }

        public async Task<bool> UpdateBookAsync(int bookId, BookDto book)
        {
            try
            { 
                var response = await _httpClient.PutAsJsonAsync($"api/librarian/UpdateBook?bookId={bookId}", book);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                return false;
            }
        }
    }
}
