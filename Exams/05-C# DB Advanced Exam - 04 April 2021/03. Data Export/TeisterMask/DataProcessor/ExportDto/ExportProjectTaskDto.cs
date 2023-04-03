namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Xml.Serialization;
    using Data.Models.Enums;

    [XmlType("Task")]
    public class ExportProjectTaskDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }


        [XmlElement("Label")]
        public string Label { get; set; }

    }
}
