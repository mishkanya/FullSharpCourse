using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using WebApi.Domain.Entities;
using System.Collections.Generic;

namespace WebApi.Domain.Repositories
{
    public class UsersRepository
    {
        private readonly AppDbContext context;

        public UsersRepository(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Метод возвращающий список пользователей 
        /// </summary>
        public IEnumerable<Users> Get()
        {
            return context.UsersDB.OrderBy(x => x.Id);
        }

        /// <summary>
        /// Метод возвращающий пользователя по его ID
        /// </summary>
        public Users GetById(int id)
        {
            return context.UsersDB.Single(x => x.Id == id);
        }

        /// <summary>
        /// Метод позволяющий изменить информацию о пользователе
        /// </summary>
        public int Save(Users entity)
        {
            if (entity.Id == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
            return entity.Id;
        }


        /// <summary>
        /// Метод для удаления пользователя по ID 
        /// </summary>
        public void DeleteById(int id)
        {
            context.UsersDB.Remove(new Users() { Id = id });
            context.SaveChanges();
        }
    }
}
