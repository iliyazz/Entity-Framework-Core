namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using TeisterMask.Common;

    [XmlType("Project")]
    public class ImportProjectTaskDto
    {

        [Required]
        [MinLength(ValidationConstants.TaskNameMinLength)]
        [MaxLength(ValidationConstants.TaskNameMaxLength)]
        [XmlElement("Name")]
        public string Name { get; set; }


        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }


        [XmlElement("DueDate")]
        public string? DueDate { get; set; }



        [XmlArray("Tasks")]
        public ImportPrTaskDto[] Tasks { get; set; }
    }
}
