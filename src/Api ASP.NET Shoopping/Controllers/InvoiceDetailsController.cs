using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("invoicesdetails")]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly InvoicesDetailsServices _serviceDetails;

        public InvoiceDetailsController(InvoicesDetailsServices serviceDetails) 
        { 
            this._serviceDetails = serviceDetails;
        }


        [HttpGet]
        public ActionResult<List<InvoicesDetails>> GetAll()
        {
            try
            {
                var res = _serviceDetails.AllInvoiceDetailsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<InvoicesDetails> Get()
        {
            try
            {
                var res = _serviceDetails.AllInvoiceDetailsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Create(InvoicesDetails invoiceDetail)
        {
            try
            {
                var res = _serviceDetails.AllInvoiceDetailsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
