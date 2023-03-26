namespace Artillery.DataProcessor.ImportDto
{
    using Artillery.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Country")]
    public class ImportCountryDto
    {
        [Required]
        [MinLength(ValidationConstants.CountryCountryNameMinLength)]
        [MaxLength(ValidationConstants.CountryCountryNameMaxLength)]
        [XmlElement("CountryName")]
        public string CountryName { get; set; }


        [Required]
        [Range(ValidationConstants.CountryArmySizeMinValue, ValidationConstants.CountryArmySizeMaxValue)]
        [XmlElement("ArmySize")]
        public int ArmySize { get; set; }
    }
}
