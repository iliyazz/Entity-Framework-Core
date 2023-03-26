namespace Artillery.DataProcessor.ImportDto
{
    using Artillery.Common;
    using Artillery.Data.Models.Enums;
    using Artillery.Data.Models;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public class ImportGunDto
    {

        [Required]
        [JsonProperty(nameof(ManufacturerId))]
        public int ManufacturerId { get; set; }



        [Required]
        [Range(ValidationConstants.GunGunWeightMinValue, ValidationConstants.GunGunWeightMaxValue)]
        [JsonProperty(nameof(GunWeight))]
        public int GunWeight { get; set; }


        [Required]
        [Range(ValidationConstants.GunBarrelLengthMinValue, ValidationConstants.GunBarrelLengthMaxValue)]
        [JsonProperty(nameof(BarrelLength))]
        public double BarrelLength { get; set; }


        [JsonProperty(nameof(NumberBuild))]
        public int? NumberBuild { get; set; }


        [Required]
        [Range(ValidationConstants.GunRangeMinValue, ValidationConstants.GunRangeMaxValue)]
        [JsonProperty(nameof(Range))]
        public int Range { get; set; }


        [Required]
        [JsonProperty(nameof(GunType))]
        public string GunType { get; set; }


        [Required]
        [JsonProperty(nameof(ShellId))]
        public int ShellId { get; set; }


        [JsonProperty(nameof(Countries))]
        public ImportCountryIdDto[] Countries { get; set; }
    }
}
