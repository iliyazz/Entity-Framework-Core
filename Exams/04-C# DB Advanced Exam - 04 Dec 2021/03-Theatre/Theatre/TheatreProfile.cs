namespace Theatre
{
    using System.Globalization;
    using AutoMapper;
    using Data.Models;
    using DataProcessor.ExportDto;
    using DataProcessor.ImportDto;
    using Theatre.Common;

    public class TheatreProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public TheatreProfile()
        {
            //this.CreateMap<ImportPlayDto, Play>()
            //    .ForMember(dest => dest.Duration, memOpt => memOpt.MapFrom(src => src.Duration));
            //this.CreateMap<ImportTheatreTicketDto, Theatre>();
            //this.CreateMap<ImportTicketDto, Ticket>();
            this.CreateMap<Cast, ExportCastDto>()
                .ForMember(dest => dest.FullName, memOpt => memOpt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.MainCharacter, memOpt => memOpt.MapFrom(src => src.IsMainCharacter));
            this.CreateMap<Play, ExportPlayCastDto>()
                .ForMember(dest => dest.Title, memOpt => memOpt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Duration, memOpt => memOpt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Rating, memOpt => memOpt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Genre, memOpt => memOpt.MapFrom(src => src.Genre));

        }
    }
}
