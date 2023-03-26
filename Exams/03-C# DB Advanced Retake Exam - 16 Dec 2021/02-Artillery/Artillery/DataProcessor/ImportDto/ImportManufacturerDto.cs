namespace Artillery.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;

    [XmlType("Manufacturer")]
    public class ImportManufacturerDto
    {
        [Required]
        [MinLength(ValidationConstants.ManufacturerManufacturerNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerManufacturerNameMaxLength)]
        [XmlElement("ManufacturerName")]
        public string ManufacturerName { get; set; }


        [Required]
        [MinLength(ValidationConstants.ManufacturerFoundedNameMinLength)]
        [MaxLength(ValidationConstants.ManufacturerFoundedNameMaxLength)]
        [XmlElement("Founded")]
        public string Founded { get; set; }
    }
}
