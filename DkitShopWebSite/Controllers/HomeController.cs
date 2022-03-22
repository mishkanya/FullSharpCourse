using DkitShopWebSite.Domain;
using DkitShopWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DkitShopWebSite.Domain.Repositories;
using WebApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using DkitShopWebSite.Views.Helpers;

namespace DkitShopWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookieManager _cookieManager;
        public ICookieManager GetCookieManager { get => _cookieManager; }

        public HomeController(ILogger<HomeController> logger, ICookieManager cookieManager)
        {
            _logger = logger;
            _cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            return View(_cookieManager);
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteFromBasket(string basketElementId)
        {
            BasketRepository.Delete(int.Parse(basketElementId));
            return Redirect("/Home/Basket");
        }

        [HttpPost]
        public IActionResult AddToBasket(string productid)
        {
            BasketRepository.Create(new Basket()
            {
                Id = default,
                ProductId = int.Parse(productid),

                UserId = _cookieManager.Get<Users>("UserData").Id,
                OrderTime = DateTime.Now

            });
            return Redirect("/Home/Products");
        }
        public IActionResult Basket()
        {
            var basketelements = BasketRepository.Get().Where(t => t.UserId == _cookieManager.Get<Users>("UserData").Id);
            return View(basketelements);
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reg(string login, string password1, string password2)
        {
            if (login == "" || login == null || password1 == "" || password1 == null)
                return View("Login", "Необходимо ввести все данные!");

            if (password1 != password2)
                return View("Login", $"Пароли не совпадают");


            var code = UsersRepository.Registration(login, password1);
            if (code == "Ok")
            {
                var user = UsersRepository.Login(login, password1);
                _cookieManager.Set("UserData", user);
                return Redirect("~/Home/Index");
            }
            else
            {
                return View("Login", "Не верные данные");
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Log(string login, string password)
        {
            if (login == "" || login == null || password == "" || password == null)
                return View("Login", "Необходимо ввести все данные!");

            var user = UsersRepository.Login(login, password);
            if (user != null)
            {
                _cookieManager.Set("UserData", new Users()
                {
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name
                });
                return Redirect("~/Home/Index");
            }
            else
            {
                return View("Login", "Не верные данные");
            }
        }
        public IActionResult Profile()
        {
            var user = UsersRepository.GetById(_cookieManager.Get<Users>("UserData").Id);
            _cookieManager.Set("UserData", new Users()
            {
                Id = user.Id, IsAdmin = user.IsAdmin, Login = user.Login, Password = user.Password, Name = user.Name
            });
            return View(user);
        }
        public IActionResult ProfileEdit()
        {
            var user = UsersRepository.GetById(_cookieManager.Get<Users>("UserData").Id);
            _cookieManager.Set("UserData", new Users()
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name
            });
            return View(user);
        }
        [HttpPost]
        public IActionResult ProfileEdit(string Id, string login, string password, IFormFile AvatarBase64, string Name, string oldimg)
        {
            Users oldUser = UsersRepository.GetById(int.Parse(Id));
            string imgSrc;
            if (AvatarBase64 != null)
            {
                imgSrc = $"data:image/png;base64,{AvatarBase64.FileToBase64()}";
            }
            else
            {
                imgSrc = oldimg;
            }
            Users users = new Users()
            {
                Id = int.Parse(Id),
                Login = login,
                Password = password,
                IsAdmin = oldUser.IsAdmin,
                Name = Name,
                AvatarBase64 = imgSrc
            };

            if (ModelState.IsValid)
            {

                UsersRepository.Save(users);
                _cookieManager.Set("UserData", new Users()
                {
                    Id = users.Id,
                    IsAdmin = users.IsAdmin,
                    Login = users.Login,
                    Password = users.Password,
                    Name = users.Name
                });
                return RedirectToAction(nameof(Index));
            }
            return Redirect("/Home/Profile");
        }
    }
}
