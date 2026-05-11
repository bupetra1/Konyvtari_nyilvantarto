using Share.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();
        Task<IEnumerable<ReaderDto>> GetReadersAsync();
        Task<IEnumerable<LoanDto>> GetLoansAsync();
        Task CreateBookAsync(CreateBookDto bookDto);
        Task CreateReaderAsync(CreateReaderDto readerDto);
        Task CreateLoanAsync(CreateLoanDto createLoanDto);
        Task UpdateBookAsync(int bookId, BookDto bookDto);
        Task UpdateReaderAsync(int readerId, ReaderDto readerDto);
        Task UpdateLoanAsync(int loanId, LoanDto loanDto);
        Task DeleteBookAsync(int bookId);
        Task DeleteReaderAsync(int readerId);
        Task DeleteLoanAsync(int loanId);
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}