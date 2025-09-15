using Microsoft.AspNetCore.Mvc;
using Secure_Product_Manager.Entity;
using Secure_Product_Manager.Services;

namespace Secure_Product_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService booksService) =>
            _productService = booksService;

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var book = await _productService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  Product newBook)
        {
            if (string.IsNullOrEmpty(newBook.id))
            {
                newBook.id= Guid.NewGuid().ToString();
            }
            await _productService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updatedBook)
        {
            var book = await _productService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.id = book.id;

            await _productService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _productService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }
    }
}
