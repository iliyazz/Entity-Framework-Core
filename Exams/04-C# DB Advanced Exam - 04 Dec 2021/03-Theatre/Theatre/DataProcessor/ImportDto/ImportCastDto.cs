namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Theatre.Common;
    using Theatre.Data.Models;

    [XmlType("Cast")]
    public class ImportCastDto
    {

        [Required]
        [MinLength(ValidationConstants.CastFullNameMinLength)]
        [MaxLength(ValidationConstants.CastFullNameMaxLength)]
        [XmlElement("FullName")]
        public string FullName { get; set; }



        [Required]
        [XmlElement("IsMainCharacter")]
        public bool IsMainCharacter { get; set; }


        [Required]
        [RegularExpression(ValidationConstants.CastPhoneNumberRegex)]
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }


        [Required]
        [XmlElement("PlayId")]
        public int PlayId { get; set; }
    }
}
