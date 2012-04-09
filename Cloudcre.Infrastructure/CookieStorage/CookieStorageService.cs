using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Cloudcre.Infrastructure.CookieStorage
{
    public class CookieStorageService : ICookieStorageService
    {
        #region ICookieStorageService Members

        public void Save<T>(string key, T obj)
        {
            if (HttpContext.Current.Response.Cookies[key] == null)
                throw new ApplicationException("Cookies is null");

            HttpCookie cookie = HttpContext.Current.Response.Cookies[key];
            if (cookie == null)
                return;

            cookie.Value = SerializeToJson(obj);
            HttpContext.Current.Response.Cookies.Remove(key);
            HttpContext.Current.Response.Cookies.Add(cookie);
            //HttpContext.Current.Response.Cookies[key].Value = value;
            //HttpContext.Current.Response.Cookies[key].Expires = expires;
        }

        public T Retrieve<T>(string key) where T : class
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie == null)
                return null;

            return DeserializeToObject<T>(HttpUtility.UrlDecode(cookie.Value));
        }

        #endregion

        private T DeserializeToObject<T>(string json) where T : class
        {
            if (string.IsNullOrEmpty(json))
                return null;

            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof (T));
                return (T) serializer.ReadObject(memoryStream);
            }
        }

        private string SerializeToJson<T>(T obj)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(obj);
        }
    }
}