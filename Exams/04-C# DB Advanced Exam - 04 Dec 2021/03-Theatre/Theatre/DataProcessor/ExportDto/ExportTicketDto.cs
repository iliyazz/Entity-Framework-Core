namespace Theatre.DataProcessor.ExportDto
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Theatre.Common;

    public class ExportTicketDto
    {
        [JsonProperty(nameof(Price))]
        public decimal Price { get; set; }


        [JsonProperty(nameof(RowNumber))]
        public sbyte RowNumber { get; set; }
    }
}
