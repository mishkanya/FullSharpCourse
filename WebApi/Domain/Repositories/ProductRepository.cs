using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Domain.Entities;
using System.Collections.Generic;

namespace WebApi.Domain.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> Get()
        {
            return context.Product.OrderBy(x => x.Id);
        }
        public Product GetById(int id)
        {
            return context.Product.Single(x => x.Id == id);
        }
        public int Save(Product entity)
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
            context.Product.Remove(new Product() { Id = id });
            context.SaveChanges();
        }
    }
}
