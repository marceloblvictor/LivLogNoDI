using LivlogNoDI.Models.DTO;
using LivlogNoDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivlogNoDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // Obter multas de um cliente.

        // Registrar pagamento de multa por cliente.


    }
}
