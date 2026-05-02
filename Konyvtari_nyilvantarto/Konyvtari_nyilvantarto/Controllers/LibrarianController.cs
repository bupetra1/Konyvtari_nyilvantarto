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
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _repository.GetBooksAsync());
        }

        [HttpGet("readers")]
        public async Task<IActionResult> GetReaders()
        {
            return Ok(await _repository.GetReadersAsync());
        }

        [HttpGet("loans")]
        public async Task<IActionResult> GetLoans()
        {
            return Ok(await _repository.GetLoansAsync());
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(BookDto bookDto)
        {
            await _repository.CreateBookAsync(bookDto);
            return Ok(bookDto);
        }
        [HttpPost("CreateReader")]
        public async Task<IActionResult> CreateReader(ReaderDto readerDto)
        {
            await _repository.CreateReaderAsync(readerDto);
            return Ok(readerDto);
        }
        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan(CreateLoanDto createLoanDto)
        {
            await _repository.CreateLoanAsync(createLoanDto);
            return Ok(createLoanDto);
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            await _repository.DeleteBookAsync(bookId);
            return Ok();
        }
        [HttpDelete("DeleteReader")]
        public async Task<IActionResult> DeleteReader(int readerId)
        {
            await _repository.DeleteReaderAsync(readerId);
            return Ok();
        }
        [HttpDelete("DeleteLoan")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            await _repository.DeleteLoanAsync(loanId);
            return Ok();
        }
        
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(int bookId, BookDto bookDto)
        {
            await _repository.UpdateBookAsync(bookId,bookDto);
            return Ok();
        }
        [HttpPut("UpdateReader")]
        public async Task<IActionResult> UpdateReader(int readerId, ReaderDto readerDto)
        {
            await _repository.UpdateReaderAsync(readerId,readerDto);
            return Ok();
        }
        [HttpPut("UpdateLoan")]
        public async Task<IActionResult> UpdateLoan(int loanId, LoanDto loanDto)
        {
            await _repository.UpdateLoanAsync(loanId,loanDto);
            return Ok();
        }
    }
}