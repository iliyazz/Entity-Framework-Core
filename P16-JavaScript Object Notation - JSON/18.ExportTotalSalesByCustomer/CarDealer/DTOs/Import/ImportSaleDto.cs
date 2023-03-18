namespace CarDealer.DTOs.Import
{
    using Newtonsoft.Json;

    public class ImportSaleDto
    {
        [JsonProperty("discount")]
        public decimal Discount { get; set; }


        [JsonProperty("carId")]
        public int CarId { get; set; }


        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
    /*
       {
         "carId": 105,
         "customerId": 30,
         "discount": 30
        }
     */
}
