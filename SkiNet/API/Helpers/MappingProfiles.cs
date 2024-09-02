using AutoMapper;
using SkiNet.API.DTO;

namespace SkiNet.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Pra propriedade ProductBrand do DTO, ela deve ser mapeada recebendo o ProductBrand.Name do Product
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
