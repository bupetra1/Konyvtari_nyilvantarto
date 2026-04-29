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
    }
}