using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.IO;
using DkitShopWebSite.Services;
using WebApi.Domain.Entities;
using System.Diagnostics;
using System;
using System.Text;
using System.Net.Http;

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

            request.MaximumAutomaticRedirections = 100000;
            request.MaximumResponseHeadersLength = 100000;
            request.ContentLength = 10000;

            request.ContentType = "application/json";
            request.Credentials = CredentialCache.DefaultCredentials;

            request.AutomaticDecompression = DecompressionMethods.GZip;


            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            try
            {
               return JsonSerializer.Deserialize<T>(json, options);
                
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Метод для отправки данных на сервер
        /// </summary>
        public static string Send(string serverRouter)
        {
            string response = string.Empty;
            string url = $"{Config.ServerUrl}/{serverRouter}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.MaximumAutomaticRedirections = 100000;
            request.MaximumResponseHeadersLength = 100000;
            request.ContentLength = 10000;

            request.ContentType = "application/json";
            request.Credentials = CredentialCache.DefaultCredentials;

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

            request.MaximumAutomaticRedirections = 100;
            request.MaximumResponseHeadersLength = 100;
            request.ContentLength = 100;

            request.ContentType = "application/json";
            request.Credentials = CredentialCache.DefaultCredentials;

            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
            using (Stream stream = responseStream.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                response = reader.ReadToEnd();
            }

            return response;
        }

        public static string SendPost(string serverRouter, T data)
        {
            //string response = string.Empty;
            //string jsonData = JsonSerializer.Serialize(data); //

            //string url = $"{Config.ServerUrl}/{serverRouter}";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//

            //request.ContentType = "application/json";
            //request.MediaType = "application/json";
            //request.Accept = "application/json";
            //request.Method = "POST";



            //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData); //


            //request.ContentLength = byteArray.Length;//



            //request.AutomaticDecompression = DecompressionMethods.GZip;

            //using (Stream dataStream = request.GetRequestStream())
            //{
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //}

            //using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
            //{
            //    using (Stream stream = responseStream.GetResponseStream())
            //    {
            //        using (StreamReader reader = new StreamReader(stream))
            //        {
            //            response = reader.ReadToEnd();
            //        }
            //    }
            //}




            //return response;

            string json = JsonSerializer.Serialize(data); //

            string url = $"{Config.ServerUrl}/{serverRouter}";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string result = String.Empty;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}
