namespace CarDealer.DTOs.Export.CarsWithTheirListOfParts
{
    using System.Xml.Serialization;


    [XmlType("part")]
    public class ExportListOfPartsDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = null!;


        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
