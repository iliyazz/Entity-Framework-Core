namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;

    [XmlType("Officer")]
    public class ImportOfficerWithPrisonersDto
    {
        [Required]
        [MinLength(ValidationConstatnts.OfficerFullnameMinLength)]
        [MaxLength(ValidationConstatnts.OfficerFullnameMaxLength)]
        [XmlElement("Name")]
        public string FullName { get; set; }


        //[Required]
        [Range(typeof(decimal), ValidationConstatnts.NonNegativeDecimalMinValue, ValidationConstatnts.NonNegativeDecimalMaxValue)]
        [XmlElement("Money")]
        public decimal Salary { get; set; }


        //[Required]
        [XmlElement("Position")]
        public string Position { get; set; }


        //[Required]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }


        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }


        [XmlArray("Prisoners")]
        public ImportOfficerPrisonerDto[] Prisoners { get; set; }
    }
}
