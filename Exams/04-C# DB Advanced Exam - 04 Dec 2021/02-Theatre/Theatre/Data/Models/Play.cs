namespace Theatre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using global::Theatre.Data.Models.Enums;

    public class Play
    {
        public Play()
        {
            this.Tickets = new HashSet<Ticket>();
            this.Casts = new HashSet<Cast>();
        }
        [Key]
        public int Id { get; set; }
        

        [MaxLength(ValidationConstants.PlayTitleMaxLength)]
        public string Title { get; set; }
        
        
        public TimeSpan Duration { get; set; }
        

        [Required]
        public float Rating { get; set; }
        

        public Genre Genre { get; set; }


        [MaxLength(ValidationConstants.PlayDescriptionMaxLength)]
        public string Description { get; set; }
        

        [MaxLength(ValidationConstants.PlayScreenwriterMaxLength)]
        public string Screenwriter { get; set; }

        
        public virtual ICollection<Cast> Casts { get; set; }


        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
