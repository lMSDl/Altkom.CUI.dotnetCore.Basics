using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Basics.Services;
using Newtonsoft.Json;

namespace Core.Basics.Client
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            var httpClient = new HttpClient() {BaseAddress = new Uri("http://localhost:5000")};

            var customerService = new CustomersService(httpClient);
            var customers = await customerService.GetAsync();            

            Console.WriteLine(JsonConvert.SerializeObject(customers));
        }
    }
}
