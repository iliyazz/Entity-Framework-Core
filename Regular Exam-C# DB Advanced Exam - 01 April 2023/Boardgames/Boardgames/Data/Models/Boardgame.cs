namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Boardgames.Data.Models.Enums;
    using Common;

    public class Boardgame
    {
        public Boardgame()
        {
            this.BoardgamesSellers = new HashSet<BoardgameSeller>();
        }

        [Key]
        public int Id { get; set; }


        [Required]
        //[MinLength(ValidationConstants.BoardgameNameMinLength)]
        [MaxLength(ValidationConstants.BoardgameNameMaxLength)]
        public string Name { get; set; }


        [Required]
        //[Range(ValidationConstants.BoardgameRatingMinValue, ValidationConstants.BoardgameRatingMaxValue)]
        public double Rating { get; set; }


        [Required]
        //[Range(ValidationConstants.BoardgameYearPublishedMinValue, ValidationConstants.BoardgameYearPublishedMaxValue)]
        public int YearPublished { get; set; }


        [Required]
        public CategoryType CategoryType { get; set; }


        [Required]
        public string Mechanics { get; set; }


        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }
        public Creator Creator { get; set; }



        public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }

    }
}
