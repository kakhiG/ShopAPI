using Microsoft.AspNetCore.Mvc;
using ShopApi.ApiModels;
using ShopApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _products;

        public ProductsController(IProductRepository productRepository)
        {
            _products = productRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProductModel>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductModel> products = await _products.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            ProductModel product = await _products.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            ProductModel createdProduct = await _products.CreateProduct(product);

            return CreatedAtAction(
                nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductModel product)
        {
            try
            {
                await _products.UpdateProduct(id, product);
                return NoContent();
            }
            catch (EntityNotFoundException<ProductModel>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _products.DeleteProduct(id);
            return NoContent();
        }
    }
}
