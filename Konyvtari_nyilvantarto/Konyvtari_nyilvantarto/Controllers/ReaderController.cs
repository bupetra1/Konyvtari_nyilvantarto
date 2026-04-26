using Konyvtari_nyilvantarto.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Konyvtari_nyilvantarto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        IReaderRepository _repository;

        public ReaderController(IReaderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{readerId}")]
        public IActionResult GetLoansByReaderId(int readerId)
        {
            var readerLoans = _repository.GetLoansByReaderId(readerId);

            if (readerLoans is null)
            {
                return NotFound($"There is no reader with ID: {readerId}");
            }

            return Ok(readerLoans);
        }

        [HttpGet]
        public IActionResult GetAvailableBooks()
        {
            var availableBooks = _repository.GetAvailableBooks();

            return Ok(availableBooks);
        }
    }
}