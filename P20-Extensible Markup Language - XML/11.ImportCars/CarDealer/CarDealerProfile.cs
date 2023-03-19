using AutoMapper;

namespace CarDealer
{
    using DTOs.Import;
    using DTOs.Import.NewFolder;
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

        }
    }
}
