using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using DkitShopWebSite.Domain.Repositories;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using DkitShopWebSite.Views.Helpers;

namespace DkitShopWebSite.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public IActionResult Index()
        {
            return View(UsersRepository.Get());
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = UsersRepository.GetById((int)id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Id, string login, string password, string isAdmin, IFormFile AvatarBase64, string Name)
        {
            Users users = new Users()
            {
                Id = default,
                Login = login,
                Password = password,
                IsAdmin = isAdmin == "on",
                AvatarBase64 = $"data:image/png;base64,{AvatarBase64.FileToBase64()}",
                Name = Name
            };
            if (ModelState.IsValid)
            {
                UsersRepository.Save(users);
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = UsersRepository.GetById((int)id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Id, string login, string password, string isAdmin, IFormFile AvatarBase64, string Name, string oldimg)
        {
            string imgSrc;
            if(AvatarBase64 != null)
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
                IsAdmin= isAdmin ==  "on",
                Name = Name,
                AvatarBase64 = imgSrc
            };

            if (ModelState.IsValid)
            {

                UsersRepository.Save(users);
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = UsersRepository.GetById((int)id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var users = UsersRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return UsersRepository.GetById((int)id) != null;
        }
    }
}
