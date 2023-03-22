namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Newtonsoft.Json;


    public class ImportPrisonersWithMailsDto
    {

        [Required]
        [MinLength(ValidationConstatnts.PrisonerFullnameMinLength)]
        [MaxLength(ValidationConstatnts.PrisonerFullnameMaxLength)]
        [JsonProperty(nameof(FullName))]
        public string FullName { get; set; }


        [Required]
        [RegularExpression(ValidationConstatnts.PrisonerNickNameRegex)]
        [JsonProperty(nameof(Nickname))]
        public string Nickname { get; set; }


        [Range(ValidationConstatnts.PrisonerAgeMinValue, ValidationConstatnts.PrisonerAgeMaxValue)]
        [JsonProperty(nameof(Age))]
        public int Age { get; set; }


        [Required]
        [JsonProperty(nameof(IncarcerationDate))]
        public string IncarcerationDate { get; set; }


        [JsonProperty(nameof(ReleaseDate))]
        public string ReleaseDate { get; set; }


        [Range(typeof(decimal), ValidationConstatnts.NonNegativeDecimalMinValue, ValidationConstatnts.NonNegativeDecimalMaxValue)]
        [JsonProperty(nameof(Bail))]
        public decimal? Bail { get; set; }


        [JsonProperty(nameof(CellId))]
        public int? CellId { get; set; }


        [JsonProperty(nameof(Mails))]
        public ImportPrisonerMailDto[] Mails { get; set; }
    }
}
