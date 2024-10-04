using System.ComponentModel.DataAnnotations;

namespace TalabatAPI
{
    public class AddressDto
    {
        [Required]
        //public int Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
