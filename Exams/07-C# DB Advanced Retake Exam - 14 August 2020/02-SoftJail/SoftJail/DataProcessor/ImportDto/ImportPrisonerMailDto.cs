namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Newtonsoft.Json;


    public class ImportPrisonerMailDto
    {
        [Required]
        [JsonProperty(nameof(Description))]
        public string Description { get; set; }


        [Required]
        [JsonProperty(nameof(Sender))]
        public string Sender { get; set; }


        [Required]
        [RegularExpression(ValidationConstatnts.MailAddressRegex)]
        [JsonProperty(nameof(Address))]
        public string Address { get; set; }
    }
}
