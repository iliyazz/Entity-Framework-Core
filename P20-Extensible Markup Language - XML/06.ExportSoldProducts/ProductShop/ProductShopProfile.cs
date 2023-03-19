using AutoMapper;

namespace ProductShop
{
    using DTOs.Export;
    using DTOs.Import;
    using Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //User
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<User, ExportSoldProductDto>()
                .ForMember(dest => dest.SoldProducts, opt => opt.MapFrom(s => s.ProductsSold));

            //Product
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<Product, ExportProductsInRangeDto>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.Buyer.FirstName} {src.Buyer.LastName}"));
            this.CreateMap<Product, ExportProductUserDto>();


            //categories
            this.CreateMap<ImportCategoryDto, Category>();

            //CategoryProduct
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
        }
    }
}
