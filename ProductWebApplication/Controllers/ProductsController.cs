using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductWebApplication.Models;
using ProductManagement.Models.Repositories;
using ProductManagement.Models;

namespace ProductWebApplication.Controllers
{
    //This is an API controller that handles all traffic to the given IProductRepository
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var ar = new ActionResult<IEnumerable<Product>>(_productRepository.Get());
            return ar;
        }

        // GET api/products/bd313d3b-d83f-47da-808e-262e49a38628
        [HttpGet("{id}")]
        public ActionResult<Product> Get(Guid id)
        {
            return _productRepository.Get(id);
        }

        // POST api/products
        [HttpPost]
        public ActionResult<Guid> GuidPost([FromBody] ProductCreateInputModel model)
        {
            var newProduct = _productRepository.Add(model);
            if (newProduct != null)
            {
                return Created(Url != null ? Url.ToString() : "", newProduct.Id);
            }
            return NoContent();
        }

        // PUT api/product
        [HttpPut]
        public ActionResult<Product> Put([FromBody] ProductUpdateInputModel model)
        {
            var product = _productRepository.Update(model);

            if (product == null)
                return NoContent();

            return product;
        }

        // DELETE api/products/bd313d3b-d83f-47da-808e-262e49a38628
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(Guid id)
        {
            bool deleted = _productRepository.Delete(id);

            if (deleted)
                return Ok("Entry deleted");

            return NoContent();
        }
    }
}