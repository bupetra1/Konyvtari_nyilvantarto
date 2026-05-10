using Konyvtari_nyilvantarto.Dtos;

namespace Konyvtari_nyilvantarto.Repositories
{
    /// <summary>
    /// Defines the contract for managing reader data in the underlying data store.
    /// </summary>
    public interface ILibrarianRepository
    {
        /// <summary>
        /// Lists all books stored in the database.
        /// </summary>
        /// <returns>A collection of <see cref="BookDto"/> objects representing all stored books.</returns>
        Task<IEnumerable<BookDto>> GetBooksAsync();
        /// <summary>
        /// Lists all reader data stored in the database.
        /// </summary>
        /// <returns>A collection of <see cref="ReaderDto"/> objects representing all stored reader data.</returns>
        Task<IEnumerable<ReaderDto>> GetReadersAsync();
        /// <summary>
        /// Lists all loans stored in the database.
        /// </summary>
        /// <returns>A collection of <see cref="LoanDto"/> objects representing all stored loans.</returns>
        Task<IEnumerable<LoanDto>> GetLoansAsync();

        /// <summary>
        /// Creates a new book record in the database.
        /// </summary>
        /// <param name="createBookDto">A <see cref="CreateBookDto"/> object containing the details of the new book to be registered.</param>
        Task CreateBookAsync(CreateBookDto createBookDto);

        /// <summary>
        /// Creates a new reader record in the database.
        /// </summary>
        /// <param name="createReaderDto">A <see cref="CreateReaderDto"/> object containing the details of the new reader to be registered.</param>
        Task CreateReaderAsync(CreateReaderDto createReaderDto);

        /// <summary>
        /// Creates a new loan record in the database.
        /// </summary>
        /// <param name="createLoanDto">A <see cref="CreateLoanDto"/> object containing the details of the new loan to be registered.</param>
        Task CreateLoanAsync(CreateLoanDto createLoanDto);

        /// <summary>
        /// Updates the details of an existing book in the database.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to update.</param>
        /// <param name="bookDto">A <see cref="BookDto"/> object containing the updated details.</param>
        Task UpdateBookAsync(int bookId, BookDto bookDto);

        /// <summary>
        /// Updates the details of an existing reader in the database.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader to update.</param>
        /// <param name="readerDto">A <see cref="ReaderDto"/> object containing the updated details.</param>
        Task UpdateReaderAsync(int readerId, ReaderDto readerDto);

        /// <summary>
        /// Updates the details of an existing loan in the database.
        /// </summary>
        /// <param name="loanId">The unique identifier of the loan to update.</param>
        /// <param name="loanDto">A <see cref="LoanDto"/> object containing the updated details.</param>
        Task UpdateLoanAsync(int loanId, LoanDto loanDto);

        /// <summary>
        /// Deletes a specific book from the library catalog.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to be deleted.</param>
        Task DeleteBookAsync(int bookId);

        /// <summary>
        /// Deletes a specific reader from the library catalog.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader to be deleted.</param>
        Task DeleteReaderAsync(int readerId);

        /// <summary>
        /// Deletes a specific loan from the library catalog.
        /// </summary>
        /// <param name="loanId">The unique identifier of the loan to be deleted.</param>
        Task DeleteLoanAsync(int loanId);

        /// <summary>
        /// Checks whether a book is available to borrow. 
        /// </summary>
        /// <param name="bookId">The unique identifier of a book.</param>
        /// <returns><see langword="true"/> if the specified book is available, <see langword="false"/> otherwise.</returns>
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}