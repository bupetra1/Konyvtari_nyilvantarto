namespace Konyvtari_nyilvantarto.Repositories
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<ReaderDto>> GetReadersAsync();
        Task<IEnumerable<LoanDto>> GetLoansAsync();

        Task CreateBookAsync(BookDto bookDto);
        Task CreateReaderAsync(ReaderDto readerDto);
        Task CreateLoanAsync(LoanDto loanDto);
        Task UpdateBookAsync(int bookId, BookDto bookDto);
        Task UpdateReaderAsync(int readerId, ReaderDto readerDto);
        Task UpdateLoanAsync(int loanId, LoanDto loanDto);
        Task DeleteBookAsync(int bookId);
        Task DeleteReaderAsync(int readerId);
        Task DeleteLoanAsync(int loanId);
    }
}