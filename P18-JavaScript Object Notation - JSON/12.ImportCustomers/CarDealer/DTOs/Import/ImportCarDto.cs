namespace CarDealer.DTOs.Import
{
    using Newtonsoft.Json;

    public class ImportCarDto
    {
        [JsonProperty("make")]
        public string Make { get; set; } = null!;

        [JsonProperty("model")]
        public string Model { get; set; } = null!;

        [JsonProperty("traveledDistance")]
        public long TravelledDistance { get; set; }

        [JsonProperty("partsId")]
        public int[] PartsId { get; set; }





        /*
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public long TravelledDistance { get; set; }
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();    
        public virtual ICollection<PartCar> PartsCars { get; set; } = new List<PartCar>();


         "make": "Opel",
         "model": "Omega",
         "traveledDistance": 176664996,
         "partsId": [
           38,
           102,
           23,
           116,
           46,
           68,
           88,
           104,
           71,
           32,
           114
         ]
         */
    }
}
