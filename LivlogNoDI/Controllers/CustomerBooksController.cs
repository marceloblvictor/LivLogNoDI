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
        /// Obter os livros de clientes de um determinado cliente.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("/by-customer/")]
        public ActionResult<IEnumerable<CustomerBookDTO>> GetByCustomer([FromQuery] int customerId)
            => Ok(_service.GetByCustomer(customerId));

        /// <summary>
        /// Atualizar os dados de um livro de cliente.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<BookDTO> UpdateBookRentalData(int id, CustomerBookDTO customerBookDTO)
            => Ok(_service.Update(id, customerBookDTO));

        /// <summary>
        /// Deletar um registro de livro de cliente.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id) 
            => Ok(_service.Delete(id));

        /// <summary>
        /// Alugar livros.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/rent")]
        public ActionResult<CustomerBookDTO> RentBooks([FromBody] CustomerBooksRequestDTO request)
            => Ok(_service.RentBooks(request));

        /// <summary>
        /// Devolver Livros.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/return")]
        public ActionResult<CustomerBookDTO> ReturnBooks([FromBody] IList<int> customerbookIds)
            => Ok(_service.ReturnBooks(customerbookIds));

        /// <summary>
        /// Enviar email para avisar ao cliente que ele deve fazer a devolução do livro no dia seguinte.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/send-reminder")]
        public ActionResult SendReminderToCustomer([FromBody] int customerId)
            => Ok(_service.SendReminderToCustomer(customerId));

        

        // Adicionar cliente na lista de espera por livro.

        // Remover cliente da lista de espera por livro.

        // Obter relatório sobre os alugueis de livros.

    }
}
