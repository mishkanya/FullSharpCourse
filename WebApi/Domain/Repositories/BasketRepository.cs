using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Domain.Entities;
using System.Collections.Generic;

namespace WebApi.Domain.Repositories
{
    public class BasketRepository
    {
        private readonly AppDbContext context;

        public BasketRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Basket> Get()
        {
            return context.Basket.OrderBy(x => x.Id);
        }
        public Basket GetById(int id)
        {
            return context.Basket.Single(x => x.Id == id);
        }
        public int Save(Basket entity)
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
            context.Basket.Remove(new Basket() { Id = id });
            context.SaveChanges();
        }
    }
}
