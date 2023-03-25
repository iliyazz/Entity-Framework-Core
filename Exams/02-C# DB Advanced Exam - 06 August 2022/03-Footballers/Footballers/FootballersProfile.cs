namespace Footballers
{
    using AutoMapper;
    using Data.Models;
    using DataProcessor.ExportDto;

    // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
    public class FootballersProfile : Profile
    {
        public FootballersProfile()
        {
            CreateMap<Coach, ExportCoachDto>()
                .ForMember(dest => dest.Name, memOpt => memOpt.MapFrom(src => src.Name))
                .ForMember(dest => dest.FootballersCount, memOpt => memOpt.MapFrom(src => src.Footballers.Count))
                .ForMember(dest => dest.Footballers,
                    memOpt => memOpt.MapFrom(src => src.Footballers.ToArray().OrderBy(f => f.Name).ToArray()));
            CreateMap<Footballer, ExportCoachFootballerDto>()
                .ForMember(dest => dest.Name, memOpt => memOpt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Position, memOpt => memOpt.MapFrom(src => src.PositionType.ToString()));
        }
    }
}
