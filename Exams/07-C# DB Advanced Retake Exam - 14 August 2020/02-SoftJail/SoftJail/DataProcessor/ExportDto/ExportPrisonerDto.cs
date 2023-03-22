namespace SoftJail.DataProcessor.ExportDto
{
    using System;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    [XmlType("Prisoner")]
    public class ExportPrisonerDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }


        [XmlElement("Name")]
        public string FullName { get; set; }


        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }


        [XmlArray("EncryptedMessages")]
        public ExportPrisonerMailsDto[] Mails { get; set; }

    }
}
