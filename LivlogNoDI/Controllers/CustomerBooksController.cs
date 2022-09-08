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

        /// <summary>
        /// Alugar livros.
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult<CustomerBookDTO> RentBooks([FromBody] RentalRequestDTO request)
            => Ok(_service.RentBooks(request));

        // Devolver Livros.

        // Adicionar cliente na lista de espera por livro.

        // Remover cliente da lista de espera por livro.

        // Obter histórico de livros de um cliente.

        // Obter relatório sobre os alugueis de livros.

        // Enviar email para avisar ao cliente que ele deve fazer a devolução do livro no dia seguinte.
    }
}
