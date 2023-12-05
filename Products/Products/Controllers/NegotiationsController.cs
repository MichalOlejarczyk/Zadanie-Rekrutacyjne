using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Models.Entities;
using Products.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Products.Controllers
{
    [ApiController]
    public class NegotiationsController : ControllerBase

    {
        private readonly ILogger<NegotiationsController> _logger;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<NegotiationEntity> _negotiationRepository;

        public NegotiationsController(ILogger<NegotiationsController> logger, IRepository<ProductEntity> productRepository, IRepository<NegotiationEntity> negotiationRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _negotiationRepository = negotiationRepository;
        }

        // GET: api/<NegotiationsController>
        [Route("products/{id}/negotiations")]
        [HttpGet]
        public ActionResult<IEnumerable<Negotiation>> GetAll(Guid id)
        {
            if (_productRepository.GetById(id) == null) return NotFound();
            return Ok( _negotiationRepository.GetAll()
                .Where(negotiation=> negotiation.ProductId.Equals(id))
                .Select(nEnt => new Negotiation
            {
                Id = nEnt.Id,
                ProductId = nEnt.ProductId,
                NewCost = nEnt.NewCost,
                CustomerId = nEnt.CustomerId,
                Status = nEnt.Status
            }));
        }

        // GET api/<NegotiationsController>/5
        [Route("products/{id}/negotiations/{nId}")]
        [HttpGet]
        public ActionResult<Negotiation> Get(Guid id, Guid nId)
        {
            var nEnt = _negotiationRepository.GetById(nId);
            return nEnt == null || nEnt.ProductId != id ? NotFound(): new Negotiation
            {
                Id = nEnt.Id,
                ProductId = nEnt.ProductId,
                NewCost = nEnt.NewCost,
                CustomerId = nEnt.CustomerId,
                Status = nEnt.Status
            };     
        }

        // POST api/<NegotiationsController>
        [Route("products/{id}/negotiations")]
        [HttpPost]
        public ActionResult Post(Guid id, [FromBody] Negotiation negotiation)
        {
            if (negotiation == null) return BadRequest("Invalid Negotiation");
            var product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            var result = _negotiationRepository.Add(new NegotiationEntity
            {
                Id = negotiation.Id,
                ProductId = negotiation.ProductId,
                NewCost = negotiation.NewCost,
                CustomerId = negotiation.CustomerId,
                Status = negotiation.NewCost<= product.Cost / 2 ? "Rejected" : "Pending"
            });
            return result ? Created() : Conflict();
        }

        // PUT api/<NegotiationsController>/5
        [Route("products/{id}/negotiations/{nId}")]
        [HttpPut]
        public ActionResult Put(Guid id, Guid nId, [FromBody] Negotiation negotiation)
        {
            if (negotiation == null) return BadRequest("Invalid Negotiation");
            if (_productRepository.GetById(id) == null) return NotFound();
            var nEnt = _negotiationRepository.GetById(nId);
            if (nEnt == null || nEnt.ProductId != id) return NotFound();
            var result = _negotiationRepository.Update(new NegotiationEntity
            {
                Id = negotiation.Id,
                ProductId = negotiation.ProductId,
                NewCost = negotiation.NewCost,
                CustomerId = negotiation.CustomerId,
                Status = negotiation.Status
            });
            return result ? Created() : Conflict();
        }
    }
}
