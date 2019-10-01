
using System;
using Bogus;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class CustomerFaker : BaseFaker<Customer> {
        public CustomerFaker() {
            RuleFor(x => x.Gender, f => f.PickRandom<Gender>());
            RuleFor(x => x.FirstName, (f, c) => f.Name.FirstName((Bogus.DataSets.Name.Gender)(c.Gender)));
            RuleFor(x => x.LastName, (f, c) => f.Name.LastName((Bogus.DataSets.Name.Gender)(c.Gender)));
            RuleFor(x => x.LoyaltyCardId, f => f.Random.Bool() ? Guid.NewGuid() : (Guid?) null);
        }
    }
}