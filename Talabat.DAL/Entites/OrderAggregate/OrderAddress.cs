using System.ComponentModel.DataAnnotations;

namespace Talabat
{
    public class OrderAddress
    {
        public OrderAddress() { }
        public OrderAddress(string firstName, string lastName, string country, string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

        //public int Id { get; set; } we don't need to use Id->becuase we won't convert this class to table
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}