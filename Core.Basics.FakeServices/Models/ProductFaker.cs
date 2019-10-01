
using System;
using Bogus;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class ProductFaker : Faker<Product> {
        public ProductFaker() : base("pl") {
            StrictMode(true);
            RuleFor(x => x.Id, f => f.IndexGlobal);
            RuleFor(x => x.Name, f => f.Commerce.ProductName());
        }
    }
}