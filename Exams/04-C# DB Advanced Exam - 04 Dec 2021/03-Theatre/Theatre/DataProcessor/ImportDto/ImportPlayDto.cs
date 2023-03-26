namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;

    [XmlType("Play")]
    public class ImportPlayDto
    {
        [MinLength(ValidationConstants.PlayTitleMinLength)]
        [MaxLength(ValidationConstants.PlayTitleMaxLength)]
        [XmlElement("Title")]
        public string Title { get; set; }

        [Required]
        [XmlElement("Duration")]
        public string Duration { get; set; }

        [Required]
        [Range(ValidationConstants.PlayRatingMinLength, ValidationConstants.PlayRatingMaxLength)]
        [XmlElement("Raiting")]
        public float Rating { get; set; }

        [Required]
        [XmlElement("Genre")]
        public string Genre { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PlayDescriptionMaxLength)]
        [XmlElement("Description")]
        public string Description { get; set; }

        [Required]
        [MinLength(ValidationConstants.PlayScreenwriterMinLength)]
        [MaxLength(ValidationConstants.PlayScreenwriterMaxLength)]
        [XmlElement("Screenwriter")]
        public string Screenwriter { get; set; }
    }
}
