using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoicesServices _serviceInvoice;

        public InvoicesController(InvoicesServices serviceInvoice)
        {
            this._serviceInvoice = serviceInvoice;
        }


        [HttpGet]
        public ActionResult<List<Invoices>> GetAll()
        {
            try
            {
                var res = _serviceInvoice.AllInvoicesService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public ActionResult<Invoices> Get(int id)
        {
            try
            {
                var res = _serviceInvoice.GetInvoiceService(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }


        public class Details
        {
            public int ProductID { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public class DataInvoice
        {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public List<Details> Detail { get; set; }
        }


        [HttpPost]
        public IActionResult Create([FromBody] DataInvoice invoiceStart)
        {
            try
            {
                var invoiceResult = new Invoices
                {
                    ClientID = invoiceStart.ClientId,
                };

                invoiceStart.Detail.ForEach(item =>
                    invoiceResult.Detail.Add(new InvoicesDetails
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        Price = item.Price
                    })
                );

                _serviceInvoice.CreateInvoiceService(invoiceResult);
                return CreatedAtAction(nameof(Create), "La compra se registro correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] DataInvoice invoiceNew)
        {
            try
            {
                var invoiceUpdated = new Invoices
                {
                    Id = invoiceNew.Id,
                    ClientID = invoiceNew.ClientId
                };

                invoiceNew.Detail.ForEach( item => {
                invoiceUpdated.Detail.Add(new InvoicesDetails
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                });

                _serviceInvoice.UpdateInvoiceService(invoiceUpdated);
                return Ok($"La compra con ID: {invoiceNew.Id} se actualizo correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _serviceInvoice.DeleteInvoiceService(id);
                return Ok($"La compra con ID: {id} se elimino correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
