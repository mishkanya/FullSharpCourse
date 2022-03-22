using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductTypesController : Controller
    {
        private readonly ProductTypesRepository repository;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        public ProductTypesController(ProductTypesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<ProductTypes> Get()
        {
            return repository.Get();
        }
        [HttpGet]
        [Route("GetById")]
        public ProductTypes Registration(int id)
        {
            return repository.GetById(id);
        }

        [HttpPost]
        [Route("Edit")]
        public int Edit([FromBody]ProductTypes ProductData)
        {
            return repository.Save(ProductData);
        }

        [HttpPost]
        [Route("Create")]
        public int Create([FromBody] ProductTypes ProductData)
        {
            return repository.Save(ProductData);
        }

        [HttpGet]
        [Route("Delete")]
        public bool Delete(int id)
        {
            repository.DeleteById(id);
            return true;
        }
    }
}
