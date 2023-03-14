using AutoMapper;

namespace ProductShop
{
    using DTOs.Import;
    using Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            
            this.CreateMap<ImportUserDto, User>();

            this.CreateMap<ImportProductDto, Product>();
        }
    }
}
