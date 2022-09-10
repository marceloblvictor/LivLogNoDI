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

        /// <summary>
        /// Obter todos os livros.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAll()
            => Ok(_service.GetAll());

        /// <summary>
        /// Obter um livro pelo Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BookDTO> Get(int id)
            => Ok(_service.Get(id));

        /// <summary>
        /// Criar um livro.
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BookDTO> Create(BookDTO bookDTO)
            => Ok(_service.Create(bookDTO));

        /// <summary>
        /// Atualizar um livro.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<BookDTO> Update(int id, BookDTO bookDTO)
            => Ok(_service.Update(id, bookDTO));

        /// <summary>
        /// Deletar um livro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id) 
            => Ok(_service.Delete(id));
    }
}
