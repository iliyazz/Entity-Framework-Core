namespace Theatre
{
    using System.Globalization;
    using AutoMapper;
    using Data.Models;
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
        }
    }
}
