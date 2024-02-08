using Microsoft.AspNetCore.Mvc;
using SavePalestineApi.Models;
using SavePalestineApi.Repositories;

namespace SavePalestineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productRepository.GetProducts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }


        [HttpPost]
        public ActionResult AddProduct([FromForm] Product product, IFormFile formFile)
        {
            _productRepository.AddProduct(product, formFile);
            return Ok(product);
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();

            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _productRepository.UpdateProduct(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteProduct(int id)
        {

            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(product);
            return Ok(product);
        }
   
}


}
