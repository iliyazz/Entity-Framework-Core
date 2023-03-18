namespace CarDealer.DTOs.Import
{
    using Newtonsoft.Json;
    using System.Xml.Linq;

    //[JsonObject]
    public class ImportPartDto
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("supplierId")]
        public int SupplierId { get; set; }

        /*
        "name": "Bonnet/hood",
        "price": 1001.34,
        "quantity": 10,
        "supplierId": 17
        -----------------------------
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
         */
    }
}
