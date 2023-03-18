using AutoMapper;

namespace ProductShop
{
    using DTOs.Import;
    using Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //User
            this.CreateMap<ImportUserDto, User>();

            //Product
            this.CreateMap<ImportProductDto, Product>();

            //categories
            this.CreateMap<ImportCategoryDto, Category>();

            //CategoryProduct
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
        }
    }
}
