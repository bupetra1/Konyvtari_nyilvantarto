using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetBooks()
        {
            return Ok(_repository.GetBooks());
        }

        [HttpGet("readers")]
        public IActionResult GetReaders()
        {
            return Ok(_repository.GetReaders());
        }

        [HttpGet("loans")]
        public IActionResult GetLoans()
        {
            return Ok(_repository.GetLoans());
        }

        [HttpPost("CreateBook")]
        public IActionResult CreateBook(BookDto bookDto)
        {
            _repository.CreateBook(bookDto);
            return Ok(bookDto);
        }
        [HttpPost("CreateReader")]
        public IActionResult CreateReader(ReaderDto readerDto)
        {
            _repository.CreateReader(readerDto);
            return Ok(readerDto);
        }
        [HttpPost("CreateLoan")]
        public IActionResult CreateLoan(LoanDto loanDto)
        {
            _repository.CreateLoan(loanDto);
            return Ok(loanDto);
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            _repository.DeleteBook(bookId);
            return Ok();
        }
        [HttpDelete("DeleteReader")]
        public IActionResult DeleteReader(int readerId)
        {
            _repository.DeleteReader(readerId);
            return Ok();
        }
        [HttpDelete("DeleteLoan")]
        public IActionResult DeleteLoan(int loanId)
        {
            _repository.DeleteLoan(loanId);
            return Ok();
        }
        
        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(int bookId, BookDto bookDto)
        {
            _repository.UpdateBook(bookId,bookDto);
            return Ok();
        }
        [HttpPut("UpdateReader")]
        public IActionResult UpdateReader(int readerId, ReaderDto readerDto)
        {
            _repository.UpdateReader(readerId,readerDto);
            return Ok();
        }
        [HttpPut("UpdateLoan")]
        public IActionResult UpdateLoan(int loanId, LoanDto loanDto)
        {
            _repository.UpdateLoan(loanId,loanDto);
            return Ok();
        }
    }
}