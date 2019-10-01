using Bogus;
using Core.Basics.IServices;
using Core.Basics.Models;

namespace Core.Basics.FakeServices
{
    public class FakeCustomersService : FakeBaseService<Customer>, ICustomersService
    {
        public FakeCustomersService(Faker<Customer> faker, int count) : base(faker, count)
        {
        }
    }
}