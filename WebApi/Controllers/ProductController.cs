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
    public class ProductController : Controller
    {
        private readonly ProductRepository repository;

        public ProductController(ProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<Product> Get()
        {
            return repository.Get();
        }
        [HttpGet]
        [Route("GetById")]
        public Product GetById(int id)
        {
            return repository.GetById(id);
        }
        [HttpGet]
        [Route("Edit")]
        public int Edit(string ProductData)
        {
            var product = JsonSerializer.Deserialize<Product>(ProductData);
            return repository.Save(product);
        }

        [HttpGet]
        [Route("Create")]
        public int Create(string ProductData)
        {
            var product = JsonSerializer.Deserialize<Product>(ProductData);
            return repository.Save(product);
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
