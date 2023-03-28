namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Trucks.Common;

    public class ImportClientDto
    {
        [Required]
        [MinLength(ValidationConstants.ClientNameMinLength)]
        [MaxLength(ValidationConstants.ClientNameMaxLength)]
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;


        [Required]
        [MinLength(ValidationConstants.ClientNationalityMinLength)]
        [MaxLength(ValidationConstants.ClientNationalityMaxLength)]
        [JsonProperty("Nationality")]
        public string Nationality { get; set; } = null!;


        [Required]
        [JsonProperty("Type")]
        public string Type { get; set; } = null!;


        [JsonProperty("Trucks")]
        public int[] Trucks { get; set; }
    }
}
