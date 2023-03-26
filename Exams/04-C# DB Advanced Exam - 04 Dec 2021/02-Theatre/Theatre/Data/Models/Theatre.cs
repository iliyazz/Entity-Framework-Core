﻿namespace Theatre.Data.Models
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


        [Required]
        [MinLength(ValidationConstants.TheatreNameMinLength)]
        [MaxLength(ValidationConstants.TheatreNameMaxLength)]
        public string Name { get; set; }



        [Required]
        [Range(ValidationConstants.TheatreNumberOfHallsMinValue, ValidationConstants.TheatreNumberOfHallsMaxValue)]
        public sbyte NumberOfHalls { get; set; }


        [Required]
        [MinLength(ValidationConstants.TheatreDirectorMinLength)]
        [MaxLength(ValidationConstants.TheatreDirectorMaxLength)]
        public string Director { get; set; }



        public virtual ICollection<Ticket> Tickets{ get; set; }
        
    }
}
