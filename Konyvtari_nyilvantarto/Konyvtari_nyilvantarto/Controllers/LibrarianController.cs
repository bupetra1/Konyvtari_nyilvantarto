using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;
using Share.Dtos;

namespace Konyvtari_nyilvantarto.Controllers
{
    /// <summary>
    /// Provides endpoints for managing library resources.
    /// Provides CRUD operations for the core library entities: books, readers, and loans.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        /// <summary>
        /// Repository that provides data access operations for librarian tasks.
        /// </summary>
        ILibrarianRepository _repository;

        /// <summary>
        /// Constructor for the <see cref="LibrarianController"/> class.
        /// </summary>
        /// <param name="repository">The repository used for data access operations.</param>
        public LibrarianController(ILibrarianRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a list of all books stored in the database.  
        /// </summary>
        /// <returns>A list of <see cref="BookDto"/> objects representing the stored books.</returns>
        /// <response code="200">Returns the list of books successfully.</response>
        [HttpGet("books")]
        public async Task<ActionResult<List<BookDto>>> GetBooksAsync()
        {
            return Ok(await _repository.GetBooksAsync());
        }

        /// <summary>
        /// Retrieves a list of all readers stored in the database. 
        /// </summary>
        /// <returns>A list of <see cref="ReaderDto"/> objects representing the stored reader data.</returns>
        /// <response code="200">Returns the list of readers successfully.</response>
        [HttpGet("readers")]
        public async Task<ActionResult<List<ReaderDto>>> GetReadersAsync()
        {
            return Ok(await _repository.GetReadersAsync());
        }

        /// <summary>
        /// Retrieves a list of all loans stored in the database. 
        /// </summary>
        /// <returns>A list of <see cref="LoanDto"/> objects representing the stored loan data.</returns>
        /// <response code="200">Returns the list of loans successfully.</response>
        [HttpGet("loans")]
        public async Task<ActionResult<List<LoanDto>>> GetLoansAsync()
        {
            return Ok(await _repository.GetLoansAsync());
        }

        /// <summary>
        /// Creates a new book record in the database.
        /// </summary>
        /// <param name="createBookDto">A <see cref="CreateBookDto"/> object containing the details of the new book to be registered.</param>
        /// <returns>The newly created book object.</returns>
        /// <response code="200">The book was successfully registered.</response>
        /// <response code="400">If the provided book data is invalid.</response>
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBookAsync(CreateBookDto createBookDto)
        {
            await _repository.CreateBookAsync(createBookDto);
            return Ok(createBookDto);
        }

        /// <summary>
        /// Creates a new reader record in the database.
        /// </summary>
        /// <param name="createReaderDto">A <see cref="CreateReaderDto"/> object containing the details of the new reader to be registered.</param>
        /// <returns>The newly created reader object.</returns>
        /// <response code="200">The reader was successfully registered.</response>
        /// <response code="400">If the provided reader data is invalid.</response>
        [HttpPost("CreateReader")]
        public async Task<IActionResult> CreateReaderAsync(CreateReaderDto createReaderDto)
        {
            await _repository.CreateReaderAsync(createReaderDto);
            return Ok(createReaderDto);
        }
        /// <summary>
        /// Creates a new loan record in the database.
        /// </summary>
        /// <param name="createLoanDto">A <see cref="CreateLoanDto"/> object containing the details of the new loan to be registered.</param>
        /// <returns>The newly created loan object.</returns>   
        /// <response code="200">The loan was successfully registered.</response>
        /// <response code="400">If the provided loan data is invalid.</response>
        /// <response code="409">If the specified book is currently unavailable.</response>
        [HttpPost("CreateLoanAsync")]
        public async Task<IActionResult> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            if(!await _repository.IsBookAvailableAsync(createLoanDto.BookId))
            {
                return Conflict("The specified book is currently unavailable for loans because it has not been returned yet.");
            }
            await _repository.CreateLoanAsync(createLoanDto);
            return Ok(createLoanDto);
        }

        /// <summary>
        /// Deletes a specific book from the library catalog.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to be deleted.</param>
        /// <response code="200">The book was successfully deleted.</response>  
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            await _repository.DeleteBookAsync(bookId);
            return Ok();
        }
        /// <summary>
        /// Deletes a specific reader from the library catalog.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader to be deleted.</param>
        /// <response code="200">The reader was successfully deleted.</response> 
        [HttpDelete("DeleteReader")]
        public async Task<IActionResult> DeleteReaderAsync(int readerId)
        {
            await _repository.DeleteReaderAsync(readerId);
            return Ok();
        }

        /// <summary>
        /// Deletes a specific loan from the library catalog.
        /// </summary>
        /// <param name="loanId">The unique identifier of the loan to be deleted.</param>
        /// <response code="200">The loan was successfully deleted.</response>
        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoanAsync(int loanId)
        {
            await _repository.DeleteLoanAsync(loanId);
            return Ok();
        }

        /// <summary>
        /// Updates the details of an existing book in the database.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to update.</param>
        /// <param name="bookDto">A <see cref="BookDto"/> object containing the updated details.</param>
        /// <response code="200">The book was successfully updated.</response>
        /// <response code="400">If the provided data is invalid.</response>
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBookAsync(int bookId, BookDto bookDto)
        {
            await _repository.UpdateBookAsync(bookId,bookDto);
            return Ok();
        }

        /// <summary>
        /// Updates the details of an existing reader in the database.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader to update.</param>
        /// <param name="readerDto">A <see cref="ReaderDto"/> object containing the updated details.</param>
        /// <response code="200">The reader was successfully updated.</response>
        /// <response code="400">If the provided data is invalid.</response>
        [HttpPut("UpdateReader")]
        public async Task<IActionResult> UpdateReaderAsync(int readerId, ReaderDto readerDto)
        {
            await _repository.UpdateReaderAsync(readerId,readerDto);
            return Ok();
        }

        /// <summary>
        /// Updates the details of an existing loan in the database.
        /// </summary>
        /// <param name="loanId">The unique identifier of the loan to update.</param>
        /// <param name="loanDto">A <see cref="LoanDto"/> object containing the updated details.</param>
        /// <response code="200">The loan was successfully updated.</response>
        /// <response code="400">If the provided data is invalid.</response>
        [HttpPut("UpdateLoan/{loanId}")]
        public async Task<IActionResult> UpdateLoanAsync(int loanId, LoanDto loanDto)
        {
            await _repository.UpdateLoanAsync(loanId,loanDto);
            return Ok();
        }
    }
}