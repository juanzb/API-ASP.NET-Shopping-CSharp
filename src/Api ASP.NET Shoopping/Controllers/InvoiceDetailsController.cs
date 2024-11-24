using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("invoicesdetails")]
    public class InvoiceDetailsController : Controller
    {
        private readonly InvoicesDetailsServices _serviceDetails;

        public InvoiceDetailsController(InvoicesDetailsServices serviceDetails) 
        { 
            this._serviceDetails = serviceDetails;
        }


        [HttpGet]
        public List<InvoicesDetails> GetAll()
        {
            try
            {
                List<InvoicesDetails> result = _serviceDetails.AllInvoiceDetailsService();
                return result;   
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
