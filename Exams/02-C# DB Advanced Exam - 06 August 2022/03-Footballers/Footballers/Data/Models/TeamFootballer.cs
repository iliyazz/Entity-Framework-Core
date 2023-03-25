namespace Footballers.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TeamFootballer
    {
        //[Key]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }





        //[Key]
        [ForeignKey(nameof(Footballer))]
        public int FootballerId { get; set; }
        public virtual Footballer Footballer { get; set; }


    }
}
