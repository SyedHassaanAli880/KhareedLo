using KhareedLo.Models;
using Microsoft.AspNetCore.Mvc;

namespace KhareedLo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIIController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly AppDbContext _appdbcontext;

        public ProductAPIIController(IProductRepository data)
        {
            _productRepository = data;
        }

        
        [HttpPost("add-product")]
        public IActionResult Add(Product obj)
        {
            var _prod = new Product()
            {
                Name = obj.Name,
                Price = obj.Price,
                //Category = obj.Category,
                ImagePhoto = obj.ImagePhoto,
                IsInStock  = obj.IsInStock,
                LongDescription  = obj.LongDescription,
                Quantity = obj.Quantity,
                ShortDescription  = obj.ShortDescription
            };

            _productRepository.AddProduct(_prod);
            return Ok();
        }

        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {

            return Ok(_productRepository.GetAllProducts());
        }

        [HttpGet("get-product-by-id/{id}")]
        public IActionResult GetProductById(int id)
        {
            var prod = _productRepository.GetProductById(id);

            return Ok(prod);
        }

        [HttpPut("update-product-by-id/{id}")]
        public IActionResult UpdateProductById(int id, Product prod)
        {
            var updateProd = _productRepository.UpdateProduct(id, prod);

            return Ok(updateProd);
        }

        [HttpDelete]
        public IActionResult DeleteProductById(int id)
        {
            _productRepository.DeleteProduct(id);
            return Ok();
        }
    }
}
