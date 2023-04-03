namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Creator
    {
        public Creator()
        {
            this.Boardgames = new HashSet<Boardgame>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        //[MinLength(ValidationConstants.CreatorFirstNameMinLength)]
        [MaxLength(ValidationConstants.CreatorLastNameMaxLength)]
        public string FirstName { get; set; }


        [Required]
        //[MinLength(ValidationConstants.CreatorLastNameMinLength)]
        [MaxLength(ValidationConstants.CreatorLastNameMaxLength)]
        public string LastName { get; set; }



        public ICollection<Boardgame> Boardgames { get; set; }
    }
}
