using System.Net.Http.Json;
using Share.Dtos;

namespace Frontend.Services
{
    public class ReaderService : IReaderRepository
    {
        private readonly HttpClient _httpClient;

        public ReaderService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<ReaderDto>> GetReadersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReaderDto>>("api/librarian/readers") ?? new();
        }

        public async Task<bool> CreateReaderAsync(CreateReaderDto reader)
        {
            var response = await _httpClient.PostAsJsonAsync("api/librarian/CreateReader", reader);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateReaderAsync(int readerId, ReaderDto reader)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/librarian/UpdateReader?readerId={readerId}", reader);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteReaderAsync(int readerId)
        {
            var response = await _httpClient.DeleteAsync($"api/librarian/DeleteReader?readerId={readerId}");
            return response.IsSuccessStatusCode;
        }
    }
}