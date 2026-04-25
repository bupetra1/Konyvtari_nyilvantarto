using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Konyvtari_nyilvantarto.Controllers
{
    public class ReaderController : ControllerBase
    {
        IReaderRepository _repository;

        public ReaderController(IReaderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetLoansByReaderId(int readerId)
        {
            var readerLoans = _repository.GetLoansByReaderId(readerId);

            if (readerLoans is null)
            {
                return BadRequest("Reader has no loans!");
            }

            return Ok(readerLoans);
        }

        [HttpGet]
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _repository.GetAvailableBooks();

            if(availableBooks is null)
            {
                return BadRequest("There are no books available!");
            }

            return Ok(availableBooks);
        }
    }
}