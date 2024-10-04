using Talabat;

namespace TalabatAPI.DTOs
{
    //DTO: Data Transfer Object
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? PictureUrl { get; set; }

        public String? ProductBrand { get; set; }

        public int ProductBrandId { get; set; }

        public String? ProductType { get; set; }

        public int ProductTypeId { get; set; }
    }
}
