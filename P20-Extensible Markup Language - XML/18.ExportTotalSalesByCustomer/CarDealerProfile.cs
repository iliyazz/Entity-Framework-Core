using AutoMapper;

namespace CarDealer
{
    using System.Globalization;
    using DTOs.Export;
    using DTOs.Export.CarsWithTheirListOfParts;
    using DTOs.Import;
    using DTOs.Import.ImportCar;
    using Models;
    using Utilities;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            //Supplier
            this.CreateMap<ImportSupplierDto, Supplier>();
            this.CreateMap<Supplier, ExportLocalSuppliersDto>()
                .ForMember(dest => dest.PartsCount, opt => opt.MapFrom(src => src.Parts.Count));

            //Part
            this.CreateMap<ImportPartDto, Part>();
            this.CreateMap<Part, ExportListOfPartsDto>();

            //Car
            this.CreateMap<ImportCarDto, Car>()
                .ForSourceMember(s => s.Parts, opt => opt.DoNotValidate());
            this.CreateMap<Car, ExportCarsWithDistanceDto>();
            this.CreateMap<Car, ExportCarsFromMakeBmwDto>();
            this.CreateMap<Car, ExportCarsWithTheirListOfPartsDto>()
                .ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.PartsCars.Select(pc => pc.Part).OrderByDescending(p => p.Price).ToArray()));
            this.CreateMap<Car, ExportTotalSalesByCustomerDto>();

            //Customer
            this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateTime.Parse(src.BirthDate, CultureInfo.InvariantCulture)));
            this.CreateMap<Customer, ExportTotalSalesByCustomerDto>()
                .ForMember(dest => dest.BoughtCars, opt => opt.MapFrom(src => src.Sales.Count));
                //.ForMember(dest => dest.SpentMoney,
                //    opt => opt.MapFrom(src =>
                //        src.Sales.Select(sale => sale.Car).SelectMany(car => car.PartsCars)
                //            .Sum(partcar => src.Sales.Select(c => c.Customer.IsYoungDriver) ? partcar.Part.Price * 0.95m : partcar.Part.Price)));

            //Sale
            this.CreateMap<ImportSaleDto, Sale>();

        }
    }
}
