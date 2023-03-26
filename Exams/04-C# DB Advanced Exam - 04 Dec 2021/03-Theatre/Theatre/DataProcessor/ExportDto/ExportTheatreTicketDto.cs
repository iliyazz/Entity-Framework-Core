namespace Theatre.DataProcessor.ExportDto
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Newtonsoft.Json;


    public class ExportTheatreTicketDto
    {
        [JsonProperty(nameof(Name))]
        public string Name { get; set; }


        [JsonProperty(nameof(NumberOfHalls))]
        public sbyte NumberOfHalls { get; set; }


        [JsonProperty(nameof(TotalIncome))]
        public decimal TotalIncome { get; set; }


        [JsonProperty(nameof(Tickets))]
        public ExportTicketDto[] Tickets { get; set; }

    }
}
