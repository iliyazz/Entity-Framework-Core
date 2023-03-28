namespace Trucks.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;


    [XmlType("Despatcher")]
    public class ImportDespatcherDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(ValidationConstants.DespatcherNameMinLength)]
        [MaxLength(ValidationConstants.DespatcherNameMaxLength)]
        public string Name { get; set; }



        [XmlElement("Position")]
        public string Position { get; set; }


        [XmlArray("Trucks")]
        public ImportDespatcherTruckDto[] Trucks { get; set; }
    }
}
