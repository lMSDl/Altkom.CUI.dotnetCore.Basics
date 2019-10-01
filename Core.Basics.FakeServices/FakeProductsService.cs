using Bogus;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.FakeServices
{
    public class FakeProductsService : FakeBaseService<Product>, IProductsService
    {
        public FakeProductsService(Faker<Product> faker, int count) : base(faker, count)
        {
        }
    }
}