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
    public class UsersController : Controller
    {
        private readonly UsersRepository repository;
        // Настройки для парсинга json
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        public UsersController(UsersRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        [Route("Get")]
        public IEnumerable<Users> Get()
        {
            return repository.Get();
        }

        [HttpGet]
        [Route("Registration")]
        public string Registration(string login, string password)
        {
            if(repository.Get().FirstOrDefault(t => t.Login == login) != null)
                return "Error";
            int? i = repository.Save(new Users { Login = login, Password = password });
            if (i != default)
                return "Ok";
            else
                return "Error";
        }
        [HttpGet]
        [Route("GetById")]
        public Users Registration(int id)
        {
            return repository.GetById(id);
        }
        [HttpGet]
        [Route("CorrectPassword")]
        public bool CorrectPassword(string login, string password)
        {
            return repository.Get().FirstOrDefault(t => t.Login == login && t.Password == password) != null;
        }
        [HttpGet]
        [Route("Login")]
        public string Login(string login, string password)
        {
            var user = repository.Get().FirstOrDefault(t => t.Login == login);
            if (user == null) return "Wrong login";
            if (user.Password == password) return JsonSerializer.Serialize(user, options);
            else return "Wrong password";
        }

        [HttpPost]
        [Route("Edit")]
        public int Edit([FromBody] Users Data)
        {
            return repository.Save(Data);
        }
        [HttpGet]
        [Route("Delete")]
        public string Delete(int id)
        {
            repository.DeleteById(id);
            return "OK";
        }
    }
}   
