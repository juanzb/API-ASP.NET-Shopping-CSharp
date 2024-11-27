using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Api_ASP.NET_Shoopping.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
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
                var res = _serviceProducts.AllProductsService();
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Products> Get(int id)
        {
            try
            {
                var res = _serviceProducts.GetProductcsByIdService(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Products product)
        {
            try
            {
                _serviceProducts.CreateProductcsService(product);
                return CreatedAtAction(nameof(Create),$"El producto '{product.Name}' se agrego correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Products product)
        {
            try
            {
                _serviceProducts.UpdateProductcsService(product);
                return Ok($"El producto con ID: {product.Id} se actualizo correctamente");
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
                _serviceProducts.DeleteProductcsService(id);
                return Ok($"El producto con ID: {id} se elimino correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }

    }
}
