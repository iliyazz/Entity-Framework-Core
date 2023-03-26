namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using TeisterMask.Common;
    using TeisterMask.Data.Models.Enums;

    [XmlType("Task")]
    public class ImportPrTaskDto
    {
        [Required]
        [MinLength(ValidationConstants.TaskNameMinLength)]
        [MaxLength(ValidationConstants.TaskNameMaxLength)]
        [XmlElement("Name")]
        public string Name { get; set; }


        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }


        [Required]
        [XmlElement("DueDate")]
        public string DueDate { get; set; }


        [Required]
        [XmlElement("ExecutionType")]
        [Range(0,3)]
        public int ExecutionType { get; set; }


        [Required]
        [XmlElement("LabelType")]
        [Range(0, 4)]
        public int LabelType { get; set; }
    }
}
