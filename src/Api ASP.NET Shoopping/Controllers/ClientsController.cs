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
                var res = _serviceClients.AllClientsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Clients> Get(int id)
        {
            Console.WriteLine("asdfsdf");
            try
            {
                var res = _serviceClients.GetByIdClientService(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Clients client)
        {
            try
            {
                _serviceClients.CreateClientService(client);
                return CreatedAtAction(nameof(Create), $"El cliente '{client.Name}' se agrego correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Clients client)
        {
            try
            {
                _serviceClients.UpdateClientService(client);
                return Ok($"El cliente con ID: {client.Id} se actualizo correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                _serviceClients.DeleteClientService(id);
                return Ok($"El cliente con ID: {id} se elimino correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

    }
}
