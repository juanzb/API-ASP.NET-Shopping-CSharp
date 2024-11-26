using Microsoft.AspNetCore.Mvc;
using Models;
using MySqlX.XDevAPI;
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
            Console.WriteLine("asdfsdf");
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

        [HttpPost]
        public ActionResult CreateClient([FromBody] Clients client)
        {
            try
            {
                _serviceClients.CreateClientService(client);
                return CreatedAtAction(nameof(CreateClient), new { client.Name } );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPut]
        public ActionResult UpdateClient([FromBody] Clients client)
        {
            try
            {
                _serviceClients.UpdateClientService(client);
                return CreatedAtAction(nameof(UpdateClient), new { client });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        public ActionResult DeleteClient([FromBody] int clientId)
        {
            try
            {
                Console.WriteLine(clientId);
                _serviceClients.DeleteClientService(clientId);
                return CreatedAtAction(nameof(DeleteClient), new { clientId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
