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
            this.CreateMap<Category, ExportCategoriesByProductsCountDto>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.CategoryProducts.Count))
                .ForMember(dest => dest.AveragePrice,
                    opt => opt.MapFrom(src => src.CategoryProducts.Average(cp => cp.Product.Price)))
                .ForMember(dest => dest.TotalRevenue,
                    opt => opt.MapFrom(src => src.CategoryProducts.Sum(cp => cp.Product.Price)));

            //CategoryProduct
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
        }
    }
}
