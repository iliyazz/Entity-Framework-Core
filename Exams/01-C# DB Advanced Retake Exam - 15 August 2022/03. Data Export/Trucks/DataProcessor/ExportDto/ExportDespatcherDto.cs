namespace Trucks.DataProcessor.ExportDto
{
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [XmlType("Despatcher")]
    public class ExportDespatcherDto
    {
        [XmlElement("DespatcherName")]
        public string DespatcherName { get; set; }

        [XmlAttribute("TrucksCount")]
        public int TrucksCount { get; set; }

        [XmlArray("Trucks")]
        public ExportDespatcherTruckDto[] Trucks { get; set; }
    }
}
