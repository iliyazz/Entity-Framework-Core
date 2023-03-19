using AutoMapper;

namespace CarDealer
{
    using System.Globalization;
    using DTOs.Export;
    using DTOs.Import;
    using DTOs.Import.ImportCar;
    using Models;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Supplier
            this.CreateMap<ImportSupplierDto, Supplier>();

            //Part
            this.CreateMap<ImportPartDto, Part>();

            //Car
            this.CreateMap<ImportCarDto, Car>()
                .ForSourceMember(s => s.Parts, opt => opt.DoNotValidate());
            this.CreateMap<Car, ExportCarsWithDistanceDto>();
            this.CreateMap<Car, ExportCarsFromMakeBmwDto>();

            //Customer
            this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateTime.Parse(src.BirthDate, CultureInfo.InvariantCulture)));

            //Sale
            this.CreateMap<ImportSaleDto, Sale>();

        }
    }
}
