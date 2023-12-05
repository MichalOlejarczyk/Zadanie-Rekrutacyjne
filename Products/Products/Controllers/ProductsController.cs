using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Products.Models;
using Products.Models.Entities;
using Products.Repository;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IRepository<ProductEntity> _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IRepository<ProductEntity> productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetAll().Select(pEnt => new Product 
            {
                Id= pEnt.Id, 
                Name=pEnt.Name, 
                Cost=pEnt.Cost, 
                Description=pEnt.Description, 
                InStock=pEnt.InStock
            });
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Product value)
        {
            if (value == null) return BadRequest("Invalid Product");
            var result = _productRepository.Add(new ProductEntity
            {
                Id = value.Id,
                Name = value.Name,
                Cost = value.Cost,
                Description = value.Description,
                InStock = value.InStock
            });
            return result ? Created() : Conflict();
        }
       
        [HttpGet("{id}")]      
        public ActionResult<Product> Get(Guid id)
        {   
            var pEnt = _productRepository.GetById(id);
            if (pEnt == null) return NotFound();
            return new Product
            {
                Id = pEnt.Id,
                Name = pEnt.Name,
                Cost = pEnt.Cost,
                Description = pEnt.Description,
                InStock = pEnt.InStock
            };
        }
    }
}
