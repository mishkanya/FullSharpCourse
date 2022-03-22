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
    public class BasketController : Controller
    {


        // GET: Baskets
        public IActionResult Index()
        {
            return View(BasketRepository.Get());
        }

        // GET: Baskets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = BasketRepository.GetById((int)id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // GET: Baskets/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int userid, int productid)
        {
            Basket basket = new Basket()
            {
                Id = default,
                UserId = userid,
                ProductId = productid,
                OrderTime = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                BasketRepository.Create(basket);
                return RedirectToAction(nameof(Index));
            }
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = BasketRepository.GetById((int)id);
            if (basket == null)
            {
                return NotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, int UserId, int ProductId, DateTime OrderTime)
        {
            Basket basket = new Basket()
            {
                Id = Id,
                UserId = UserId,
                ProductId = ProductId,
                OrderTime = OrderTime
            };

            if (ModelState.IsValid)
            {
                BasketRepository.Edit(basket);
                return RedirectToAction(nameof(Index));
            }
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = BasketRepository.GetById((int)id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            BasketRepository.Delete((int)id);
            return RedirectToAction(nameof(Index));
        }

    }
}
