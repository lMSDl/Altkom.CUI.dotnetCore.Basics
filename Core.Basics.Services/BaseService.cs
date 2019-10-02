using System;
using System.Net.Http;

namespace Core.Basics.Services
{
    public abstract class BaseService
    {
        protected HttpClient Client {get;}
        abstract protected string Path {get;}

        public BaseService(HttpClient httpClient) {
            Client = httpClient;
        }

    }
}
