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
    public class ProductsController : Controller
    {
        // GET: Products
        public IActionResult Index()
        {
            return View(ProductRepository.Get().ToList());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = ProductRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(string Id, string Name, string Description, string Price, IFormFile ImageBase64, string TypeId)
        {
            var product = new Product()
            {
                Id = default,
                Name = Name,
                Description = Description,
                Price = int.Parse(Price),
                ImageBase64 = $"data:image/png;base64,{ImageBase64.FileToBase64()}",
                TypeId = int.Parse(TypeId)
            };
            ProductRepository.Create(product);
            return Redirect("~/Products");
        }
        

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = ProductRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(string Id, string Name, string Description,string oldImg, string Price, IFormFile ImageBase64, string TypeId)
        {
            string base64;
            if (ImageBase64 == null)
            {
                base64 = oldImg;
            }
            else
            {
                base64 = $"data:image/png;base64,{ImageBase64.FileToBase64()}";
            }
            var product = new Product()
            {
                Id = int.Parse(Id),
                Name = Name,
                Description = Description,
                Price = int.Parse(Price),
                ImageBase64 = base64,
                TypeId = int.Parse(TypeId)
            };
            ProductRepository.Create(product);
            return Redirect("~/Products");
        }


        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var product = ProductRepository.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = ProductRepository.Delete(id);
            return Redirect("~/Products");
        }
    }
}
