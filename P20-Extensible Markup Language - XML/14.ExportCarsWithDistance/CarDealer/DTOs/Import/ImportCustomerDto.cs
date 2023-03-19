namespace CarDealer.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class ImportCustomerDto
    {
        [XmlElement("name")]
        public string Name { get; set; } = null!;


        [XmlElement("birthDate")] public string BirthDate { get; set; } = null!;


        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
/*
 <Customers>
    <Customer>
        <name>Emmitt Benally</name>
        <birthDate>1993-11-20T00:00:00</birthDate>
        <isYoungDriver>true</isYoungDriver>
    </Customer>
 */
