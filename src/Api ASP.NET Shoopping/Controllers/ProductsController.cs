using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly ProductsServices _serviceProducts;
        public ProductsController(ProductsServices serviceProducts)
        {
            this._serviceProducts = serviceProducts;
        }

        [HttpGet]
        public ActionResult<List<Products>> GetAll()
        {
            try
            {
                var products = _serviceProducts.AllProductsService();
                return CreatedAtAction(nameof(GetAll), new { products });
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
