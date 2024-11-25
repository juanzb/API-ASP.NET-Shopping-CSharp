using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientsServices _serviceClients;
        public ClientsController(ClientsServices serviceClients) 
        { 
            this._serviceClients = serviceClients;
        }

        [HttpGet]
        public ActionResult<List<Clients>> GetAll()
        {
            try
            {
                List<Clients> clients = _serviceClients.AllClientsService();
                return CreatedAtAction(nameof(GetAll), new { clients });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
