using Konyvtari_nyilvantarto.Dtos;
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

        [HttpGet("GetLoansBy{readerId}")]
        public async Task<ActionResult<List<ReaderLoanListDto>>> GetLoansByReaderIdAsync(int readerId)
        {
            var readerLoans = await _repository.GetLoansByReaderIdAsync(readerId);

            if (readerLoans is null)
            {
                return NotFound($"There is no reader with ID: {readerId}");
            }

            return Ok(readerLoans);
        }

        [HttpGet("GetAvailableBooks")]
        public async Task<ActionResult<List<ReaderBookListDto>>> GetAvailableBooksAsync()
        {
            var availableBooks = await _repository.GetAvailableBooksAsync();

            return Ok(availableBooks);
        }
    }
}