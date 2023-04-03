namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Common;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class ImportSellerDto
    {
        [Required]
        [MinLength(ValidationConstants.SellerNameMinLength)]
        [MaxLength(ValidationConstants.SellerNameMaxLength)]
        [JsonProperty("Name")]
        public string Name { get; set; }


        [Required]
        [MinLength(ValidationConstants.SellerAddressMinLength)]
        [MaxLength(ValidationConstants.SellerAddressMaxLength)]
        [JsonProperty("Address")]
        public string Address { get; set; }


        [Required]
        [JsonProperty("Country")]
        public string Country { get; set; }


        [Required]
        [RegularExpression(ValidationConstants.SellerWebsiteRegex)]
        [JsonProperty("Website")]
        public string Website { get; set; }


        [JsonProperty("Boardgames")]
        public int[] Boardgames { get; set;}

    }
}
