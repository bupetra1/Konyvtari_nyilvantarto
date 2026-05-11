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
            // A Backend: [HttpGet("GetAvailableBooks")]
            return await _httpClient.GetFromJsonAsync<List<ReaderBookListDto>>("api/Reader/GetAvailableBooks") ?? new();
        }

        public async Task<List<ReaderLoanListDto>> GetMyLoansAsync(int readerId)
        {
            // A Backend útvonalad: [HttpGet("GetLoansBy{readerId}")]
            // Ezért az URL: api/Reader/GetLoansBy5 (például)
            return await _httpClient.GetFromJsonAsync<List<ReaderLoanListDto>>($"api/Reader/GetLoansBy{readerId}") ?? new();
        }
    }
}