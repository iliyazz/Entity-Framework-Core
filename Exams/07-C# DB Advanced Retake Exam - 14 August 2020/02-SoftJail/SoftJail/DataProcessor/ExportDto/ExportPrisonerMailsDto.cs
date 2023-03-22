namespace SoftJail.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Message")]
    public class ExportPrisonerMailsDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
