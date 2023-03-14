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
            
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<ImportCategoryDto, Category>();
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
            this.CreateMap<Product, ExportProductInRangeDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.ProductPrice, opt => opt.MapFrom(s => s.Price))
                .ForMember(dest => dest.SellerName,
                    opt => opt.MapFrom(src => $"{src.Seller.FirstName} {src.Seller.LastName}"));

        }
    }
}
