namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Creator")]
    public class ImportCreatorDto
    {
        [Required]
        [MinLength(ValidationConstants.CreatorFirstNameMinLength)]
        [MaxLength(ValidationConstants.CreatorLastNameMaxLength)]
        [XmlElement("FirstName")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(ValidationConstants.CreatorLastNameMinLength)]
        [MaxLength(ValidationConstants.CreatorLastNameMaxLength)]
        [XmlElement("LastName")]
        public string LastName { get; set; }


        [XmlArray("Boardgames")]
        public ImportCreatorBoardgameDto[] Boardgames { get; set; }
    }
}
