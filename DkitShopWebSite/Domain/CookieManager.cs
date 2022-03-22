using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace DkitShopWebSite.Domain
{
    public class CookieManager : ICookieManager
    {

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // интерфейс для доступа к HttpContex, для работы с куки
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Конструктор в котором мы получаем объект IHttpContextAccessor и сохраняем его
        public CookieManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Метод для сохранения куки
        /// </summary>
        public void Set(string key, object value)
        {
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true, //<- Без этой строки ничего работать не будет
                Expires = DateTime.Now.AddMonths(1),
                SameSite = SameSiteMode.Lax
            };
            var valuestr = JsonSerializer.Serialize(value, options);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, valuestr,cookieOptions);
        }

        public string Get(string key)
        {
            var t = _httpContextAccessor.HttpContext.Request.Cookies[key];
            if (t == null)
                return null;
            if (t.Replace(" ", "") == "")
                return null;

            if (t.Replace(" ", "").Replace("\n", "").Replace("\"", "").Replace("'", "") == "")
                return null;
            Debug.WriteLine(t + "123456");

            return t;
        }

        /// <summary>
        /// Метод для получения данных куки
        /// </summary>
        public T Get<T>(string key) 
        {
            var t = _httpContextAccessor.HttpContext.Request.Cookies[key];

            if (t == null)
                return default(T);
            if (t.Replace(" ", "") == "")
                return default(T);

            if (t.Replace(" ", "").Replace("\n", "").Replace("\"", "").Replace("'", "") == "")
                return default(T);
            Debug.WriteLine(t + "123456");

            return JsonSerializer.Deserialize<T>(t,options);
        }
    }
    public static class CookieWorker
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public static void Set(this HttpContext context ,string key, object value)
        {
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true, //<- Без этой строки ничего работать не будет
                Expires = DateTime.Now.AddMonths(1),
                SameSite = SameSiteMode.Lax
            };
            context.Response.Cookies.Append(key, JsonSerializer.Serialize(value,options), cookieOptions);
        }

        public static T Get<T>(this HttpContext context,string key)
        {
            var t = context.Request.Cookies[key];

            if (t == null)
                return default(T);
            if (t.Replace(" ", "") == "")
                return default(T);

            if (t.Replace(" ", "").Replace("\n", "").Replace("\"","").Replace("'","") == "")
                return default(T);
            Debug.WriteLine(t + "123456");
            return JsonSerializer.Deserialize<T>(t);
        }

        /// <summary>
        /// Метод для получения данных куки
        /// </summary>
        public static string Get(this HttpContext context,string key)
        {
            var t = context.Request.Cookies[key];
            if (t == null)
                return null;
            if (t.Replace(" ", "") == "")
                return null;

            if (t.Replace(" ", "").Replace("\n", "").Replace("\"", "").Replace("'", "") == "")
                return null;

            return t;
        }
    }

    public interface ICookieManager
    {
        string Get(string key);
        void Set(string key, object value);
        T Get<T>(string key);
    }
}
