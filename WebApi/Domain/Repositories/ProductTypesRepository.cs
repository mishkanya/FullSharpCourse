using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Domain.Entities;
using System.Collections.Generic;

namespace WebApi.Domain.Repositories
{
    public class ProductTypesRepository
    {
        private readonly AppDbContext context;

        public ProductTypesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductTypes> Get()
        {
            return context.ProductTypes.OrderBy(x => x.Id);
        }
        public ProductTypes GetById(int id)
        {
            return context.ProductTypes.Single(x => x.Id == id);
        }
        public int Save(ProductTypes entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }
        public void DeleteById(int id)
        {
            context.ProductTypes.Remove(new ProductTypes() { Id = id });
            context.SaveChanges();
        }
    }
}
