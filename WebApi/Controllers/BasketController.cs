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
    public class BasketController : Controller
    {
        private readonly BasketRepository repository;

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
        [HttpGet]
        [Route("Edit")]
        public int Edit(string ProductData)
        {
            var basket = JsonSerializer.Deserialize<Basket>(ProductData);
            return repository.Save(basket);
        }

        [HttpGet]
        [Route("Create")]
        public int Create(string ProductData)
        {
            var basket = JsonSerializer.Deserialize<Basket>(ProductData);
            return repository.Save(basket);
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
