namespace Konyvtari_nyilvantarto.Repositories
{
    public interface ILibrarianRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<ReaderDto>> GetReadersAsync();
        Task<IEnumerable<LoanDto>> GetLoansAsync();

        void CreateBook(BookDto bookDto);
        void CreateReader(ReaderDto readerDto);
        void CreateLoan(LoanDto loanDto);

        void UpdateBook(int bookId, BookDto bookDto);
        void UpdateReader(int readerId, ReaderDto readerDto);
        void UpdateLoan(int loanId, LoanDto loanDto);

        void DeleteBook(int bookId);
        void DeleteReader(int readerId);
        void DeleteLoan(int loanId);
    }
}