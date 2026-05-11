using System.Net.Http.Json;
using Share.Dtos; // Fontos!

namespace Frontend.Services
{
    public class LoanService:ILoanRepository
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // A korábbi DTO osztályokat (LoanDto, CreateLoanDto) TÖRÖLD INNEN, 
        // mert már benne vannak a Share projektben!

        public async Task<List<LoanDto>> GetLoansAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LoanDto>>("api/librarian/loans") ?? new();
        }

        public async Task<bool> CreateLoanAsync(CreateLoanDto loan)
        {
            var response = await _httpClient.PostAsJsonAsync("api/librarian/CreateLoan", loan);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateLoanAsync(int loanId, LoanDto loan)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/librarian/UpdateLoan/{loanId}", loan);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLoanAsync(int loanId)
        {
            var response = await _httpClient.DeleteAsync($"api/librarian/DeleteLoan?loanId={loanId}");
            return response.IsSuccessStatusCode;
        }
    }
}