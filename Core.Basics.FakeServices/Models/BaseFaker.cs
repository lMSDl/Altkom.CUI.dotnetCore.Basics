
using System;
using Bogus;
using Core.Basics.Models;

namespace Core.Basics.FakeServices.Models
{
    public class BaseFaker<T> : Faker<T> where T : Base
    {
        public BaseFaker() : base("pl") {
            StrictMode(true);
            RuleFor(x => x.Id, f => f.IndexFaker);
        }
    }
}