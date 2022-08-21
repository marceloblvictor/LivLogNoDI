using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using LivlogNoDI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

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
