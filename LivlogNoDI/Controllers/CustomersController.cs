using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomersController()
        {
            _service = new CustomerService();
        }

        /// <summary>
        /// Obter todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
            => Ok(_service.GetAll());

        /// <summary>
        /// Obter um cliente por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> Get(int id)
            => Ok(_service.Get(id));

        /// <summary>
        /// Criar um cliente.
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<CustomerDTO> Create(CustomerDTO customerDTO)
            => Ok(_service.Create(customerDTO));

        /// <summary>
        /// Atualizar um cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<CustomerDTO> Update(int id, CustomerDTO customerDTO)
            => Ok(_service.Update(id, customerDTO));

        /// <summary>
        /// Deletar um cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id) 
            => Ok(_service.Delete(id));
    }
}
