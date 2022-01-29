using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.IO;
using DkitShopWebSite.Services;

namespace DkitShopWebSite.Domain
{
    public static class AppServerContext<T> where T : class
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        /// <summary>
        /// Метод для получения данных с сервера
        /// </summary>
        public static T Get(string serverRouter)
        {
            string json = string.Empty;
            string url = $"{Config.ServerUrl}/{serverRouter}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// Метод для отправки данных на сервер
        /// </summary>
        public static string Send(string serverRouter)
        {
            string response = string.Empty;
            string url = $"{Config.ServerUrl}/{serverRouter}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
            using (Stream stream = responseStream.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }

        /// <summary> 
        /// Метод для отправки данных на сервер
        /// </summary>
        /// <param name="serverRouter">
        /// Роутер вида ?data=%data%
        /// Где %data% будет заменяться на json строку
        /// </param>
        public static string Send(string serverRouter, T data)
        {
            string response = string.Empty;
            string jsonData = JsonSerializer.Serialize(data);

            serverRouter = serverRouter.Replace("%data%",jsonData);

            string url = $"{Config.ServerUrl}/{serverRouter}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
            using (Stream stream = responseStream.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }
    }
}
