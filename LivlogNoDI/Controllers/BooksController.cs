using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service;

        public BooksController()
        {
            _service = new BookService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAll()
            => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetBook(int id)
            => Ok(_service.Get(id));
    }
}
