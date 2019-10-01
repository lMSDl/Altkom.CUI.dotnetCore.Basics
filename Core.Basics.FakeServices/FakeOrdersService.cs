using Bogus;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.FakeServices
{
    public class FakeOrdersService : FakeBaseService<Order>, IOrdersService
    {
        public FakeOrdersService(Faker<Order> faker, int count) : base(faker, count)
        {
        }
    }
}