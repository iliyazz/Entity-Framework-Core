namespace Artillery
{
    using AutoMapper;
    using Data.Models;
    using DataProcessor.ExportDto;

    public class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            this.CreateMap<Country, ExportCountryDto>()
                .ForMember(dest => dest.Country, memOpt => memOpt.MapFrom(src => src.CountryName))
                .ForMember(dest => dest.ArmySize, memOpt => memOpt.MapFrom(src => src.ArmySize));
            this.CreateMap<Gun, ExportGunDto>()
                .ForMember(dest => dest.Manufacturer, memOpt => memOpt.MapFrom(src => src.Manufacturer))
                .ForMember(dest => dest.GunType, memOpt => memOpt.MapFrom(src => src.GunType))
                .ForMember(dest => dest.GunWeight, MemOpt => MemOpt.MapFrom(src => src.GunWeight))
                .ForMember(dest => dest.BarrelLength, memOpt => memOpt.MapFrom(src => src.BarrelLength))
                .ForMember(dest => dest.Range, memOpt => memOpt.MapFrom(src => src.Range));
        }
    }
}