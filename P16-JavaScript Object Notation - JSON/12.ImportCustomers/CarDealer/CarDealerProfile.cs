using AutoMapper;
using CarDealer.Data;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    using DTOs.Import;
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
            this.CreateMap<ImportCarDto, Car>();

            //Customer
            this.CreateMap<ImportCustomerDto, Customer>();

        }
    }
}
