using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoicesServices _service;

        public InvoicesController(InvoicesServices service)
        {
            this._service = service;
        }

        // GET: api/<HomeController>
        [HttpGet]
        public ActionResult<List<Invoices>> GetAll()
        {
            try
            {
                List<Invoices> result = _service.AllInvoicesService();
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        //public ActionResult<string> GetAll() => "asasas";

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Pizza {id}";
        }

        // POST api/<HomeController>
        [HttpPost]
        public IActionResult Create(object pizza)
        {
            Console.WriteLine(CreatedAtAction(nameof(Get), new { id = 12 }, pizza));
            return CreatedAtAction(nameof(Get), new { id = 12 }, pizza);
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            Console.WriteLine(id);
            return NoContent();
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Console.WriteLine(id);
            return NoContent();
        }
    }
}
