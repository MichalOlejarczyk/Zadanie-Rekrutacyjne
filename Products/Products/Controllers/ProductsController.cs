using Microsoft.AspNetCore.Mvc;
using Products.Models;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = Guid.NewGuid(),
                Name = index.ToString(),
                Cost = index,
                InStock = index,

            })
            .ToArray();
        }
    }
}
