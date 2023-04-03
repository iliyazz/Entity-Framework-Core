namespace Boardgames.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Creator")]
    public class ExportCreatorDto
    {
        [XmlElement("CreatorName")]
        public string CreatorName { get; set; }



        [XmlAttribute("BoardgamesCount")]
        public int BoardgamesCount { get; set; }



        [XmlArray("Boardgames")]
        public ExportCreatorBoardgameDto[] Boardgames { get; set; }
    }
}
