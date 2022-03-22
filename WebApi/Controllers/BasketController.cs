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
    public class BasketController : Controller
    {
        private readonly BasketRepository repository;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        public BasketController(BasketRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<Basket> Get()
        {
            return repository.Get();
        }
        [HttpGet]
        [Route("GetById")]
        public Basket GetById(int id)
        {
            return repository.GetById(id);
        }

        [HttpPost]
        [Route("Edit")]
        public int Edit([FromBody] Basket Data)
        {
            return repository.Save(Data);
        }


        [HttpPost]
        [Route("Create")]
        public int Create([FromBody]Basket Data)
        {
           return repository.Save(Data);
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
