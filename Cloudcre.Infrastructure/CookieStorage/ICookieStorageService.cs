using System;

namespace Cloudcre.Infrastructure.CookieStorage
{
    public interface ICookieStorageService
    {
        void Save<T>(string key, T obj);

        T Retrieve<T>(string key) where T : class;
    }
}