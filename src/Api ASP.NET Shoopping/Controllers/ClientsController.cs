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

        [HttpGet("{id}")]
        public ActionResult<Clients> GetClient(int id)
        {
            try
            {
                Clients client = _serviceClients.GetByIdClientService(id);
                return CreatedAtAction(nameof(GetClient), new { client });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("p")]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType<Product>(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Clients CreateClient([FromBody] Clients client)
        {
            Console.WriteLine("AJAJASJAJSAKUISUUV");
            Console.WriteLine(client);
            _serviceClients.CreateClientService(client);
            return client;
        }
    }
}
  