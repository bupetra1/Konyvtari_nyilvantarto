using Share.Dtos;
using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Konyvtari_nyilvantarto.Controllers
{
    /// <summary>
    /// Provides endpoints for querying available books and user loans.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {   
        /// <summary>
        /// Repository that provides data access operations for readers.
        /// </summary>
        IReaderRepository _repository;

        /// <summary>
        /// Constructor for the <see cref="ReaderController"/> class.
        /// </summary>
        /// <param name="repository">The repository used for data access operations.</param>
        public ReaderController(IReaderRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Lists loans for the specified reader.
        /// </summary>
        /// <param name="readerId">The unique identifier of the reader whose loans are to be queried.</param>
        /// <returns>A list of <see cref="ReaderLoanListDto"/> objects representing the reader's loan data.</returns>
        /// <response code="200">Returns the list of loans for the specified reader successfully.</response>
        /// <response code="404">If no reader is found with the specified ID.</response>
        [HttpGet("loans/{readerId}")]
        public async Task<ActionResult<List<ReaderLoanListDto>>> GetLoansByReaderIdAsync(int readerId)
        {
            var readerLoans = await _repository.GetLoansByReaderIdAsync(readerId);

            if (readerLoans is null)
            {
                return NotFound($"There is no reader with ID: {readerId}");
            }

            return Ok(readerLoans);
        }

        /// <summary>
        /// Lists the available books that the reader can borrow.
        /// </summary>
        /// <returns>A list of <see cref="ReaderBookListDto"/> objects representing the available books.</returns>
        /// <response code="200">Returns the list of books available for borrowing.</response>
        [HttpGet("books")]
        public async Task<ActionResult<List<ReaderBookListDto>>> GetAvailableBooksAsync()
        {
            var availableBooks = await _repository.GetAvailableBooksAsync();

            return Ok(availableBooks);
        }
    }
}