using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBooksController : ControllerBase
    {
        private readonly CustomerBookService _service;

        public CustomerBooksController()
        {
            _service = new CustomerBookService();
        }

        /// <summary>
        /// Obter todos os livros de clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CustomerBookDTO>> GetAll()
            => Ok(_service.GetAll());

        /// <summary>
        /// Obter um livro de cliente por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CustomerBookDTO> Get(int id)
            => Ok(_service.Get(id));
    }
}
