namespace CarDealer.DTOs.Export.CarsWithTheirListOfParts
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarsWithTheirListOfPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; } = null!;


        [XmlAttribute("model")]
        public string Model { get; set; } = null!;


        [XmlAttribute("traveled-distance")]
        public long TraveledDistance { get; set; }


        [XmlArray("parts")]
        public ExportListOfPartsDto[] Parts { get; set; }
    }
}
