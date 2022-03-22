using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductRepository repository;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


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
        [HttpPost]
        [Route("Edit")]
        public int Edit([FromBody] Product product)
        {
            return repository.Save(product);
        }

        [HttpPost]
        [Route("Create")]
        public int Create([FromBody] Product product)
        {
           
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
