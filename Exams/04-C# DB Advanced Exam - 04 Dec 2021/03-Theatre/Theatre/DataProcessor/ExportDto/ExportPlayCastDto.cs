namespace Theatre.DataProcessor.ExportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Common;
    using Data.Models.Enums;

    [XmlType("Play")]
    public class ExportPlayCastDto
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }


        [XmlAttribute("Duration")]
        public string Duration { get; set; }


        [XmlAttribute("Rating")]
        public string Rating { get; set; }


        [XmlAttribute("Genre")]
        public string Genre { get; set; }


        [XmlArray("Actors")]
        public ExportCastDto[] Actors { get; set; }
    }
}
