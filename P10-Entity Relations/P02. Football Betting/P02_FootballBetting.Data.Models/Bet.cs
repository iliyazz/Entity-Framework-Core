namespace P02_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;


    public class Bet
    {
        [Key]
        public int BetId { get; set; }
        
        public decimal Amount { get; set; }


        //enumeration are not null
        [Required]
        public Prediction Prediction { get; set; }

        //Required by default
        //DATETIME2 -> SQL
        //C# -> DateTime, DateTime? (nullable) -> DATETIME2
        //[Column(TypeName = "DATETIME2")]
        public DateTime DateTime { get; set;}



        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;



        [ForeignKey(nameof(Game))]
        public int GameId { get; set; } //foreign key
        public virtual Game Game { get; set; } = null!;//navigation property

    }
}
