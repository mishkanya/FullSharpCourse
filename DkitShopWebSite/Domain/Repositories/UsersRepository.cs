using System.Collections.Generic;
using System.Linq;
using WebApi.Domain.Entities;

namespace DkitShopWebSite.Domain.Repositories
{
    public static class UsersRepository
    {
        /// <summary>
        /// Метод возвращающий список пользователей 
        /// </summary>
        public static IEnumerable<Users> Get()
        {
            return AppServerContext<IEnumerable<Users>>.Get("Users/Get").OrderBy(x => x.Id);
        }

        /// <summary>
        /// Метод возвращающий пользователя по его ID
        /// </summary>
        public static Users GetById(int id)
        {
            return AppServerContext<Users>.Get($"Users/GetById?id={id}");
        }

        /// <summary>
        /// Метод позволяющий изменить информацию о пользователе
        /// </summary>
        public static string Save(Users entity)
        {
            string id = AppServerContext<Users>.Send("Users/Edit?UserData=%data%", entity);
            return id;
        }

        /// <summary>
        /// Метод регистрации пользователя
        /// </summary>
        public static string Registration(string login, string password)
        {
            string response = AppServerContext<Users>.Send($"Users/Registration?login={login}&password={password}");
            return response;
        }

        /// <summary>
        /// Проверка правильности пароля
        /// </summary>
        public static string CorrectPassword(string login, string password)
        {
            string response = AppServerContext<Users>.Send($"Users/CorrectPassword?login={login}&password={password}");
            return response;
        }

        /// <summary>
        /// Метод входа
        /// </summary>
        public static Users Login(string login, string password)
        {
            Users response = AppServerContext<Users>.Get($"Users/Login?login={login}&password={password}");
            return response;
        }
    }
}
