namespace Footballers.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Footballer")]
    public class ImportFootballerDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        //[XmlElement("Name")]
        public string Name { get; set; }


        [Required]
        [XmlElement("ContractStartDate")]
        public string ContractStartDate { get; set; }


        [Required]
        [XmlElement("ContractEndDate")]
        public string ContractEndDate { get; set; }


        [Required]
        [XmlElement("BestSkillType")]
        [Range(0,4)]
        public int BestSkillType { get; set; }


        [Required]
        [XmlElement("PositionType")]
        [Range(0,3)]
        public int PositionType { get; set; }

    }
}
