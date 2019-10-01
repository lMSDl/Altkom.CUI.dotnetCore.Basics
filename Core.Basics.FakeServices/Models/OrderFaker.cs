
using System.Linq;
using Bogus;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class OrderFaker : Faker<Order> {
        public OrderFaker(ICustomersService customersService, IProductsService productsService) : base("pl") {
            StrictMode(true);
            RuleFor(x => x.Id, f => f.IndexGlobal);
            RuleFor(x => x.Customer, f => f.PickRandom(customersService.GetAsync().Result));
            RuleFor(x => x.Products, f => f.PickRandom(productsService.GetAsync().Result, f.Random.Number(1, 10)).ToList());
        }
    }
}