namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Theatre.Common;
    using Theatre.Data.Models;

    public class ImportTheatreTicketDto
    {
        [Required]
        [MinLength(ValidationConstants.TheatreNameMinLength)]
        [MaxLength(ValidationConstants.TheatreNameMaxLength)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; }



        [Required]
        [Range(ValidationConstants.TheatreNumberOfHallsMinValue, ValidationConstants.TheatreNumberOfHallsMaxValue)]
        [JsonProperty(nameof(NumberOfHalls))]
        public sbyte NumberOfHalls { get; set; }


        [Required]
        [MinLength(ValidationConstants.TheatreDirectorMinLength)]
        [MaxLength(ValidationConstants.TheatreDirectorMaxLength)]
        [JsonProperty(nameof(Director))]
        public string Director { get; set; }


        [Required]
        [JsonProperty(nameof(Tickets))]
        public ImportTicketDto[] Tickets { get; set; }
    }
}
