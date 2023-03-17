namespace CarDealer.DTOs.Import
{
    using Newtonsoft.Json;
    using System.Xml.Linq;

    public class ImportSupplierDto
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("isImporter")]
        public bool IsImporter { get; set; }



        /*
        "name": "3M Company",
        "isImporter": true

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsImporter { get; set; }
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
         */
    }
}
