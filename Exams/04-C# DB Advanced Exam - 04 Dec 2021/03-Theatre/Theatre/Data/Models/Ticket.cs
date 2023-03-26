namespace Theatre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;

    public class Ticket
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Range(typeof(decimal) ,"1.0", "100.0")]
        public decimal Price { get; set; }


        [Required]
        [Range(ValidationConstants.TicketRowNumberMinValue, ValidationConstants.TicketRowNumberMaxValue)]
        public sbyte RowNumber { get; set; }


        [Required]
        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }
        public Play Play { get; set; }


        [Required]
        [ForeignKey(nameof(Theatre))]
        public int TheatreId { get; set; }
        public Theatre Theatre { get; set; }
    }
}
