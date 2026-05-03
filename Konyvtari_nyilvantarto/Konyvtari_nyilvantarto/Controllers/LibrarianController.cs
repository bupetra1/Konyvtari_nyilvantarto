using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;
using Konyvtari_nyilvantarto.Dtos;

namespace Konyvtari_nyilvantarto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        ILibrarianRepository _repository;

        public LibrarianController(ILibrarianRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok(await _repository.GetBooksAsync());
        }

        [HttpGet("readers")]
        public async Task<IActionResult> GetReadersAsync()
        {
            return Ok(await _repository.GetReadersAsync());
        }

        [HttpGet("loans")]
        public async Task<IActionResult> GetLoansAsync()
        {
            return Ok(await _repository.GetLoansAsync());
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBookAsync(CreateBookDto createBookDto)
        {
            await _repository.CreateBookAsync(createBookDto);
            return Ok(createBookDto);
        }
        [HttpPost("CreateReader")]
        public async Task<IActionResult> CreateReaderAsync(CreateReaderDto createReaderDto)
        {
            await _repository.CreateReaderAsync(createReaderDto);
            return Ok(createReaderDto);
        }
        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            if(!await _repository.IsBookAvailableAsync(createLoanDto.BookId))
            {
                return Conflict("The specified book is currently unavailable for loans because it has not been returned yet.");
            }
            await _repository.CreateLoanAsync(createLoanDto);
            return Ok(createLoanDto);
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            await _repository.DeleteBookAsync(bookId);
            return Ok();
        }
        [HttpDelete("DeleteReader")]
        public async Task<IActionResult> DeleteReaderAsync(int readerId)
        {
            await _repository.DeleteReaderAsync(readerId);
            return Ok();
        }
        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoanAsync(int loanId)
        {
            await _repository.DeleteLoanAsync(loanId);
            return Ok();
        }
        /// <summary>
        /// Itt tudod frissíteni egy már meglévő könyv adatait az azonosítója (ID) alapján.
        /// </summary>
        /// <param name="bookId">A könyv egyedi azonosítója.</param>
        /// <param name="bookDto">A könyv új adatai (Cím, Szerző, stb.).</param>
        /// <response code="200">Sikeres frissítés.</response>
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBookAsync(int bookId, BookDto bookDto)
        {
            await _repository.UpdateBookAsync(bookId,bookDto);
            return Ok();
        }
        [HttpPut("UpdateReader")]
        public async Task<IActionResult> UpdateReaderAsync(int readerId, ReaderDto readerDto)
        {
            await _repository.UpdateReaderAsync(readerId,readerDto);
            return Ok();
        }
        [HttpPut("UpdateLoan")]
        public async Task<IActionResult> UpdateLoanAsync(int loanId, LoanDto loanDto)
        {
            await _repository.UpdateLoanAsync(loanId,loanDto);
            return Ok();
        }
    }
}