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
    public class ProductsTypeController : Controller
    {
        
        // GET: ProductsType
        public IActionResult Index()
        {
            return View(ProductTypesRepository.Get().ToList());
        }

        // GET: ProductsType/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypes = ProductTypesRepository.GetById((int)id);
            if (productTypes == null)
            {
                return NotFound();
            }

            return View(productTypes);
        }

        // GET: ProductsType/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,Name")] ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                ProductTypesRepository.Create(productTypes);
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // GET: ProductsType/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypes = ProductTypesRepository.GetById((int)id);
            if (productTypes == null)
            {
                return NotFound();
            }
            return View(productTypes);
        }

        // POST: ProductsType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name")] ProductTypes productTypes)
        {
            if (id != productTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductTypesRepository.Edit(productTypes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // GET: ProductsType/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTypes = ProductTypesRepository.GetById((int)id);
            if (productTypes == null)
            {
                return NotFound();
            }

            return View(productTypes);
        }

        // POST: ProductsType/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            ProductTypesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
