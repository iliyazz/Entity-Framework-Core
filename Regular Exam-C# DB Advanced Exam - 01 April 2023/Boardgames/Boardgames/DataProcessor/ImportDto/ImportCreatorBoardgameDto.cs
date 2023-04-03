namespace Boardgames.DataProcessor.ImportDto
{
    using Boardgames.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Boardgame")]
    public class ImportCreatorBoardgameDto
    {
        [Required]
        [MinLength(ValidationConstants.BoardgameNameMinLength)]
        [MaxLength(ValidationConstants.BoardgameNameMaxLength)]
        [XmlElement("Name")]
        public string Name { get; set; }


        [Required]
        [Range(ValidationConstants.BoardgameRatingMinValue, ValidationConstants.BoardgameRatingMaxValue)]
        [XmlElement("Rating")]
        public double Rating { get; set; }


        [Required]
        [Range(ValidationConstants.BoardgameYearPublishedMinValue, ValidationConstants.BoardgameYearPublishedMaxValue)]
        [XmlElement("YearPublished")]
        public int YearPublished { get; set; }


        [Required]
        [XmlElement("CategoryType")]
        [Range(0, 4)]
        public int CategoryType { get; set; }


        [Required]
        [XmlElement("Mechanics")]
        public string Mechanics { get; set; }
    }
}
