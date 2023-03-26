namespace Artillery.DataProcessor.ImportDto
{
    using Artillery.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Shell")]
    public class ImportShellDto
    {
        [Required]
        [Range(ValidationConstants.ShellShellWeightMinValue, ValidationConstants.ShellShellWeightMaxValue)]
        [XmlElement("ShellWeight")]
        public double ShellWeight { get; set; }


        [Required]
        [MinLength(ValidationConstants.ShellCaliberMinLength)]
        [MaxLength(ValidationConstants.ShellCaliberMaxLength)]
        [XmlElement("Caliber")]
        public string Caliber { get; set; }
    }
}
