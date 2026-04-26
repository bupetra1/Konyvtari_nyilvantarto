namespace Konyvtari_nyilvantarto.Repositories
{
    public interface ILibrarianRepository
    {
        IEnumerable<BookDto> GetBooks();
        IEnumerable<ReaderDto> GetReaders();
        IEnumerable<LoanDto> GetLoans();

        void CreateBook(BookDto bookDto);
        void CreateReader(ReaderDto readerDto);
        void CreateLoan(LoanDto loanDto);

        void UpdateBook();
        void UpdateReader();
        void UpdateLoan();

        void DeleteBook();
        void DeleteReader();
        void DeleteLoan();
    }
}