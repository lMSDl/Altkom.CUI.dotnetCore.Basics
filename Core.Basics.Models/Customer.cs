using System;

namespace Core.Basics.Models
{
    public class Customer : Base
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public Gender Gender {get; set;}
    }
}
