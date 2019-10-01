
using Bogus;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class CustomerFaker : Faker<Customer> {

        public CustomerFaker() : base("pl") {
            StrictMode(true);
            RuleFor(x => x.Id, f => f.IndexGlobal);
            RuleFor(x => x.FirstName, f => f.Person.FirstName);
            RuleFor(x => x.LastName, f => f.Person.LastName);
        }
    }
}