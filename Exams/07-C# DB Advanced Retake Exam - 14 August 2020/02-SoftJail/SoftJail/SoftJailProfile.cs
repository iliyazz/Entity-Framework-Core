namespace SoftJail
{
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using DataProcessor.ExportDto;
    using DataProcessor.ImportDto;


    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            this.CreateMap<ImportDepartmentCellDto, Cell>();

            this.CreateMap<ImportPrisonerMailDto, Mail>();

            this.CreateMap<Mail, ExportPrisonerMailsDto>()
                .ForMember(dest => dest.Description, memOpt => memOpt.MapFrom(src => string.Join("", src.Description.Reverse())));

            this.CreateMap<Prisoner, ExportPrisonerDto>()
                .ForMember(dest => dest.IncarcerationDate,
                    memOpt => memOpt.MapFrom(src => src.IncarcerationDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Mails, memOpt => memOpt.MapFrom(src => src.Mails));

        }
    }
}
