using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;

namespace DkitShopWebSite.Domain.Repositories
{
    public static class ProductTypesRepository
    {
        public static IEnumerable<ProductTypes> Get()
        {
            return AppServerContext<IEnumerable<ProductTypes>>.Get("ProductTypes/Get").OrderBy(x => x.Id);
        }
        public static ProductTypes GetById(int id)
        {
            return AppServerContext<ProductTypes>.Get($"ProductTypes/GetById?id={id}");
        }

        public static string Edit(ProductTypes entity)
        {
            string id = AppServerContext<ProductTypes>.Send("ProductTypes/Edit?ProductTypesData=%data%", entity);
            return id;
        }

        public static string Create(ProductTypes entity)
        {
            string id = AppServerContext<ProductTypes>.Send("ProductTypes/Create?ProductTypesData=%data%", entity);
            return id;
        }
        public static string Create(int id)
        {
            string response = AppServerContext<ProductTypes>.Send($"ProductTypes/Delete?id={id}");
            return response;
        }
    }
}
