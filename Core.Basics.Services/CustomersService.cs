using System;
using System.Net.Http;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.Services
{
    public class CustomersService : EntityService<Customer>, ICustomersService
    {
        public CustomersService(HttpClient httpClient) : base(httpClient) {
        }

        protected override string Path => "api/customers";
    }
}
