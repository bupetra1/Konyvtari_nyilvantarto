using System.Net.Http.Json;
using Share.Dtos;

namespace Frontend.Services
{
    public class UserReaderService
    {
        private readonly HttpClient _httpClient;

        public UserReaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReaderBookListDto>> GetAvailableBooksAsync()
        {
            // Kontroller: [HttpGet("GetAvailableBooks")]
            return await _httpClient.GetFromJsonAsync<List<ReaderBookListDto>>("api/Reader/GetAvailableBooks") ?? new();
        }

        public async Task<List<ReaderLoanListDto>> GetMyLoansAsync(int readerId)
        {
            return await _httpClient.GetFromJsonAsync<List<ReaderLoanListDto>>($"api/Reader/GetLoansBy/{readerId}") ?? new();
        }
    }
}