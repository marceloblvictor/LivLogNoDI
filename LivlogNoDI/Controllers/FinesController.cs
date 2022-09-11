using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinesController : ControllerBase
    {
        private readonly FineService _service;

        public FinesController()
        {
            _service = new FineService();
        }

        /// <summary>
        /// Obter todas as multas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<FineDTO>> GetAll()
            => Ok(_service.GetAll());

        /// <summary>
        /// Obter o valor total devido por um cliente.
        /// </summary>
        /// <returns></returns>
        [HttpGet("debts/{customerId}")]
        public ActionResult<decimal> GetCustomerDebts(int customerId)
            => Ok(_service.GetCustomerDebts(customerId));

        /// <summary>
        /// Registrar pagamento de multa por cliente.
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{fineId}")]
        public ActionResult<BookDTO> RegisterPayment(int fineId, decimal amountPaid)
            => Ok(_service.UpdateFineToPaid(fineId, amountPaid));

        /// <summary>
        /// Deletar uma multa.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
            => Ok(_service.Delete(id));
    }
}
