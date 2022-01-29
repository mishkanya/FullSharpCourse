using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;

namespace DkitShopWebSite.Domain.Repositories
{
    public static class ProductRepository
    {
        public static IEnumerable<Product> Get()
        {
            return AppServerContext<IEnumerable<Product>>.Get("Product/Get").OrderBy(x => x.Id);
        }
        public static Product GetById(int id)
        {
            return AppServerContext<Product>.Get($"Product/GetById?id={id}");
        }

        public static string Edit(Product entity)
        {
            string id = AppServerContext<Product>.Send("Product/Edit?ProductData=%data%", entity);
            return id;
        }

        public static string Create(Product entity)
        {
            string id = AppServerContext<Product>.Send("Product/Create?ProductData=%data%", entity);
            return id;
        }
        public static string Create(int id)
        {
            string response = AppServerContext<Product>.Send($"Product/Delete?id={id}");
            return response;
        }
    }
}
