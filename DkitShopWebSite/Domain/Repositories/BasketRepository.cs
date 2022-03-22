using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;
using System;

namespace DkitShopWebSite.Domain.Repositories
{
    public static class BasketRepository
    {
        public static IEnumerable<Basket> Get()
        {
            return AppServerContext<IEnumerable<Basket>>.Get("Basket/Get").OrderBy(x => x.Id);
        }
        public static Basket GetById(int id)
        {
            return AppServerContext<Basket>.Get($"Basket/GetById?id={id}");
        }

        public static string Edit(Basket entity)
        {
            string id = AppServerContext<Basket>.SendPost("Basket/Edit", entity);
            return id;
        }

        public static string Create(Basket entity)
        {
            string id = AppServerContext<Basket>.SendPost("Basket/Create", entity);
            return id;
        }
        public static string Create(int id)
        {
            string response = AppServerContext<Basket>.Send($"Basket/Delete?id={id}");
            return response;
        }
        public static bool Delete(int? id)
        {
            String response = AppServerContext<String>.Get($"Basket/Delete?id={id}");
            return true;
        }
    }
}
