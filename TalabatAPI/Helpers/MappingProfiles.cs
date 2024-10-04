using AutoMapper;
using Talabat;
using TalabatAPI.DTOs;

namespace TalabatAPI.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
             .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
             .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
             .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());//from dataBase to client side

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, OrderAddress>();
        }
    }
}
