namespace Theatre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Theatre
    {
        public Theatre()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        [Key]
        public int Id { get; set; }



        [MaxLength(ValidationConstants.TheatreNameMaxLength)]
        public string Name { get; set; }



        [Required]
        [MaxLength(ValidationConstants.TheatreNumberOfHallsMaxValue)]
        public sbyte NumberOfHalls { get; set; }



        [MaxLength(ValidationConstants.TheatreDirectorMaxValue)]
        public string Director { get; set; }



        public virtual ICollection<Ticket> Tickets{ get; set; }
        
    }
}
