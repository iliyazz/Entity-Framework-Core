namespace Theatre.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using Theatre.Common;
    using Theatre.Data.Models;

    public class ImportTicketDto
    {
        [Required]
        [Range(typeof(decimal), "1.0", "100.0")]
        [JsonProperty(nameof(Price))]
        public decimal Price { get; set; }


        [Required]
        [Range(ValidationConstants.TicketRowNumberMinValue, ValidationConstants.TicketRowNumberMaxValue)]
        [JsonProperty(nameof(RowNumber))]
        public sbyte RowNumber { get; set; }


        [Required]
        [ForeignKey(nameof(Play))]
        [JsonProperty(nameof(PlayId))]
        public int PlayId { get; set; }
    }
}
