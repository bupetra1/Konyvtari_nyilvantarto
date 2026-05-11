using Share.Dtos;

namespace Frontend.Services
{
    public interface ILoanRepository
    {
        Task<List<LoanDto>> GetLoansAsync();
        Task<bool> CreateLoanAsync(CreateLoanDto loan);
        Task<bool> UpdateLoanAsync(int loanId, LoanDto loan);
        Task<bool> DeleteLoanAsync(int loanId);
    }
}