
using System;
using Bogus;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class ProductFaker : BaseFaker<Product> {
        public ProductFaker() {
            RuleFor(x => x.Name, f => f.Commerce.ProductName());
        }
    }
}